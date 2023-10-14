using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys.Base
{
    public class RepositoryBase
    {
        protected readonly IDbConnection _dbConnection;
        protected string Message { get; set; }
        protected bool OperationSuccess { get; set; }


        public RepositoryBase(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Método responsável por fazer o execute async na base de dados
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected async Task<bool> ExecuteAsync(string query, object param = null)
        {
            bool result = false;
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();


                int rowsAffecteds = await _dbConnection.ExecuteAsync(query, param);

                result = rowsAffecteds > 0;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                Message = ex.Message;
                OperationSuccess = false;
                result = false;
            }
            finally { _dbConnection.Close(); }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected async Task<List<int>> QueryAsync(string query)
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();

                var result = await _dbConnection.QueryAsync<int>(query);

                return result.ToList();

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                Message = ex.Message;
                OperationSuccess = false;
                return null;
            }
            finally { _dbConnection.Close(); }
        }

        /// <summary>
        /// Método responsável fazer o query na base (select)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected async Task<List<T>> QueryAsync<T>(string query) where T : class
        {
            IEnumerable<T> result;

            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();

                result = await _dbConnection.QueryAsync<T>(query);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                Message = ex.Message;
                OperationSuccess = false;
                return null;
            }
            finally { _dbConnection.Close(); }

            return result?.ToList() ?? new List<T>();
        }
        protected async Task<IEnumerable<T>> QueryAsync<T>(string query, object param) where T : class
        {
            try
            {
                return await _dbConnection.QueryAsync<T>(query, param);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                Message = ex.Message;
                OperationSuccess = false;
                return null;
            }

        }

        protected async Task<IEnumerable<int>> QueryAsync(string query, object param)
        {
            try
            {
                return await _dbConnection.QueryAsync<int>(query, param);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                Message = ex.Message;
                OperationSuccess = false;
                return null;
            }

        }

        protected async Task<bool> ExecuteCommand(IDbCommand cmd)
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();

                cmd.Connection = _dbConnection;

                var result = cmd.ExecuteNonQuery();

                return result > 0;

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                Message = ex.Message;
                OperationSuccess = false;
                return false;
            }
            finally { _dbConnection.Close(); }
        }

        protected async Task<object?> ExecuteScalarAsync(IDbCommand cmd)
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();

                cmd.Connection = _dbConnection;

                return cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                Message = ex.Message;
                OperationSuccess = false;
                return false;
            }
            finally { _dbConnection.Close(); }
        }


        public string GetMessage() => Message;

    }
}
