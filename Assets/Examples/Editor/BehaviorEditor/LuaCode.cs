using Assets.Examples.Editor.BehaviorEditor.AST;
using Assets.Examples.Editor.BehaviorEditor.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Examples.Editor.BehaviorEditor
{
    internal class LuaCode
    {
        public string className = "Level";
        public List<BaseType> Variable = new List<BaseType>();
        public List<BaseType> Functions = new List<BaseType>();
        public List<CodeBody> Block = new List<CodeBody>();
    }
}
