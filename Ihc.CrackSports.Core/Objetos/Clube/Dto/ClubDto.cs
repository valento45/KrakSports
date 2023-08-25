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
        public string endereco { get; set; }
        public int numero { get; set; }
        public string cidade { get; set; }
        public string cep { get; set; }
        public string uf { get; set; }
        public string imagem_base64 { get; set; }

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
            imagem_base64 = imagem_base64;

            return club;
        }
    }
}
