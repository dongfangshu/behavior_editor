using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Examples.Editor.BehaviorEditor.TypeSystem
{
    internal class ArrayType : BaseType
    {
        public BaseType ItemType { get; set; }
        public ArrayType(string code) : base(code)
        {
        }
    }
}
