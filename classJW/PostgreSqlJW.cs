using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace classJW
{
    public class PostgreSqlJW
    {
        NpgsqlConnection conn;
        /// <summary>
        /// 初始化連線
        /// </summary>
        /// <param name="connectionString">連線字串範例：Server=10.66.16.30;Port=5433;Database=iop;Username=postgres;Password=postgres;</param>
        public PostgreSqlJW(string connectionString)
        {
            conn = new NpgsqlConnection(connectionString);
        }

        public DataSet getDataSet(string sqlString)
        {
            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqlString, conn);
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
    }
}
