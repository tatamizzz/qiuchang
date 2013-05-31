using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ruckerpark
{
    class DBHelp
    {
        //声明 数据库连接相关对象
        private static string url = ConfigurationManager.ConnectionStrings[1].ToString();//连接字符串
        private static SqlConnection con = new SqlConnection(url);         //连接对象
       

        #region 通用 打开数据库连接的方法
        /// <summary>
        /// 通用 打开数据库连接的方法
        /// </summary>
        /// <param name="sql">传入 要执行的SQL命令</param>
        public SqlConnection OpenConnection()
        {
            if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
            {
                try
                {
                    con.Open();
                }
                catch (Exception)
                {

                }
            }
            return con;
           
        }
        #endregion

        #region 通用 关闭数据库连接的方法

        /// <summary>
        /// 通用 关闭数据库连接的方法
        /// </summary>
        public static void CloseConnection()
        {
            try
            {
                //关闭连接对象
                con.Close();
            }
            catch (Exception) { }
        }

        #endregion

        /// <summary>
        /// 通用 查询 信息 的 方法
        /// </summary>
        /// <param name="sql">传入 要查询的 SQL 命令</param>
        /// <returns>返回 查询 出来的结果集合，如果查询失败，返回 null</returns>
        public DataSet ExuteDataset(string sql)
        {
            try
            {
                SqlConnection conn = OpenConnection();
                SqlCommand comm = new SqlCommand(sql, conn);
                DataSet ds = new DataSet();
                new SqlDataAdapter(comm).Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public SqlDataReader ExecuteSqlDataReader(string sql)
        {
            SqlConnection conn = OpenConnection();
            SqlCommand comm = new SqlCommand(sql, conn);
            return comm.ExecuteReader();
        }


        /// <summary>
        /// 通用 增删改 方法
        /// </summary>
        /// <param name="sql">传入 要执行的 增删改 命令</param>
        /// <returns>true 表示 执行 成功 ， false 表示 执行失败</returns>
        public bool InsertUpdateDel(string sql)
        {
            int num = 0;    //受影响行数

            try
            {
                SqlConnection conn = OpenConnection();
                SqlCommand comm = new SqlCommand(sql, conn);
                num = comm.ExecuteNonQuery();

                //判断 执行 是否 成功
                if (num <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                //调用 通用 关闭 方法
                CloseConnection();
            }
        }

        public object ExecuteScalar(string sql)
        {
            SqlConnection conn = OpenConnection();
            SqlCommand comm = new SqlCommand(sql, conn);
            return comm.ExecuteScalar();
        }
    }
}
