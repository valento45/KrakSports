using Ihc.CrackSports.Core.Access;
using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using Ihc.CrackSports.Core.Requests.Aluno;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Alunos
{
    public class Aluno : PessoaFisica
    {
        private byte[] _fotoAlunoBytes { get; set; }
        private string _fotoAlunoBase64 { get; set; }

        public long IdClub { get; set; }
        public long IdUsuario { get; set; }
        public string PosicaoJogador { get; set; }
        public int CamisetaNumero { get; set; }
        public string FotoAlunoBase64
        {
            get
            {
                return _fotoAlunoBase64;
            }
            set
            {

                _fotoAlunoBase64 = value;
            }
        }

        public Responsavel Responsavel { get; set; }
        public Usuario Usuario { get; set; }
        public bool HasEditResponsavel { get; set; }

        public byte[] GetBytesFotoAluno()
        {
            if (_fotoAlunoBytes == null)
            {
                _fotoAlunoBytes = Convert.FromBase64String(this.FotoAlunoBase64);
            }
            return _fotoAlunoBytes;
        }





        public Aluno()
        {
            Responsavel = new Responsavel();
            Endereco = new Endereco();
        }

        public Aluno(CadastroRequest cadastro)
        {
            Nome = cadastro.NomeAluno;
            CpfCnpj = cadastro.CpfAluno;
            Documento = cadastro.DocumentoAluno;
            Email = cadastro.Email;
            DataNascimento = cadastro.DataNascimento;
            Endereco = cadastro.Endereco;
            Responsavel = new Responsavel();

            Responsavel responsavelCadastro = new Responsavel(cadastro.NomeResponsavel, cadastro.CpfResponsavel, cadastro.DocumentoResponsavel, cadastro.GrauParentesco,
                cadastro.TelefoneResponsavel, cadastro.CelularResponsavel);

            Responsavel = responsavelCadastro;
        }

        public Aluno(DataRow dr)
        {

            ///Pessoa base
            Id = long.Parse(dr["id_aluno"].ToString());
            Nome = dr["nome"].ToString();
            IsPj = dr["is_pj"] != DBNull.Value ? bool.Parse(dr["is_pj"].ToString()) : false;
            CpfCnpj = dr["cpf_cnpj"] != DBNull.Value ? long.Parse(dr["cpf_cnpj"].ToString()) : 0;
            Endereco = new Endereco();
            Endereco.Logradouro = dr["endereco"].ToString();
            Endereco.Numero = dr["numero"] != DBNull.Value ? int.Parse(dr["numero"].ToString()) : 0;
            Endereco.Cidade = dr["cidade"].ToString();
            Endereco.UF = dr["uf"].ToString();
            Endereco.CEP = dr["cep"].ToString();
            Endereco.Complemento = dr["complemento"].ToString();

            ///Pessoa Fisica
            DataNascimento = dr["data_nascimento"] != DBNull.Value ? DateTime.Parse(dr["data_nascimento"].ToString()) : new DateTime();
            Documento = dr["documento"].ToString();
            Email = dr["email"].ToString();
            Telefone = dr["telefone"].ToString();
            Celular = dr["celular"].ToString();
            IsPCD = dr["is_pcd"] != DBNull.Value ? bool.Parse(dr["is_pcd"].ToString()) : false;
            DescricaoPCD = dr["descricao_pcd"].ToString();


            ///Aluno
            IdClub = dr["id_club"] != DBNull.Value ? long.Parse(dr["id_club"].ToString()) : 0;
            IdUsuario = dr["id_usuario"] != DBNull.Value ? long.Parse(dr["id_usuario"].ToString()) : 0;
            PosicaoJogador = dr["posicao_jogador"].ToString();
            FotoAlunoBase64 = dr["foto_base64"].ToString();
            CamisetaNumero = dr["camiseta_numero"] != DBNull.Value ? int.Parse(dr["camiseta_numero"].ToString()) : -1;
        }


        public void InformarResponsavel(Responsavel responsavel)
        {
            Responsavel = responsavel;
        }

        public void InformarResponsavel(List<Responsavel> responsavel)
        {
            Responsavel = responsavel?.FirstOrDefault() ?? new Responsavel();
        }

        public static Aluno GetById(long idAluno)
        {
            string query = "select * from sys.aluno_tb where id_aluno = " + idAluno;
            NpgsqlCommand cmd = new NpgsqlCommand(query);

            var ds = PGAccess.ExecuteReader(cmd);
            if(ds != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    return new Aluno(row);
                }
            }

            return null;           
        }
    }
}
