using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaAnalyzer.TypeSystem
{
    internal class TableType : BaseType
    {
        public ClassInfo ClassInfo;
        public List<FieldInfo> FieldInfos { get; set; }
        public bool IsArray { get; private set; }
        public TableType(string name, string value, List<Token> originToken, List<Token> annotation) : base(name, value, originToken, annotation)
        {
        }

        public override void SetUp()
        {
            if (Annotation.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Token token in Annotation)
                {
                    sb.Append(token.Value);
                }
                AnnotationValue = sb.ToString();

                //string[] anno = Annotation.Last().Value.Split(' ');

                //var key = anno[0];
                //var isPublic = anno[1] == "public";
                //IsPublic = isPublic;
                //var fieldName = anno[2];
                //var type = anno[3];

                //if (IsPrimitiveType(type))
                //{
                    
                //}
                //else
                //{
                    
                //}

                //if (anno.Length > 4)
                //{
                //    var desc = anno[4];
                //    this.Desc = desc;
                //}

                FieldInfos = new List<FieldInfo>();
                foreach (var anno in Annotation)
                {
                    if (anno.Value.Contains("class"))
                    {
                        ClassInfo = ClassInfo.TryParse(anno.Value);
                    }
                    else if (anno.Value.Contains(Name))
                    {
                        if (anno.Value.Contains("[]"))
                        {
                            IsArray = true;
                            IsPublic = anno.Value.Contains("public");
                        }
                    }
                    else if (anno.Value.Contains("field"))
                    {
                        FieldInfo fieldInfo = FieldInfo.TryParse(anno.Value);
                        FieldInfos.Add(fieldInfo);
                    }
                }
            }
        }
        public void AnalyzeAnno()
        {

        }
        public bool IsPrimitiveType(string type)
        {
            if (type == "number")
            {
                return true;
            }
            if (type == "string")
            {
                return true;
            }
            if (type == "boolean")
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return "table";
        }
    }
}
