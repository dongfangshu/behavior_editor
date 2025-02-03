using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaAnalyzer.TypeSystem
{
    internal class FieldInfo
    {
        public bool IsPublic;
        public string Desc;
        public string Name;
        public string TypeName;

        public static FieldInfo TryParse(string anno)
        {
            FieldInfo fieldInfo = new FieldInfo();
            string[] array = anno.Split(' ');
            Queue<string> queue = new Queue<string>(array);
            int index = 0;
            string str = queue.Dequeue();
            for (int i = 0; i < 5; i++)
            {
                if (queue.Count == 0)
                    break;
                if (i == 0)
                {
                    //field
                }
                if (i == 1)
                {
                    //public
                    fieldInfo.IsPublic = str == "public";
                }
                if (i == 2)
                {
                    //name
                    fieldInfo.Name = str;
                }
                if (i == 3)
                {
                    //type
                    fieldInfo.TypeName = str;
                }
                if (i == 4)
                {
                    //desc
                    fieldInfo.Desc = str;
                }

                str = queue.Dequeue();
            }
            return fieldInfo;
        }
    }
}
