using React.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Interface.Common
{
    public interface IGenericRepository<T> where T : class
    {
        Task<APIBaseResponse<IEnumerable<T>>> GetAllAsync();
        Task<APIBaseResponse<T>> GetByIdAsync(object id);
        Task<APIBaseResponse<T>> AddAsync(T entity);
        Task<APIBaseResponse<T>> UpdateAsync(T entity);
        Task<APIBaseResponse<T>> DeleteAsync(T entity);
    }   
}
