using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace classJW
{
    public class MssqlJW : IDisposable
    {
        SqlConnection _conn;
        public class ParameterSP
        {
            /// <summary>
            /// 參數名稱
            /// </summary>
            public string praName { get; set; }
            /// <summary>
            /// 參數值
            /// </summary>
            public object praValue { get; set; }//任何物件
            /// <summary>
            /// 參數方向,default InputOutput
            /// </summary>
            public ParameterDirection praType { get; set; } = ParameterDirection.InputOutput;
        }
        public MssqlJW(string connStr)
        {
            _conn = new SqlConnection(connStr);
        }

        public void Dispose()
        {
            _conn.Close();
            _conn.Dispose();
            //throw new NotImplementedException();
        }

        public DataSet GetDataSet(string sqlString)
        {
            try
            {
                _conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlString, _conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }
        public DataSet GetDataSet_pra(string sqlString,List<ParameterSP> parameterSPs)
        {
            try
            {
                _conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlString, _conn);
                //--Pra
                //Input 1 Output 2 InputOutput 3 ReturnValue 6
                foreach (var item in parameterSPs)
                {
                    da.SelectCommand.Parameters.Add(new SqlParameter(item.praName, item.praValue)
                    { Direction = item.praType });
                }
                
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }

        public int exeSql(string sqlString)
        {
            int resultVal = -1;
            try
            {
                SqlCommand cmd = _conn.CreateCommand();
                cmd.CommandText = sqlString;
                _conn.Open();

                resultVal = cmd.ExecuteNonQuery();
                _conn.Close();
                return resultVal;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }
        public int exeSql_pra(string sqlString, List<ParameterSP> parameterSPs)
        {
            int resultVal = -1;
            try
            {
                SqlCommand cmd = _conn.CreateCommand();
                cmd.CommandText = sqlString;
                //--Pra
                //Input 1 Output 2 InputOutput 3 ReturnValue 6
                foreach (var item in parameterSPs)
                {
                    cmd.Parameters.Add(new SqlParameter(item.praName, item.praValue)
                    { Direction = item.praType });
                }
                //cmd.BindByName = true;//解決參數順序問題
                //--
                _conn.Open();

                resultVal = cmd.ExecuteNonQuery();
                _conn.Close();
                return resultVal;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
