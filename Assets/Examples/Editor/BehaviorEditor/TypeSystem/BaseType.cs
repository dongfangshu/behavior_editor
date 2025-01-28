using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Examples.Editor.BehaviorEditor.TypeSystem
{
    abstract class BaseType
    {
        public string Name { get; set; }
        protected string Code { get; }
        protected BaseType(string code)
        {
            Code = code;
        }
    }
}
