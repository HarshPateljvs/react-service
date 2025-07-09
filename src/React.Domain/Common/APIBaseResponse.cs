using Newtonsoft.Json;
using System.Net;

namespace React.Domain.Common
{
    public class APIBaseResponse<T>
    {
        public T Data { get; set; }
        public int TotalCount { get; set; } = 0;

        public List<string> ErrorMessage { get; set; } = new List<string>();
        public List<string> InfoMessage { get; set; } = new List<string>();
        public List<string> WarningMessage { get; set; } = new List<string>();
        public List<string> ValidationMessage { get; set; } = new List<string>();
        public bool HasError => ErrorMessage.Any() || ValidationMessage.Any();
        public string ResponseCode { get; set; }
        public int StatusCode { get; set; }
        public APIBaseResponse()
        {
        }
    }
}
