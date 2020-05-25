using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace classJW
{
    class MysqlJW
    {
        public class MySqlPars
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
        //public int ExeSql(string sql, List<MySqlPars> pra = null)
        //{
        //    int result;
        //    try
        //    {
        //        MySqlCommand cmd = conn.CreateCommand();
        //        cmd.CommandText = sql;
        //        foreach (var item in pra)
        //        {
        //            cmd.Parameters.Add(new MySqlParameter(item.praName, item.praValue));
        //        }
        //        conn.Open();
        //        result = cmd.ExecuteNonQuery();
        //        conn.Close();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
    }
}
