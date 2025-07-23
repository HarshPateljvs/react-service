using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Domain.Common
{
    public class ResponseCodes
    {
        // ✅ Success
        public const string SUCCESS = "200";
        public const string CREATED = "201";

        // ⚠️ Client-side errors
        public const string BAD_REQUEST = "400";
        public const string VALIDATION_FAILED = "4001";
        public const string UNAUTHORIZED = "401";
        public const string FORBIDDEN = "403";
        public const string NOT_FOUND = "404";

        // ❌ Server-side errors
        public const string INTERNAL_SERVER_ERROR = "500";
        public const string SERVICE_UNAVAILABLE = "503";

        // 📌 Custom business codes
        public const string USER_NOT_FOUND = "USR_404";
        public const string DUPLICATE_ENTRY = "DUP_409";
        public const string LOGIN_FAILED = "AUTH_403";
        public const string PASSWORD_INCORRECT = "AUTH_402";
        public const string TOKEN_EXPIRED = "AUTH_498";
        public const string ACCESS_DENIED = "AUTH_403";

        // 💡 Informational or fallback codes
        public const string NO_CONTENT = "204";
        public const string UNKNOWN_ERROR = "UNK_500";
        public const string CORS_ORIGIN_NOT_ALLOWED = "CROS";
    }
}
