using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaAnalyzer.TypeSystem
{
    internal class StringType:BaseType
    {
        public StringType(string name, string value, List<Token> originToken, List<Token> annotation) : base(name, value, originToken, annotation)
        {
        }

        public override string ToString()
        {
            return "string";
        }
    }
}
