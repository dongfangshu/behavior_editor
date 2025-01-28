using Assets.Examples.Editor.BehaviorEditor.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Examples.Editor.BehaviorEditor.AST
{
    public enum BodyType
    {
        Function = 1,
        Variable = 2,
    }
    internal class CodeBody
    {
        public string attribute = "";
        public BodyType bodyType;
        public string[] Code { get; }
        public CodeBody(string[] code) { 
            Code = code;
            foreach (var c in code)
            {
                if (c.Contains("---@field"))
                {
                    bodyType = BodyType.Variable;
                    break;
                }
                if (c.Contains("---@function"))
                {
                    bodyType = BodyType.Function;
                }
            }
        }
        public BaseType GetBaseType()
        {
            BaseType baseType = null;
            if (bodyType == BodyType.Function)
            {
                baseType =  new FunctionType(Code[1]);
            }
            else
            {
                string[] att = Code[0].Split(" ");
                var fieldName = att[1];
                var fieldType = att[2];
                if (fieldType.Contains("bool"))
                {
                    baseType = new BoolType("");
                    baseType.Name = fieldName;
                }
                else if (fieldType.Contains("number"))
                {
                    baseType = new NumberType("");
                    baseType.Name = fieldName;
                }
                else if (fieldType.Contains("[]"))
                {
                    var array = new ArrayType("");
                    array.Name = fieldName;
                    //array.ItemType = new CodeBody().GetBaseType();
                }
                else if (fieldType.Contains("string"))
                {
                    var stringType = new StringType("");
                    stringType.Name = fieldName;
                    baseType = stringType;
                }
                else
                {
                    throw new NotImplementedException("");
                }
            }
            return baseType;
        }
    }
}
