using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using Ihc.CrackSports.Core.Objetos.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;   

namespace Ihc.CrackSports.Core.Requests
{
    public class CadastroRequest
    {
        [JsonProperty("idAluno")]
        public long IdAluno { get; set; }

        [JsonProperty("idUsuario")]
        public long IdUsuario { get; set; }

        [JsonProperty("nomeResponsavel")]
        public string NomeResponsavel { get; set; }

        [JsonProperty("documentoResponsavel")]
        public string documentoResponsavel { get; set; }

        [JsonProperty("cpfResponsavel")]
        public long CpfResponsavel { get; set; }

        [JsonProperty("grauParentesco")]
        public string GrauParentesco { get; set; }

        [JsonProperty("telefoneResponsavel")]
        public string TelefoneResponsavel { get; set; }

        [JsonProperty("celularResponsavel")]
        public string CelularResponsavel { get; set; }

        [JsonProperty("nomeAluno")]
        public string NomeAluno { get; set; }

        [JsonProperty("documentoAluno")]
        public string documentoAluno { get; set; }

        [JsonProperty("cpfAluno")]
        public long CpfAluno { get; set; }

        [JsonProperty("dataNascimento")]
        public DateTime DataNascimento { get; set; }

        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("senha")]
        public string Senha { get; set; }

        [JsonProperty("tipo")]
        public TipoUsuario Tipo { get; set; }

        public CadastroRequest()
        {
            
        }
    }
}
