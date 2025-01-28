using Assets.Examples.Editor.BehaviorEditor.AST;
using Assets.Examples.Editor.BehaviorEditor.TypeSystem;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assets.Examples.Editor.BehaviorEditor
{
    internal static class LuaAnalyzeHelper
    {
        public static LuaCode Analyze(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }
            string[] context = File.ReadAllLines(filePath);
            if (!context[0].Contains("---@class"))
            {
                throw new Exception("格式不对");
            }
            //var fuctions = ExtractFunctions(context);
            //var variables = ExtractSelfVariables(context);
            LuaCode luaCode = new LuaCode();
            luaCode.className = context[0].Split(" ")[1];


            for (int i = 0; i < context.Length - 1; i++)
            {
                string line = context[i];
                if (line.Trim().StartsWith("--") && !line.Trim().StartsWith("---@"))
                {
                    continue;
                }
                string nextLine = context[i + 1];
                if (line.Contains("---@function") && nextLine.Contains("function"))
                {
                    List<string> body = new List<string>();
                    body.Add(line);
                    body.Add(nextLine);
                    for (int j = i + 2; j < context.Length - 1; j++)
                    {
                        body.Add(context[j]);
                        if (context[j].Contains("end"))
                        {
                            break;
                        }
                    }
                    i += body.Count;
                    CodeBody codeBody = new CodeBody(body.ToArray());
                    luaCode.Block.Add(codeBody);
                }

            }

            for (int i = 0; i < context.Length -1; i++)
            {
                string line = context[i];
                if (line.Trim().StartsWith("--") && !line.Trim().StartsWith("---@"))
                {
                    continue;
                }
                string nextLine = context[i + 1];
                if (line.Contains("---@field") && nextLine.Contains("self."))
                {
                    CodeBody codeBody = new CodeBody(new string[] { line, nextLine });
                    i += 2;
                    luaCode.Block.Add(codeBody);
                }
            }


            //for (int i = 0; i < fuctions.Count; i = i + 2)
            //{
            //    string code = fuctions[i];
            //    string attr = fuctions[i+1];
            //    CodeBody codeBody = new CodeBody(new[] { attr,code });
            //    //var type = codeBody.GetBaseType(); 
            //    //FunctionType functionType = new FunctionType(code);
            //    //luaCode.Functions.Add(type);
            //    luaCode.Block.Add(codeBody);
            //}
            //for (int i = 0; i < variables.Count; i = i + 2)
            //{
            //    string code = variables[i];
            //    string attr = variables[i + 1];
            //    CodeBody codeBody = new CodeBody(new[] { attr, code });
            //    //var type = codeBody.GetBaseType();
            //    //luaCode.Variable.Add(type);
            //    luaCode.Block.Add(codeBody);
            //    //var v = TypeFactor.GetCodeType(attr);
            //    //luaCode.Variable.Add(functionType);
            //}
            //foreach (var fuc in fuctions)
            //{
            //    FunctionType functionType = new FunctionType(fuc);
            //    luaCode.Functions.Add(functionType);
            //}
            //foreach (var fuc in variables)
            //{
            //    var v = TypeFactor.GetCodeType(fuc);
            //    luaCode.Variable.Add(v);
            //}
            foreach (var b in luaCode.Block)
            {
                var type = b.GetBaseType();
                if (type is FunctionType f)
                {
                    luaCode.Functions.Add(f);
                }
                else
                {
                    luaCode.Variable.Add(type);
                }
            }
            return luaCode;
        }
        // 提取所有 function
        static List<string> ExtractFunctions(string[] luaCode)
        {
            var functions = new List<string>();
            //var regex = new Regex(@"function\s+(\w+:\w+)");
            //var matches = regex.Matches(luaCode);

            //foreach (Match match in matches)
            //{
            //    functions.Add(match.Groups[1].Value);
            //}
            for (int i = 0; i < luaCode.Length - 1; i++)
            {
                string line = luaCode[i];
                string nextLine = luaCode[i + 1];
                if (line.Contains("---@function") && nextLine.Contains("function"))
                {
                    functions.Add(line);
                    functions.Add(nextLine);
                    for (int j = i+2; j < luaCode.Length - 1; j++)
                    {
                        functions.Add(luaCode[j]);
                        if (luaCode[j].Contains("end"))
                        {
                            break;
                        }
                    }
                    i += functions.Count;
                }
                
            }
            return functions;
        }

        // 提取所有 self 变量
        static List<string> ExtractSelfVariables(string[] luaCode)
        {
            var selfVariables = new List<string>();
            //var regex = new Regex(@"---@field.+\nself\.");
            //var matches = regex.Matches(luaCode);

            //foreach (Match match in matches)
            //{
            //    selfVariables.Add(match.Groups[1].Value);
            //}
            for (int i = 0; i < luaCode.Length - 1; i++)
            {
                string line = luaCode[i];
                string nextLine = luaCode[i + 1];
                if (line.Contains("---@field") && nextLine.Contains("self."))
                {
                    selfVariables.Add(line);
                    selfVariables.Add(nextLine);
                    i += 2;
                }
                
            }

            return selfVariables;
        }
    }
}
