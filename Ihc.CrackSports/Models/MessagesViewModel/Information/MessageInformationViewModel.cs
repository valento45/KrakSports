using System.ComponentModel;

namespace Ihc.CrackSports.WebApp.Models.MessagesViewModel.Information
{
	public class MessageInformationViewModel
	{
		public string Titulo { get; private set; }
		public string Message { get; private set; }
		public TipoMessage Tipo { get; set; }
		public string UrlRedirect { get;  set; }
		public string UrlVoltar { get; set; }
		public bool Sucesso { get; set; }


		public MessageInformationViewModel()
		{

		}

		public MessageInformationViewModel(TipoMessage tipo)
		{
			Tipo = tipo;
			Sucesso = Tipo != TipoMessage.Erro;
		}

		public string ObterAcao(TipoMessage tipo)
		{
			switch (tipo)
			{
				case TipoMessage.Insercao:

					return "cadastrado com sucesso !";

				case TipoMessage.Alteracao:

					return "atualizado com sucesso !";

				case TipoMessage.Exclusao:

					return "excluído com sucesso !";

				case TipoMessage.Erro:

					return "Erro ao realizar a operação! Por favor, tente mais tarde.";

				default:
					return "";
			}
		}


		public void InformarMessage(string message)
		{
			Message = message;
		}


		public void InformarTitulo(string titulo)
		{
			Titulo = titulo;
		}
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
