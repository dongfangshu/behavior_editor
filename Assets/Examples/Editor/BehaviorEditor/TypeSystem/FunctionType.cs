using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Examples.Editor.BehaviorEditor.TypeSystem
{
    internal class FunctionType : BaseType
    {
        public string FunctionName { get; }
        public List<BaseType> Argument = new List<BaseType>();
        public BaseType ReturnValue { get; } = new NilType("nil");
        public FunctionType(string code) : base(code)
        {

        }
    }
}
