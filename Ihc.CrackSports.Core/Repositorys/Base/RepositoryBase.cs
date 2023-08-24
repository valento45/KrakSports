﻿using Dapper;
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
            try
            {
                _dbConnection.Open();


                int result = await _dbConnection.ExecuteAsync(query, param);

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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected async Task<List<int>> QueryAsync(string query)
        {
            try
            {
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
            try
            {
                _dbConnection.Open();

                var result = await _dbConnection.QueryAsync<T>(query);

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

        protected async Task<bool> ExecuteCommand(IDbCommand cmd)
        {
            try
            {
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
