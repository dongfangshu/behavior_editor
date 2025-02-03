using GraphProcessor;
using LuaAnalyzer;
using System.Collections.Generic;

namespace Assets.Examples.Editor.BehaviorEditor
{
    internal class TenplateNode : BaseNode
    {
        public LuaCode AST;
        public override string name => AST?.ClassDesc +'(' + AST?.ClassName + ')';

        //public TenplateNode(string templatePath)
        //{
        //    AST = LuaAnalyzeHelper.Analyze(templatePath);
        //    Member.AddRange(AST.Variable);
        //}
        public void Init(LuaCode code)
        {
            AST = code;
        }
    }
}
