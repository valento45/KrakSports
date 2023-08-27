using Ihc.CrackSports.Core.Objetos.Enums;

namespace Ihc.CrackSports.WebApp.Models.Usuarios
{
    public class UsuarioViewModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}
