using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaAnalyzer.TypeSystem
{
    public class BoolType : BaseType
    {
        public BoolType(string name, string value, List<Token> originToken, List<Token> annotation) : base(name, value, originToken, annotation)
        {
        }

        public bool AsBool
        {
            get
            {
                return bool.Parse(Value);
            }
        }
        public override string ToString()
        {
            return "bool";
        }
    }
}
