using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Clube.Dto
{
    public class ClubDto
    {
        public long id_club { get; set; }
        public string nome_fantasia { get; set; }
        public string razao_social { get; set; }
        public long cpf_cnpj { get; set; }
        public bool is_pj { get; set; }
        public bool is_verificado { get; set; }
        public string endereco { get; set; }
        public int numero { get; set; }
        public string cidade { get; set; }
        public string cep { get; set; }
        public string uf { get; set; }
        public string imagem_base64 { get; set; }
        public DateTime data_fundacao { get; set; }
        public string nome_presidente { get; set; }
        public long id_usuario { get; set; }
        public string sobre_o_clube { get; set; }
        public string celular { get; set; }

        public Club ToClub()
        {
            Club club = new Club();

            club.Id = id_club;
            club.Nome = nome_fantasia;
            club.RazaoSocial = razao_social;
            club.CpfCnpj = cpf_cnpj;
            club.IsPj = is_pj;
            club.Endereco = new Base.Pessoas.Endereco
            {
                Logradouro = endereco,
                Numero = numero,
                Cidade = cidade,
                CEP = cep,
                UF = uf
            };
            club.ImagemBase64 = imagem_base64;
            club.DataFundacao = data_fundacao;
            club.NomePresidente = nome_presidente;
            club.IdUsuario = id_usuario;
            club.TipoUsuario = Enums.TipoUsuario.Club;
            club.SobreOClube = sobre_o_clube;
            club.IsVerificado = is_verificado;
            club.Celular = celular;

            return club;
        }
    }
}
