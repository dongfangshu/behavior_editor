using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaAnalyzer.TypeSystem
{
    public class NumberType: BaseType
    {
        public NumberType(string name, string value, List<Token> originToken, List<Token> annotation) : base(name, value, originToken, annotation)
        {
        }

        public int AsInt
        {
            get
            {
                return int.Parse(Value);
            }
        }
        public long AsLong
        {
            get
            {
                return long.Parse(Value);
            }
        }
        public float AsFloat
        {
            get
            {
                return float.Parse(Value);
            }
        }
        public override string ToString()
        {
            return "number";
        }
        public override void SetUp()
        {
            base.SetUp();

        }
    }
}
