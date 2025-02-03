using LuaAnalyzer.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LuaAnalyzer
{
    internal class LuaCode
    {
        public string ClassName { get;private set; }
        public string ClassDesc { get;private set; } = string.Empty;
        public IEnumerable<BaseType> MemberVariable { get { return membersVariable; } }
        public IEnumerable<BaseType> LocalVariable { get { return localsVariable; } }
        public IEnumerable<FunctionType> Function { get { return functions; } }

        private List<BaseType> membersVariable = new List<BaseType>();
        private List<BaseType> localsVariable = new List<BaseType>();
        private List<FunctionType> functions = new List<FunctionType>();
        public bool TryParse(string code)
        {
            try
            {
                var lexer = LuaLexer.Lex(code);
                int pos = 0;
                int len = lexer.Count;
                List<Token> attr = new List<Token>();
                while (pos < len)
                {
                    var token = lexer[pos];
                    if (token.Type == TokenType.EmmyLuaComment)
                    {
                        attr.Add(token);
                    }
                    if (token.Type == TokenType.EmmyLuaComment && token.Value.Contains("class"))
                    {
                        pos++;
                        var attrArrya = token.Value.Split(' ');
                        var className = attrArrya[1];
                        ClassName = className;
                        if (true)
                        {
                            
                        }
                        continue;
                    }
                    if (token.Type == TokenType.Keyword && token.Value == "local")
                    {
                        //局部变量
                        List<Token> tokens = new List<Token>();
                        tokens.Add(token);

                        pos++;
                        var ldef = lexer[pos];//变量
                        tokens.Add(ldef);

                        pos++;
                        tokens.Add(lexer[pos]);//=

                        pos++;
                        var value = lexer[pos]; //token 开始
                        tokens.Add(lexer[pos]);

                        BaseType baseType = null;
                        if (value.Type == TokenType.Number)
                        {
                            //self number成员
                            baseType = new NumberType(ldef.Value, value.Value, tokens, attr);
                        }
                        else if (value.Type == TokenType.String)
                        {
                            //self 字符串成员
                            baseType = new StringType(ldef.Value, value.Value, tokens, attr);
                        }
                        else if (value.Type == TokenType.Keyword && bool.TryParse(value.Value, out bool result))
                        {
                            //self 布尔成员
                            baseType = new BoolType(ldef.Value, value.Value, tokens, attr);
                        }
                        else if (value.Type == TokenType.Operator && value.Value == "{")
                        {
                            //self table成员

                            List<Token> table = new List<Token>();
                            while (lexer[pos].Type != TokenType.Keyword)
                            {
                                table.Add(lexer[pos]);
                                pos++;
                            }
                            StringBuilder sb = new StringBuilder();
                            foreach (var item in table)
                            {
                                sb.Append(item.Value);
                            }
                            tokens.AddRange(table);
                            baseType = new TableType(ldef.Value, sb.ToString(), tokens, attr);
                        }
                        else if (value.Type == TokenType.Identifier)
                        {
                            //函数
                            continue;
                        }
                        else
                        {
                            throw new Exception("未知类型");
                        }
                        baseType.SetUp();
                        attr = new List<Token>();
                        localsVariable.Add(baseType);

                    }
                    else if (token.Type == TokenType.Keyword && token.Value == "function")
                    {
                        List<Token> define = new List<Token>();
                        List<Token> paramter = new List<Token>();
                        int startIndex = pos;
                        bool receiveParamter = false;
                        do
                        {
                            //body.Add(lexer[pos]);
                            define.Add(lexer[pos]);
                            if (receiveParamter)
                            {
                                paramter.Add(lexer[pos]);
                            }
                            if (lexer[pos].Type == TokenType.Operator && lexer[pos].Value =="(" && !receiveParamter)
                            {
                                //参数开始
                                receiveParamter = true;
                            }
                            if (lexer[pos].Type == TokenType.Operator && lexer[pos].Value == ")" && receiveParamter)
                            {
                                //参数结束
                                receiveParamter = false;
                                break;
                            }
                            pos++;
                        } while ( true );

                        StringBuilder sb = new StringBuilder();
                        foreach (var item in define)
                        {
                            sb.Append(item.Value);
                        }
                        string _b = sb.ToString();
                        FunctionType.Func funcModel = FunctionType.Func.Global;
                        if (_b.Contains(':'))
                        {
                            //成员函数
                            funcModel = FunctionType.Func.Member;
                        }
                        else if (_b.Contains('.'))
                        {
                            //局部函数
                            funcModel = FunctionType.Func.Local;
                        }
                        else
                        {
                            //全局函数
                            funcModel = FunctionType.Func.Global;
                        }

                        List<Token> body = new List<Token>();
                        while (startIndex < len)
                        {
                            body.Add(lexer[startIndex]);
                            if (lexer[startIndex].Type == TokenType.Keyword && lexer[startIndex].Value == "end")
                            {
                                break;
                            }
                            startIndex++;
                        }

                        FunctionType functionType = new FunctionType(funcModel, _b.Replace("function",""), body,attr,paramter);
                        functionType.SetUp();
                        functions.Add(functionType);
                        attr = new List<Token>();
                    }
                    else if (token.Type == TokenType.Keyword && (token.Value == "self"))
                    {
                        List<Token> tokens = new List<Token>();
                        tokens.Add(lexer[pos]);//self

                        pos++;//.
                        tokens.Add(lexer[pos]);

                        pos++;
                        var ldef = lexer[pos];//变量
                        tokens.Add(ldef);

                        pos++;
                        tokens.Add(lexer[pos]);//=

                        pos++;
                        var value = lexer[pos]; //token 开始
                        tokens.Add(lexer[pos]);

                        BaseType baseType = null;
                        if (value.Type == TokenType.Number)
                        {
                            //self number成员
                            baseType = new NumberType(ldef.Value, value.Value, tokens, attr);
                        }
                        else if (value.Type == TokenType.String) 
                        {
                            //self 字符串成员
                            baseType = new StringType(ldef.Value, value.Value, tokens, attr);
                        }
                        else if(value.Type == TokenType.Keyword && bool.TryParse(value.Value,out bool result))
                        {
                            //self 布尔成员
                            baseType = new BoolType(ldef.Value, value.Value, tokens, attr);
                        }
                        else if (value.Type == TokenType.Operator && value.Value == "{")
                        {
                            //self table成员
                            
                            List<Token> table = new List<Token>();
                            int block = 1;
                            table.Add(lexer[pos]);
                            do
                            {
                                pos++;
                                table.Add(lexer[pos]);
                                if (lexer[pos].Type == TokenType.Operator && lexer[pos].Value == "{")
                                {
                                    block++;
                                }
                                if (lexer[pos].Type == TokenType.Operator && lexer[pos].Value == "}")
                                {
                                    block--;
                                }
                                if (block == 0)
                                {
                                    break;
                                }
                            } while (true);
                            StringBuilder sb = new StringBuilder();
                            foreach (var item in table)
                            {
                                sb.Append(item.Value);
                            }
                            tokens.AddRange(table);
                            baseType = new TableType(ldef.Value, sb.ToString(), tokens, attr);
                            //baseType.SetUp();
                            //attr = new List<Token>();
                        }
                        else
                        {
                            throw new Exception("未知类型");
                        }
                        baseType.SetUp();
                        attr = new List<Token>();
                        membersVariable.Add(baseType);

                    }
                    else
                    {
                        pos++;
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                return false;
            }
            return true;
        }
    }
}
