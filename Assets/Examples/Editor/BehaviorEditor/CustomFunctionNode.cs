using Assets.Examples.Editor.BehaviorEditor.TypeSystem;
using GraphProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Examples.Editor.BehaviorEditor
{
    internal class CustomFunctionNode: BaseNode
    {
        public FunctionType Function { get; private set; }
        public override string name => Function?.Name;
        public void Init(FunctionType function)
        {
            Function = function;
        }
    }
}
