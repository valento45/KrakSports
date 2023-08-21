using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Alunos.Dto
{
    public class AlunoDto
    {
        public long id_aluno { get; set; }
        public long id_usuario { get; set; }
        public long id_club { get; set; }
        public string nome { get; set; }
        public string documento { get; set; }
        public long cpf_cnpj { get; set; }
        public string foto_base64 { get; set; }
        public DateTime data_nascimento { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string celular { get; set; }
        public bool is_pcd { get; set; }
        public string descricao_pcd { get; set; }
        public string posicao_jogador { get; set; }
        public string endereco { get; set; }
        public int numero { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
        public int camiseta_numero { get; set; }

        public Aluno ToAluno()
        {
            return new Aluno
            {
                Id = id_aluno,
                IdUsuario = id_usuario,
                IdClub = id_club,
                Nome = nome,
                Documento = documento,
                CpfCnpj = cpf_cnpj,
                FotoAlunoBase64 = foto_base64,
                DataNascimento = data_nascimento,
                Email = email,
                Telefone = telefone,
                Celular = celular,
                IsPCD = is_pcd,
                DescricaoPCD = descricao_pcd,
                PosicaoJogador = posicao_jogador,
                Endereco = new Endereco
                {
                    Logradouro = endereco,
                    Numero = numero,
                    Cidade = cidade,
                    UF = uf,
                    CEP = cep,
                    Complemento = ""
                },
                CamisetaNumero = camiseta_numero
            };
        }
    }
}
