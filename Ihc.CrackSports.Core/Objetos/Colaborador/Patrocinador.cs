using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Colaborador
{
    public class Patrocinador
    {
        public long IdPatrocinador { get; set; }
        public string NomeOuRazaoSocial { get; set; }
        public bool IsPj { get; set; }
        public long CpfCnpj { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Mensagem { get; set; }
        public string MensagemResumo
        {
            get
            {
                int lenght = Mensagem?.Length ?? 0;
                if (lenght > 14)
                {
                    return Mensagem?.Substring(0, 14) ?? "";
                }
                return Mensagem ?? "";
            }
        }

        public string NomeRazaoSocialResumo
        {
            get
            {
                int lenght = NomeOuRazaoSocial?.Length ?? 0;
                if (lenght > 18)
                {
                    return NomeOuRazaoSocial?.Substring(0, 18) + "..." ?? "";
                }
                return NomeOuRazaoSocial ?? "";
            }
        }

        public string LogoTipoBase64 { get; set; }
        public string Observacoes { get; set; }
        public StatusPatrocinador Status { get; set; }
        public int OrdemApresentacao { get; set; }


        private string _linkSite { get; set; }
        private string _linkInstagram { get; set; }
        private string _linkLinkedin { get; set; }

        public string LinkSite
        {
            get
            {
                if (!string.IsNullOrEmpty(_linkSite))
                    if (!_linkSite.Contains("http") && !_linkSite.Contains("https"))
                        _linkSite = $"http://{_linkSite}";


                return _linkSite;

            }
            set { _linkSite = value; }
        }


        public string LinkInstagram
        {
            get
            {
                if (!string.IsNullOrEmpty(_linkInstagram))
                    if (!_linkInstagram.Contains("http") && !_linkInstagram.Contains("https"))
                        _linkInstagram = $"http://{_linkInstagram}";

                return _linkInstagram;

            }
            set { _linkInstagram = value; }
        }
        public string LinkLinkedin
        {
            get
            {
                if (!string.IsNullOrEmpty(_linkLinkedin))
                    if (!_linkLinkedin.Contains("http") && !_linkLinkedin.Contains("https"))
                        _linkLinkedin = $"http://{_linkLinkedin}";

                return _linkLinkedin;
            }
            set
            {
                _linkLinkedin = value;
            }
        }

    }

    public enum StatusPatrocinador : int
    {
        Pendente = 0,
        Ativo = 1,
        Bloqueado = 2
    }
}
