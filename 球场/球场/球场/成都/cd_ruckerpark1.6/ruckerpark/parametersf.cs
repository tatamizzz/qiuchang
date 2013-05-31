using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace ruckerpark
{
    class parametersf
    {
        public static string username;
        public static bool dgf1, dgf2;

        public static bool isnullrecord(string commstr)//判断记录是否存在
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myconn.Open();
            if (Convert.ToInt16(mycomm.ExecuteScalar().ToString()) != 0)
            {
                myconn.Close();
                return true;
            }
            else
            {
                myconn.Close();
                return false;
            }
        }

        public static string checkstr(string oldstr)
        {
            
            return oldstr.Replace("'", "");
            
        }

        public static bool checknum(string oldstr)
        {
            string num = "0123456789";
            for (int i = 0; i < oldstr.Length; i++)
            {
                if (num.IndexOf(oldstr.Substring(i, 1)) == -1)
                {
                    return false;
                }
                
            }
            return true;
        }

        public static void showmessage(string txt)
        {
            showmsg myshow = new showmsg();
            myshow.settext(txt);
            myshow.ShowDialog();
        }
    }
}
