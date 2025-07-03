using Microsoft.EntityFrameworkCore;
using React.DAL.Data;
using React.DAL.Interface.Common;
using React.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace React.DAL.Implementation.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<APIBaseResponse<IEnumerable<T>>> GetAllAsync()
        {
            var data = await _dbSet.ToListAsync();
            return new APIBaseResponse<IEnumerable<T>>
            {
                Data = data,
                ResponseCode = ResponseCodes.SUCCESS
            };
        }

        public async Task<APIBaseResponse<T>> GetByIdAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return new APIBaseResponse<T>
                {
                    ResponseCode = ResponseCodes.NOT_FOUND,
                    ErrorMessage = new List<string> { "Entity not found." },
                };
            }

            return new APIBaseResponse<T>
            {
                Data = entity,
                ResponseCode = ResponseCodes.SUCCESS
            };
        }

        public async Task<APIBaseResponse<T>> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new APIBaseResponse<T>
            {
                Data = entity,
                ResponseCode = ResponseCodes.CREATED
            };
        }

        public async Task<APIBaseResponse<T>> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return new APIBaseResponse<T>
            {
                Data = entity,
                ResponseCode = ResponseCodes.SUCCESS
            };
        }

        public async Task<APIBaseResponse<T>> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return new APIBaseResponse<T>
            {
                Data = entity,
                ResponseCode = ResponseCodes.SUCCESS
            };
        }
    }
}
