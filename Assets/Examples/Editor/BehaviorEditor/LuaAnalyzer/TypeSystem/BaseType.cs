using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaAnalyzer.TypeSystem
{
    public class BaseType
    {
        public List<Token> Annotation { get; }
        public List<Token> Origin { get; }
        public string? AnnotationValue { get; protected set; }
        public string Name { get; protected set; }
        public string Value { get; protected set; }
        public string Desc { get; protected set; }
        public bool IsPublic { get; protected set; }
        public BaseType(string name,string value,List<Token> originToken,List<Token> annotation) {
            Name = name;
            Value = value;
            Origin = originToken;
            Annotation = annotation;
        }
        public virtual void SetUp()
        {
            if (Annotation.Count > 0) { 
                StringBuilder sb = new StringBuilder();
                foreach (Token token in Annotation)
                {
                    sb.Append(token.Value);
                }
                AnnotationValue = sb.ToString();

                string[] anno = Annotation.Last().Value.Split(' ');

                var key = anno[0];
                var isPublic = anno[1] == "public";
                IsPublic = isPublic;
                var fieldName = anno[2];
                var type = anno[3];

                if (anno.Length > 4)
                {
                    var desc = anno[4];
                    this.Desc = desc;
                }
                
            }
        }
    }
}
