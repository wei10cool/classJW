using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
//MySql.Data 8.0.21
namespace classJW
{
    public class MysqlJW
    {
        MySqlConnection conn;
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
        public MysqlJW(string connectionString){
            conn = new MySqlConnection(connectionString);
        }
        public DataSet getDataSet(string sqlString)
        {
            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, conn);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }
        /// <summary>
        /// 回傳資料DataSet格式_帶參數
        /// </summary>
        /// <param name="sqlString">SQL語法</param>
        /// <param name="parameterSPs">參數清單</param>
        /// <returns></returns>
        public DataSet GetDataSet_pra(string sqlString, List<ParameterSP> parameterSPs)
        {
            try
            {
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, conn); ;
                //--Pra
                //Input 1 Output 2 InputOutput 3 ReturnValue 6
                foreach (var item in parameterSPs)
                {
                    da.SelectCommand.Parameters.Add(new MySqlParameter(item.praName, item.praValue)
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
                conn.Close();
            }
        }
        /// <summary>
        /// 執行SQL-只需要知道成功(>=0)或失敗(-1)
        /// </summary>
        /// <param name="sqlString">SQL語法</param>
        /// <returns>int</returns>
        public int exeSql(string sqlString)
        {
            int resultVal= -1;
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlString;
                conn.Open();

                resultVal = cmd.ExecuteNonQuery();
                conn.Close();
                return resultVal;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        ///  執行SQL-只需要知道成功(>=0)或失敗(-1)
        /// </summary>
        /// <param name="sqlString">SQL語法</param>
        /// <param name="parameterSPs">參數</param>
        /// <returns>int</returns>
        public int exeSql_pra(string sqlString, List<ParameterSP> parameterSPs)
        {
            int resultVal = -1;
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlString;
                //--Pra
                //Input 1 Output 2 InputOutput 3 ReturnValue 6
                foreach (var item in parameterSPs)
                {
                    cmd.Parameters.Add(new MySqlParameter(item.praName, item.praValue)
                    { Direction = item.praType });
                }
                conn.Open();

                resultVal = cmd.ExecuteNonQuery();
                conn.Close();
                return resultVal;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 執行StoreProcedure
        /// </summary>
        /// <param name="procName">參數名稱</param>
        /// <param name="parameterSPs">參數值</param>
        /// <returns>DataSet</returns>
        public DataSet runSP(string procName,List<ParameterSP> parameterSPs)
        {
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(procName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //--Pra
                //Input 1 Output 2 InputOutput 3 ReturnValue 6
                foreach (var item in parameterSPs)
                {
                    cmd.Parameters.Add(new MySqlParameter(item.praName, item.praValue)
                    { Direction = item.praType });
                }
                //--
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                int result = da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        
    }
}
