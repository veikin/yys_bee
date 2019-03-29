using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYS_Bee.Imp
{
    public class AppTools
    {
        /// <summary>
        /// 获取程序根目录
        /// </summary>
        /// <returns></returns>
        public static string GetAppBaseDir()
        {
            string str = System.AppDomain.CurrentDomain.BaseDirectory;
            return str;
        }
    }
}
