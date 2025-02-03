using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaAnalyzer.TypeSystem
{
    internal class ClassInfo
    {
        public string ClassName;
        public string ClassDesc = string.Empty;
        public List<string> BaseClass = new List<string>();
        public static ClassInfo TryParse(string context)
        {
            ClassInfo info = new ClassInfo();
            string[] array = context.Split(' ');
            string classDefine = array[1];
            string[] define = classDefine.Split(':');
            info.ClassName = define[0];
            for (int i = 1; i < define.Length; i++)
            {
                info.BaseClass.Add(define[i]);
            }
            if (array.Length > 2)
            {
                string desc = array[2];
                info.ClassDesc = desc;
            }
            return info;
        }
    }
}
