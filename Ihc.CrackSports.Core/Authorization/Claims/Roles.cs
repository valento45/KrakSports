using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Authorization.Claims
{
    public static class Roles
    {
        #region ROLES
        public const string ALUNO = "aluno";
        public const string CLUB = "club";
        public const string ADMINISTRADOR = "adm";
        #endregion

        #region PERMISSIONS

        private const string INSERT = "ins";
        private const string UPDATE = "upd";
        private const string DELETE = "del";
        private const string READ = "read";
        #endregion



        private static bool IsAdm(this ClaimsPrincipal User)
        {
            return User.Claims.Any(param => param.Value == Roles.ADMINISTRADOR);
        }


        public static bool CanAccess(this ClaimsPrincipal User, string role)
        {
            if (User.IsAdm())
                return true;

            return User?.Claims.Any(param => param.Value.Equals(role)) ?? false;
        }

        public static bool CanInsert(this ClaimsPrincipal User, string role)

        {
            if (User.IsAdm())            
                return true;
            
            return User?.Claims.Any(param => param.Value.Equals($"{Roles.INSERT}-{role}")) ?? false;
        }

        public static bool CanUpdate(this ClaimsPrincipal User, string role)
        {
            if (User.IsAdm())
                return true;

            return User?.Claims.Any(param => param.Value.Equals($"{Roles.UPDATE}-{role}")) ?? false;
        }

        public static bool CanDelete(this ClaimsPrincipal User, string role)
        {
            if (User.IsAdm())
                return true;

            return User?.Claims.Any(param => param.Value.Equals($"{Roles.DELETE}-{role}")) ?? false;

        }

        public static bool CanRead(this ClaimsPrincipal User, string role)
        {
            if (User.IsAdm())
                return true;

            return User?.Claims.Any(param => param.Value.Equals($"{Roles.DELETE}-{role}")) ?? false;
        }

        public static string GetIdentificador(this ClaimsPrincipal User)
        {

            return User?.Claims?.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
        }

        public static bool IsAuthenticated(this ClaimsPrincipal User)
        {
            return User?.Claims?.Any(p => p.Type == ClaimTypes.NameIdentifier) ?? false;
        }


    }
}
