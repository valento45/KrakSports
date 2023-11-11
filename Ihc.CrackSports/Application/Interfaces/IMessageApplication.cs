using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Colaborador;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.WebApp.Models.MessagesViewModel.Information;

namespace Ihc.CrackSports.WebApp.Application.Interfaces
{
	public interface IMessageApplication
	{


		Task<MessageInformationViewModel> GetMessage(Evento evento, TipoMessage tipo);
		Task<MessageInformationViewModel> GetMessage(Club club, TipoMessage tipo);
		Task<MessageInformationViewModel> GetMessage(Aluno aluno, TipoMessage tipo);
		Task<MessageInformationViewModel> GetMessage(Patrocinador patrocinador, TipoMessage tipo, bool sucesso);

    }
}
