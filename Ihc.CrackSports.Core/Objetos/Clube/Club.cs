using Ihc.CrackSports.Core.Access;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Clube
{
    public class Club : PessoaJuridica
    {
        public string ImagemBase64 { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataFundacao { get; set; }
        public string NomePresidente { get; set; }


        private IEnumerable<Aluno> _atletas { get; set; }
        public IEnumerable<Aluno> AtletasClube
        {
            get
            {
                if(_atletas == null)
                    _atletas = new List<Aluno>();   

                return _atletas;
            }
            private set
            {
                _atletas = value;
            }
        }



        public Club()
        {

        }

        public Club(DataRow dr)
        {
            Id = long.Parse(dr["id_club"].ToString());
            IdUsuario = !string.IsNullOrEmpty(dr["id_usuario"].ToString()) ? long.Parse(dr["id_usuario"].ToString()) : 0;
            Nome = dr["nome_fantasia"].ToString();
            RazaoSocial = dr["razao_social"].ToString();
            CpfCnpj = dr["cpf_cnpj"] != DBNull.Value ? long.Parse(dr["cpf_cnpj"].ToString()) : 0;
            IsPj = !string.IsNullOrEmpty(dr["is_pj"].ToString()) ? bool.Parse(dr["is_pj"].ToString()) : false;
            Endereco = new Endereco
            {
                Logradouro = dr["endereco"].ToString(),
                Numero = !string.IsNullOrEmpty(dr["numero"].ToString()) ? int.Parse(dr["numero"].ToString()) : 0,
                Cidade = dr["cidade"].ToString(),
                CEP = dr["cep"].ToString(),
                UF = dr["uf"].ToString()
            };

            ImagemBase64 = dr["imagem_base64"].ToString();
            IsVerificado = !string.IsNullOrEmpty(dr["is_verificado"].ToString()) ?  bool.Parse(dr["is_verificado"].ToString()) : false;
            DataFundacao = !string.IsNullOrEmpty(dr["data_fundacao"].ToString()) ? DateTime.Parse(dr["data_fundacao"].ToString()) : new DateTime();
            NomePresidente = dr["nome_presidente"].ToString();          
        }

        public bool HasImagem()
            => !string.IsNullOrEmpty(ImagemBase64);


        public void InformarAtletas(IEnumerable<Aluno> alunos)
        {
            AtletasClube = alunos;
        }

        public static Club ObterClube(long idClube)
        {
            string query = "select * from sys.club_tb where id_club = " + idClube;
            NpgsqlCommand cmd = new NpgsqlCommand(query);

            var result = PGAccess.ExecuteReader(cmd);

            if (result != null)
            {
                foreach (DataRow dr in result.Tables[0].Rows)
                {
                    return new Club(dr);
                }
            }
            return null;
        }

    }
}
