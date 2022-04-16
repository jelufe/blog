using Blog.Domain.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Blog.Api.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        private ClaimsIdentity ClaimsIdentity => new HttpContextAccessor().HttpContext.User.Identity as ClaimsIdentity;
        public int CurrentUserId => int.Parse(ClaimsIdentity.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);
        public string Username => ClaimsIdentity.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
        public string Email => ClaimsIdentity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
        public bool IsAdmin => GetRole() == UserTypeEnumeration.Administrator;
        public bool IsWriter => GetRole() == UserTypeEnumeration.Writer;
        public bool IsReader => GetRole() == UserTypeEnumeration.Reader;

        [ApiExplorerSettings(IgnoreApi = true)]
        public UserTypeEnumeration GetRole()
        {
            string userRole = ClaimsIdentity.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            if (UserTypeEnumeration.Administrator.ToString() == userRole)
                return UserTypeEnumeration.Administrator;
            else if (UserTypeEnumeration.Writer.ToString() == userRole)
                return UserTypeEnumeration.Writer;
            else
                return UserTypeEnumeration.Reader;
        }
    }
}
