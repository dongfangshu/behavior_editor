using Assets.Examples.Editor.BehaviorEditor.TypeSystem;
using GraphProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Examples.Editor.BehaviorEditor
{
    [System.Serializable,NodeMenuItem("Custom/Tenplate")]
    internal class TenplateNode : BaseNode
    {
        public List<BaseType> Member = new List<BaseType>();
        public LuaCode AST;
        public override string name => AST?.className;

        //public TenplateNode(string templatePath)
        //{
        //    AST = LuaAnalyzeHelper.Analyze(templatePath);
        //    Member.AddRange(AST.Variable);
        //}
        public void Init(string templatePath)
        {
            AST = LuaAnalyzeHelper.Analyze(templatePath);
            Member.AddRange(AST.Variable);
        }
    }
}
