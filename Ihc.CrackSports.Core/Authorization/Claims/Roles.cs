using Ihc.CrackSports.Core.Objetos.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
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

		private static string _image_base64 { get; set; }

		public static bool IsAdm(this ClaimsPrincipal User)
		{
			return User.Claims.Any(param => param.Value == Roles.ADMINISTRADOR);
		}


		public static int GetTipoUsuario(this ClaimsPrincipal User)
		{
			if (User.IsAdm())
				return (int)TipoUsuario.Administrador;

			else if (User.CanAccess(Roles.ALUNO))
				return (int)TipoUsuario.Aluno;

			else if (User.CanAccess(Roles.CLUB))
				return (int)TipoUsuario.Club;

			return -1;
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

		public static bool HasImage(this ClaimsPrincipal User)
		{
			var hasImg = User?.Claims?.Any(param => param.Type.Equals("Image")) ?? false;

			return hasImg;
		}

		public static void SetImage(string imageBase)
		{
			_image_base64 = imageBase;
		}

		public static string GetImage(this ClaimsPrincipal User)
		{
			return _image_base64;
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
		public static bool IsAluno(this ClaimsPrincipal User)
		{
			return User?.Claims.Any(param => param.Value.Equals($"{Roles.ALUNO}")) ?? false;
		}
		public static bool IsClub(this ClaimsPrincipal User)
		{
			return User?.Claims.Any(param => param.Value.Equals($"{Roles.CLUB}")) ?? false;
		}

		public static bool CanRead(this ClaimsPrincipal User, string role)
		{
			if (User.IsAdm())
				return true;

			return User?.Claims.Any(param => param.Value.Equals($"{Roles.READ}-{role}")) ?? false;
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
