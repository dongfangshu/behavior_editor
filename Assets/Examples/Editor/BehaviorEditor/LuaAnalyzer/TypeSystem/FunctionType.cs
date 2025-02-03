using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaAnalyzer.TypeSystem
{
    internal class FunctionType
    {
        public enum Func
        {
            Global,
            Local,
            Member
        }
        public Func FuncModel { get; set; }
        public string FunctionName;
        public string AnnotationValue { get; set; }
        public string ParamterValue { get; set; }
        public FunctionType(Func funcModel, string functionName, List<Token> origin , List<Token> annotation,List<Token> paramter)
        {
            FuncModel = funcModel;
            FunctionName = functionName;
            Annotation = annotation;
            Body = origin;
            Paramter = paramter;
        }

        public List<Token> Annotation { get; }
        public List<Token> Body { get; }
        public List<Token> Paramter { get; }


        public virtual void SetUp()
        {
            StringBuilder sb = new StringBuilder();
            if (Annotation.Count > 0)
            {
                sb.Clear();
                foreach (Token token in Annotation)
                {
                    sb.Append(token.Value);
                }
                AnnotationValue = sb.ToString();
            }
            if (Paramter.Count > 0)
            {
                sb.Clear();
                foreach (Token token in Paramter)
                {
                    sb.Append(token.Value);
                }
                ParamterValue = sb.ToString();
            }
        }
        public override string ToString()
        {
            return $"{FuncModel}:{FunctionName}";
        }
    }
}
