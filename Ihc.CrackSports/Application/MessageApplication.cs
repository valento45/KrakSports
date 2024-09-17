using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Colaborador;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Ihc.CrackSports.WebApp.Models.MessagesViewModel.Information;

namespace Ihc.CrackSports.WebApp.Application
{
    public class MessageApplication : IMessageApplication
    {
        public MessageApplication()
        {

        }

        public Task<MessageInformationViewModel> GetMessage(Patrocinador patrocinador, TipoMessage tipo, bool sucesso)
        {
            var result = new MessageInformationViewModel(tipo) { Sucesso = sucesso };


            if (!result.Sucesso)
                result.InformarMessage($"Erro ao salvar os dados do patrocinador. Por favor, tente mais tarde.");
            else
            {
                result.InformarMessage($"Solicitação enviada com sucesso.");
                result.InformarTextoInformativo($"Em breve entraremos em contato.");
            }


            return Task.FromResult(result);
        }

        public Task<MessageInformationViewModel> GetMessage(Evento evento, TipoMessage tipo)
        {
            var result = new MessageInformationViewModel(tipo);
            
            if (result.Sucesso)
                result.InformarMessage($"Evento {result.ObterAcao(tipo)}");

            else
                result.InformarMessage($"Erro ao salvar o Evento ! Por favor, tente mais tarde.");


            return Task.FromResult(result);
        }

        public Task<MessageInformationViewModel> GetMessage(Club club, TipoMessage tipo)
        {
            var result = new MessageInformationViewModel(tipo);


            if (!result.Sucesso)
                result.InformarMessage($"Erro ao salvar o Clube ! Por favor, tente mais tarde.");
            else
                result.InformarMessage($"Clube {result.ObterAcao(tipo)} {(result.Tipo == TipoMessage.Insercao ? "Você já pode efetuar seu login, porém estará sujeito(a) a uma aprovação pelo Admnistrador antes de desfrutar de todos benefícios." : "")}");


            return Task.FromResult(result);
        }

        public Task<MessageInformationViewModel> GetMessage(Aluno aluno, TipoMessage tipo)
        {
            var result = new MessageInformationViewModel(tipo);


            if (!result.Sucesso)
                result.InformarMessage($"Erro ao salvar os dados do Aluno ! Por favor, tente mais tarde.");
            else
                result.InformarMessage($"Aluno {result.ObterAcao(tipo)} {(result.Tipo == TipoMessage.Insercao ? "Você já pode efetuar seu login." : "")}	");


            return Task.FromResult(result);
        }




    }
}
