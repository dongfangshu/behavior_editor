using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Examples.Editor.BehaviorEditor.TypeSystem
{
    internal class TypeFactor
    {
        public static BaseType GetCodeType(string code)
        {
            if (code.StartsWith("bool"))
            {
                return new BoolType(code);
            }
            throw new NotImplementedException($"body:{code}");
        }
    }
}
