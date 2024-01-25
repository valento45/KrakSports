using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Access
{
    public static class PGAccess
    {       
     
        public static IDbConnection GetConnection()
        {
            string connectionString = @"User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=bd_sports;";

            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            
            return conn;
        }

        public static void FinalizaSessoes(string banco, string applicationName = "")
        {
            try
            {
                string query = $"SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE  pid <> pg_backend_pid() AND state like 'idle' AND datname = '{banco}' AND application_name = '{applicationName}'";
                PGAccess.ExecuteNonQuery(new NpgsqlCommand(query), false);
            }
            catch
            {
            }
        }

        public static int ExecuteNonQuery(IDbCommand pCommand, bool registraLog = true)
        {
            bool retry = false;
            bool transaction = true;
            int rowsaffected = 0;
            do
            {
                if (retry && !transaction)
                    pCommand.Connection = null;

                if (pCommand.Connection == null)
                {
                    pCommand.Connection = GetConnection();
                    transaction = false;
                }
                try
                {
                    foreach (NpgsqlParameter parmt in pCommand.Parameters)
                        try
                        {
                            if (parmt.Value == null || parmt.Value.ToString() == char.MinValue.ToString())
                                parmt.Value = DBNull.Value;
                        }
                        catch { };

                    if (pCommand.Connection.State == ConnectionState.Closed)
                    {
                        transaction = false;
                        pCommand.Connection.Open();
                    }

                    rowsaffected = pCommand.ExecuteNonQuery();

                    if (retry)
                        retry = false;
                    /*
                    #region Criando Log Auditoria
                    if (registraLog)
                    {
                        InsereLogAuditoria(pCommand);
                    }
                    #endregion
                    */
                }
                catch (NpgsqlException ex)
                {
                    /*
                    #region Criando Log Auditoria
                    InsereLogAuditoria(pCommand, ex.Message.ToString());
                    #endregion
                    */

                    // NetworkLog.Insert(ex, pCommand.CommandText);
                    if (ex.Message.Contains("Exception while writing to stream") || ex.Message.Contains("Exception while reading from stream"))
                    {
                        if (!retry)
                            retry = true;
                        else
                        {
                          //  MessageBox.Show("Atenção! Houve perda de conexão com o servidor ou ele demorou muito a responder." + (ex.InnerException != null ? "\r\n\r\nDetalhamento do erro: " + ex.InnerException.Message : "") + "\r\n\r\nÉ possível que as últimas alterações não foram salvas, por favor verifique.", "1) " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            retry = false;
                        }
                        //Application.Exit(new CancelEventArgs(true));
                    }
                    else if (ex.Data["Code"]?.ToString().CompareTo("08P01") == 0) { }
                        //MessageBox.Show("Atenção! Erro ao executar o comando:\r\n\r\n" + pCommand.CommandText + "", "Violação de protocolo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        TrataExcecao(ex, (NpgsqlCommand)pCommand);

                    if (transaction)
                        throw ex;
                }
                finally
                {
                    if (pCommand.Connection.State == ConnectionState.Open && !transaction)
                    {
                        pCommand.Connection.Close();
                    }
                }
            } while (retry);
            return rowsaffected;
        }

       

        public static Object ExecuteScalar(IDbCommand pCommand, bool registraLog = true)
        {
            bool retry = false;
            bool transaction = true;
            Object aux = null;

            do
            {
                if (retry && !transaction)
                    pCommand.Connection = null;

                if (pCommand.Connection == null)
                {
                    pCommand.Connection = GetConnection();
                    transaction = false;
                }
                try
                {
                    foreach (NpgsqlParameter parmt in pCommand.Parameters)
                        try
                        {
                            if (parmt.Value == null || parmt.Value.ToString() == char.MinValue.ToString())
                                parmt.Value = DBNull.Value;
                        }
                        catch { };

                    if (pCommand.Connection.State == ConnectionState.Closed)
                    {
                        transaction = false;
                        pCommand.Connection.Open();
                    }

                    aux = pCommand.ExecuteScalar();
                    if (retry)
                        retry = false;
                    /*
                    #region Criando Log Auditoria
                        if (registraLog)
                        {
                            InsereLogAuditoria(pCommand);
                        }
                    #endregion
                    */
                }
                catch (NpgsqlException ex)
                {
                    /*
                    #region Registra Log Auditoria
                    InsereLogAuditoria(pCommand, ex.Message.ToString());
                    #endregion
                    */
                    var paramsds = "";
                    foreach (NpgsqlParameter p in pCommand.Parameters)
                        paramsds += p.Value;

                    //NetworkLog.Insert(ex, pCommand.CommandText);

                    if (ex.Message.Contains("Exception while writing to stream") || ex.Message.Contains("Exception while reading from stream"))
                    {
                        if (!retry)
                            retry = true;
                        else
                        {
                           // MessageBox.Show("Atenção! Houve perda de conexão com o servidor ou ele demorou muito a responder." + (ex.InnerException != null ? "\r\n\r\nDetalhamento do erro: " + ex.InnerException.Message : "") + "\r\n\r\nÉ possível que as últimas alterações não foram salvas, por favor verifique.", "3) " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            retry = false;
                        }
                        //Application.Exit(new CancelEventArgs(true));
                    }
                    else if (ex.Data["Code"]?.ToString().CompareTo("08P01") == 0) { }
                       // MessageBox.Show("Atenção! Erro ao executar o comando:\r\n\r\n" + pCommand.CommandText + "", "Violação de protocolo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        TrataExcecao(ex, (NpgsqlCommand)pCommand);
                    if (transaction)
                        throw ex;
                    return null;
                }
                finally
                {
                    if (pCommand.Connection.State == ConnectionState.Open && !transaction)
                    {
                        pCommand.Connection.Close();
                    }
                }
            } while (retry);
            return aux;
        }

        public static DataSet ExecuteReader(IDbCommand pCommand)
        {
            bool retry = false;
            DataSet ds = null;
            bool transaction = true;
            do
            {
                // Caso  seja retry, se não recriar a connection vai SEMPRE ocorrer Exception while writing/readig to stream", porém quando é transaction, não pode remover a connection
                if (retry && !transaction)
                    pCommand.Connection = null;

                if (pCommand.Connection == null)
                {
                    pCommand.Connection = GetConnection();
                    transaction = false;
                }

                try
                {
                    foreach (NpgsqlParameter parmt in pCommand.Parameters)
                        try
                        {
                            if (parmt.Value == null || parmt.Value.ToString() == char.MinValue.ToString())
                                parmt.Value = DBNull.Value;
                        }
                        catch { };

                    if (pCommand.Connection.State == ConnectionState.Closed)
                    {
                        pCommand.Connection.Open();
                        transaction = false;
                    }

                    //pCommand.Connection.Open();

                    IDbDataAdapter da = new NpgsqlDataAdapter((NpgsqlCommand)pCommand);

                    ds = new DataSet();

                    da.Fill(ds);

                    //Caso não dê mais erro, remove o retry
                    if (retry)
                        retry = false;
                }
                catch (NpgsqlException ex)
                {
                    // NetworkLog.Insert(ex, pCommand.CommandText);
                    if (ex.Message.Contains("Exception while writing to stream") || ex.Message.Contains("Exception while reading from stream"))
                    {
                        if (!retry)
                            retry = true;
                        else
                        {
                           // MessageBox.Show("Atenção! Houve perda de conexão com o servidor ou ele demorou muito a responder." + (ex.InnerException != null ? "\r\n\r\nDetalhamento do erro: " + ex.InnerException.Message : "") + "\r\n\r\nÉ possível que as últimas alterações não foram salvas, por favor verifique.", "4) " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            retry = false;
                        }

                        //Application.Exit(new CancelEventArgs(true));
                    }
                    else if (ex.Data["Code"]?.ToString().CompareTo("08P01") == 0) { }
                     //   MessageBox.Show("Atenção! Erro ao executar o comando:\r\n\r\n" + pCommand.CommandText + "", "Violação de protocolo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        TrataExcecao(ex, (NpgsqlCommand)pCommand);
                }
                finally
                {
                    if (pCommand.Connection.State == ConnectionState.Open && !transaction)
                    {
                        pCommand.Connection.Close();
                    }
                }
            } while (retry);

            return ds;
        }


        public static void TrataExcecao(NpgsqlException excecao, NpgsqlCommand Comando)
        {
            string detalhes = "";


            if (excecao.Message.ToLower().Contains("violates foreign key"))
            {
               // MessageBox.Show("Este registro está associado a uma tabela e não será possível excluir! \n\r\n\r\n\rPara prosseguir, remova todas as associações e tente novamente.", "Não é possível excluir o registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (Comando.Parameters == null || Comando.Parameters.Count == 0)
                detalhes = "Comando:" + Comando.CommandText;
            else
            {
                detalhes = "Parâmetros:";
                foreach (NpgsqlParameter param in Comando.Parameters)
                    if (param.Value != null)
                        detalhes += param.Value.ToString() + ", ";
                detalhes = detalhes.Substring(0, detalhes.Length - 2) + ".";
            }
            switch (excecao.Data["Code"]?.ToString())
            {
                case "":
                 //   MessageBox.Show("Falha ao estabelecer conexão com o servidor (" + Config.IPServer + "). Verifique:\r\n\r\n* O endereço IP do servidor está correto?\r\n* Porta 5432 no firewall do servidor está desbloqueada?\r\n* As configurações do servidor PostgreSQL estão corretas?", "Falha ao estabelecer conexão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case "28000":
                   // MessageBox.Show("Atenção! O nome do usuário e/ou a senha são inválidos. Tente novamente. ", "Erro ao entrar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case "25P02":
                   // MessageBox.Show("Atenção! A transação atual foi abortada e os dados podem não ter sido salvos completamente. Motivo: " + excecao.Message + " \r\n\r\n" + detalhes, "Transação abortada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case "22P05":
                   // MessageBox.Show("Atenção! Caractere inválido encontrado.\r\nErro:" + excecao.Message + "\r\n\r\n" + detalhes, "Erro ao inserir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case "28P01":
                   // MessageBox.Show("Atenção! O nome do usuário e/ou a senha são inválidos. Tente novamente.", "Erro ao autenticar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case "23505":
                   // MessageBox.Show("Atenção! Não foi possível incluir os dados atuais.\r\nFoi retornado um erro de chave duplicada. Por favor, reinicie o SysGestao e tente novamente.\r\n\r\nCaso o problema persista entre em contato com nosso Suporte Técnico.", "Erro ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    break;

                default:
                    //MessageBox.Show(excecao.Data["Code"]?.ToString() + ": " + excecao.Message + "\r\n\r\n" + detalhes, "Erro no banco de dados", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    break;
            }
        }


    

       
    }
}
