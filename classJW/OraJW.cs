using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;


namespace classJW
{

    public class OraJW:IDisposable
    {
        OracleConnection conn;

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
        public OraJW(string connectionString){
            conn = new OracleConnection(connectionString);
        }

        //回傳資料DataSet格式
         public DataSet getDataSet(string sqlString)
        {
            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                OracleDataAdapter da = new OracleDataAdapter(sqlString, conn);
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
        /// 執行SQL-只需要知道成功(>=0)或失敗(-1)
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public int exeSql(string sqlString)
        {
            int resultVal= -1;
            try
            {
                OracleCommand cmd = conn.CreateCommand();
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
        /// <param name="sqlString"></param>
        /// <param name="parameterSPs">參數</param>
        /// <returns></returns>
        public int exeSql_pra(string sqlString, List<ParameterSP> parameterSPs)
        {
            int resultVal = -1;
            try
            {
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlString;
                //--Pra
                //Input 1 Output 2 InputOutput 3 ReturnValue 6
                foreach (var item in parameterSPs)
                {
                    cmd.Parameters.Add(new OracleParameter(item.praName, item.praValue)
                    { Direction = item.praType });
                }
                cmd.BindByName = true;//解決參數順序問題
                //--
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

                OracleCommand cmd = new OracleCommand(procName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //--Pra
                //Input 1 Output 2 InputOutput 3 ReturnValue 6
                foreach (var item in parameterSPs)
                {
                    cmd.Parameters.Add(new OracleParameter(item.praName, item.praValue)
                    { Direction = item.praType });
                }
                //--
                //int result  = cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                int result = da.Fill(ds);
                //da.Fill(ds);z
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
         public void Dispose()
        {
            conn.Close();
            conn.Dispose();
            //throw new NotImplementedException();
        }

      
    }
}
