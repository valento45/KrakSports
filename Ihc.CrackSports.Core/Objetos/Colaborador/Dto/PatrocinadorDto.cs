using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Ihc.CrackSports.Core.Objetos.Colaborador.Dto
{
    public class PatrocinadorDto
    {

        public long id_patrocinador { get; set; }
        public string nome_razaosocial { get; set; }
        public string email { get; set; }
        public bool is_pj { get; set; }
        public long cpf_cnpj { get; set; }
         public string telefone { get; set; }
        public string celular { get; set; }
        public string mensagem { get; set; }
        public string logotipo_base64 { get; set; }
        public int status { get; set; }        
		 public string observacoes { get; set; }
        public string instagram_url { get; set; }
        public string linkedin_url { get; set; }
        public string site_url { get; set; }
        public int ordem_apresentacao { get; set; }


        public Patrocinador ToPatrocinador()
        {
            return new Patrocinador { 
                IdPatrocinador = id_patrocinador,
                NomeOuRazaoSocial = nome_razaosocial,
                Celular = celular   ,
                Telefone = telefone ,
                CpfCnpj = cpf_cnpj ,
                Email = email ,
                IsPj = is_pj ,
                Mensagem = mensagem ,
                LogoTipoBase64 = logotipo_base64,
                Status   = (StatusPatrocinador)status,  
                Observacoes = observacoes ,
                LinkInstagram = instagram_url ,
                LinkLinkedin = linkedin_url ,
                LinkSite = site_url ,
                OrdemApresentacao = ordem_apresentacao            
            };
        }
    }
}
