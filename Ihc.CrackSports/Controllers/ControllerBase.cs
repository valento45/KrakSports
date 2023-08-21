using Ihc.CrackSports.Core.Authorization.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class ControllerBase : Controller
    {

        protected bool CanAccess(ClaimsPrincipal User, string role)
        {
            return User?.CanAccess(role) ?? false;
        }

        protected bool CanInsert(ClaimsPrincipal User, string role)
        {
            return User?.CanInsert(role) ?? false;
        }

        protected bool CanUpdate(ClaimsPrincipal User, string role)
        {
            return User?.CanUpdate(role) ?? false;
        }

        protected bool CanDelete(ClaimsPrincipal User, string role)
        {
            return User?.CanDelete(role) ?? false;  
        }
    }
}
