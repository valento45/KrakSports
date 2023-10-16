using System.ComponentModel;

namespace Ihc.CrackSports.WebApp.Models.MessagesViewModel.Information
{
    public class MessageInformationViewModel
    {
        public string Titulo { get; set; }
        public string Message { get; set; }
        public TipoMessage Tipo{ get; set; }
        public string UrlRedirect { get; set; }
        public string UrlVoltar { get; set; }
    }

    public enum TipoMessage : int
    {
        [Description("Geral")]
        Geral = 0,

        [Description("Inserção")]
        Insercao = 1,

        [Description("Alteração")]
        Alteracao = 2,

        [Description("Exclusão")]
        Exclusao = 3,

        [Description("Erro")]
        Erro = 99
    }
}
