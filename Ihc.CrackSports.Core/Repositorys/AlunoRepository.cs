using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Alunos.Dto;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Ihc.CrackSports.Core.Repositorys
{
    public class AlunoRepository : RepositoryBase, IAlunoRepository
    {


        public AlunoRepository(IDbConnection connection) : base(connection)
        {

        }



        public async Task<bool> Inserir(Aluno aluno)
        {
            string query = "INSERT INTO sys.aluno_tb (id_usuario, nome, documento, cpf_cnpj, data_nascimento, email, telefone, celular, is_pcd, descricao_pcd, posicao_jogador, " +
                "camiseta_numero, foto_base64," +
                "endereco, numero, cidade, uf, cep) VALUES (@id_usuario, @nome, @documento, @cpf_cnpj, @data_nascimento, @email, @telefone, @celular, @is_pcd, @descricao_pcd, " +
                "@posicao_jogador, @camiseta_numero, @foto_base64, @endereco, @numero, @cidade, @uf, @cep) returning id_aluno;";

            NpgsqlCommand cmd = new NpgsqlCommand(query);
            cmd.Parameters.AddWithValue(@"id_usuario", aluno?.IdUsuario ?? 0);
            cmd.Parameters.AddWithValue(@"nome", aluno?.Nome ?? "");
            cmd.Parameters.AddWithValue(@"documento", aluno?.Documento ?? "");
            cmd.Parameters.AddWithValue(@"cpf_cnpj", aluno?.CpfCnpj ?? 0);
            cmd.Parameters.AddWithValue(@"data_nascimento", aluno?.DataNascimento ?? new DateTime());
            cmd.Parameters.AddWithValue(@"email", aluno?.Email ?? "");
            cmd.Parameters.AddWithValue(@"telefone", aluno?.Telefone ?? "");
            cmd.Parameters.AddWithValue(@"celular", aluno?.Celular ?? "");
            cmd.Parameters.AddWithValue(@"is_pcd", aluno?.IsPCD ?? false);
            cmd.Parameters.AddWithValue(@"descricao_pcd", aluno?.DescricaoPCD ?? "");
            cmd.Parameters.AddWithValue(@"posicao_jogador", aluno?.PosicaoJogador ?? "");
            cmd.Parameters.AddWithValue(@"camiseta_numero", aluno?.CamisetaNumero ?? -1);
            cmd.Parameters.AddWithValue(@"foto_base64", aluno?.FotoAlunoBase64 ?? "");
            cmd.Parameters.AddWithValue(@"endereco", aluno?.Endereco?.Logradouro ?? "");
            cmd.Parameters.AddWithValue(@"numero", aluno?.Endereco?.Numero ?? 0);
            cmd.Parameters.AddWithValue(@"cidade", aluno?.Endereco?.Cidade ?? "");
            cmd.Parameters.AddWithValue(@"uf", aluno?.Endereco?.UF ?? "");
            cmd.Parameters.AddWithValue(@"cep", aluno?.Endereco?.CEP ?? "");

            var result = await base.ExecuteScalarAsync(cmd);
            long codigo;
            if (long.TryParse(result.ToString(), out codigo))
            {
                aluno.Id = codigo;

                if (aluno.Responsavel != null)
                    aluno.Responsavel.IdAluno = codigo;

                return codigo > 0;
            }
            else
                return false;
        }

        public async Task<bool> Atualizar(Aluno aluno)
        {
            string query = "update sys.aluno_tb set id_usuario = @id_usuario, nome = @nome, documento = @documento, cpf_cnpj = @cpf_cnpj, data_nascimento = @data_nascimento, " +
                "email = @email, telefone = @telefone, celular = @celular, is_pcd = @is_pcd, descricao_pcd = @descricao_pcd, posicao_jogador = @posicao_jogador," +
               "foto_base64 = @foto_base64, endereco = @endereco, numero = @numero, cidade = @cidade, uf = @uf, cep = @cep where id_aluno = @id_aluno;";

            NpgsqlCommand cmd = new NpgsqlCommand(query);
            cmd.Parameters.AddWithValue(@"id_aluno", aluno.Id);
            cmd.Parameters.AddWithValue(@"id_usuario", aluno?.IdUsuario ?? 0);
            cmd.Parameters.AddWithValue(@"nome", aluno?.Nome ?? "");
            cmd.Parameters.AddWithValue(@"documento", aluno?.Documento ?? "");
            cmd.Parameters.AddWithValue(@"cpf_cnpj", aluno?.CpfCnpj ?? 0);
            cmd.Parameters.AddWithValue(@"data_nascimento", aluno?.DataNascimento ?? new DateTime());
            cmd.Parameters.AddWithValue(@"email", aluno?.Email ?? "");
            cmd.Parameters.AddWithValue(@"telefone", aluno?.Telefone ?? "");
            cmd.Parameters.AddWithValue(@"celular", aluno?.Celular ?? "");
            cmd.Parameters.AddWithValue(@"is_pcd", aluno?.IsPCD ?? false);
            cmd.Parameters.AddWithValue(@"descricao_pcd", aluno?.DescricaoPCD ?? "");
            cmd.Parameters.AddWithValue(@"posicao_jogador", aluno?.PosicaoJogador ?? "");
            cmd.Parameters.AddWithValue(@"foto_base64", aluno?.FotoAlunoBase64 ?? "");
            cmd.Parameters.AddWithValue(@"endereco", aluno?.Endereco?.Logradouro ?? "");
            cmd.Parameters.AddWithValue(@"numero", aluno?.Endereco?.Numero ?? 0);
            cmd.Parameters.AddWithValue(@"cidade", aluno?.Endereco?.Cidade ?? "");
            cmd.Parameters.AddWithValue(@"uf", aluno?.Endereco?.UF ?? "");
            cmd.Parameters.AddWithValue(@"cep", aluno?.Endereco?.CEP ?? "");


            return await base.ExecuteCommand(cmd);
        }

        public async Task<bool> AtualizarDadosGerais(Aluno aluno)
        {
            string query = "update sys.aluno_tb set nome = @nome, documento = @documento, cpf_cnpj = @cpf_cnpj, data_nascimento = @data_nascimento, " +
               "foto_base64 = @foto_base64, posicao_jogador = @posicao_jogador where id_aluno = @id_aluno;";

            NpgsqlCommand cmd = new NpgsqlCommand(query);
            cmd.Parameters.AddWithValue(@"id_aluno", aluno.Id);
            cmd.Parameters.AddWithValue(@"nome", aluno?.Nome ?? "");
            cmd.Parameters.AddWithValue(@"documento", aluno?.Documento ?? "");
            cmd.Parameters.AddWithValue(@"cpf_cnpj", aluno?.CpfCnpj ?? 0);
            cmd.Parameters.AddWithValue(@"data_nascimento", aluno?.DataNascimento ?? new DateTime());
            //cmd.Parameters.AddWithValue(@"email", aluno?.Email ?? "");
            cmd.Parameters.AddWithValue(@"posicao_jogador", aluno?.PosicaoJogador ?? "");
            cmd.Parameters.AddWithValue(@"foto_base64", aluno?.FotoAlunoBase64 ?? "");



            return await base.ExecuteCommand(cmd);
        }


        public async Task<bool> AtualizarEndereco(Aluno aluno)
        {
            string query = "update sys.aluno_tb set endereco = @endereco, numero = @numero, cidade = @cidade, uf = @uf, cep = @cep, complemento = @complemento where id_aluno = @id_aluno";

            return await base.ExecuteAsync(query, new
            {
                endereco = aluno.Endereco.Logradouro,
                numero = aluno.Endereco.Numero,
                cidade = aluno.Endereco.Cidade,
                uf = aluno.Endereco.UF,
                cep = aluno.Endereco.CEP,
                complemento = aluno.Endereco.Complemento
            });
        }


        public async Task<bool> Excluir(long idAluno)
        {

            string query = "delete from sys.responsavel_aluno_tb where id_aluno = " + idAluno;
            await base.ExecuteAsync(query);
            
            query = "delete from sys.solicitacao_aluno_club_tb where id_aluno = " + idAluno;
            await base.ExecuteAsync(query);

            query = "delete from sys.aluno_tb where id_aluno = " + idAluno;
            return await base.ExecuteAsync(query);
        }


        public async Task<IEnumerable<Aluno>> ObterAlunoByCpf(long cpf)
        {
            string query = "select  * from sys.aluno_tb where cpf_cnpj = @cpf";

            var result = await base.QueryAsync<AlunoDto>(query,
                new
                {
                    cpf = cpf
                });

            return result.Select(x => x.ToAluno()).OrderBy(x => x.Nome);
        }

        public async Task<Aluno> ObterAlunoById(long idAluno)
        {
            string query = "select  * from sys.aluno_tb where id_aluno = @idAluno";

            var result = await base.QueryAsync<AlunoDto>(query,
                new
                {
                    idAluno = idAluno
                });

            return result.Select(x => x.ToAluno())?.FirstOrDefault();
        }

        public Task<IEnumerable<Aluno>> ObterAlunoByNome(string nome)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Aluno>> ObterAlunosByIdClub(long idClub)
        {
            var result = await base.QueryAsync<AlunoDto>($"select  * from sys.aluno_tb where id_club = {idClub}");
            return result.Select(x => x.ToAluno()).OrderBy(x => x.Nome);
        }

        public async Task<IEnumerable<Aluno>> ObterTodosAluno(int limite = 0)
        {
            var result = await base.QueryAsync<AlunoDto>($"select  * from sys.aluno_tb" + (limite > 0 ? $" limit {limite}" : ""));
            return result.Select(x => x.ToAluno()).OrderBy(x => x.Nome);
        }

        public async Task<Aluno?> ObterAlunoByIdUsuario(long idUser)
        {
            string query = "select  * from sys.aluno_tb where id_usuario = @idUser";

            var result = await base.QueryAsync<AlunoDto>(query,
                new
                {
                    idUser = idUser
                });

            return result.Select(x => x.ToAluno())?.FirstOrDefault();
        }

        public async Task<bool> AtualizarCamisa(Aluno aluno)
        {
            string query = "update sys.aluno_tb set camiseta_numero = @camiseta_numero where id_aluno = @id_aluno";

            return await base.ExecuteAsync(query, new
            {
                id_aluno = aluno.Id,
                camiseta_numero = aluno.CamisetaNumero
            });
        }

        public Task<bool> PossuiClub(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public Task<Club> ObterClub(long IdAluno)
        {
            throw new NotImplementedException();
        }
    }
}
