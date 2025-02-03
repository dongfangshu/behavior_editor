using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaAnalyzer
{
    internal class LuaLexer
    {
        // Lua 关键字列表 严格意义上self不属于关键字
        private static readonly HashSet<string> Keywords = new HashSet<string>
    {
        "and", "break", "do", "else", "elseif", "end", "false", "for", "function",
        "if", "in", "local", "nil", "not", "or", "repeat", "return", "then", "true",
        "until", "while","self"
    };
        // Lua 运算符列表
        private static readonly HashSet<string> Operators = new HashSet<string>
    {
        "+", "-", "*", "/", "%", "^", "#", "==", "~=", "<=", ">=", "<", ">", "=",
        "(", ")", "{", "}", "[", "]", ";", ":", ",", ".", "..", "..."
    };
        // 判断字符是否是空白字符
        private static bool IsWhitespace(char c)
        {
            return char.IsWhiteSpace(c);
        }

        // 判断字符是否是数字
        private static bool IsDigit(char c)
        {
            return char.IsDigit(c);
        }

        // 判断字符是否是字母或下划线
        private static bool IsLetterOrUnderscore(char c)
        {
            return char.IsLetter(c) || c == '_';
        }

        // 判断字符串是否是关键字
        private static bool IsKeyword(string word)
        {
            return Keywords.Contains(word);
        }

        // 判断字符是否是运算符
        private static bool IsOperator(char c)
        {
            return Operators.Contains(c.ToString());
        }
        // 词法分析主函数
        public static List<Token> Lex(string input)
        {
            List<Token> tokens = new List<Token>();
            int pos = 0;
            int len = input.Length;

            while (pos < len)
            {
                char currentChar = input[pos];

                // 跳过空白字符
                if (IsWhitespace(currentChar))
                {
                    pos++;
                }
                // 处理 EmmyLua 注释（以 ---@ 开头）
                else if (currentChar == '-' && pos + 3 < len && input[pos + 1] == '-' && input[pos + 2] == '-' && input[pos + 3] == '@')
                {
                    pos += 4; // 跳过 '---'
                    StringBuilder commentBuilder = new StringBuilder();
                    while (pos < len && input[pos] != '\n')
                    {
                        commentBuilder.Append(input[pos]);
                        pos++;
                    }
                    tokens.Add(new Token(TokenType.EmmyLuaComment, commentBuilder.ToString()));
                }
                // 处理单行注释和多行注释
                else if (currentChar == '-' && pos + 1 < len && input[pos + 1] == '-')
                {
                    pos += 2; // 跳过 '--'
                    if (pos < len && input[pos] == '[') // 检查是否是多行注释
                    {
                        pos++;
                        if (pos < len && input[pos] == '[') // 确认是多行注释
                        {
                            pos++;
                            StringBuilder commentBuilder = new StringBuilder();
                            while (pos + 1 < len && !(input[pos] == ']' && input[pos + 1] == ']'))
                            {
                                commentBuilder.Append(input[pos]);
                                pos++;
                            }
                            pos += 2; // 跳过 ']]'
                            tokens.Add(new Token(TokenType.Comment, commentBuilder.ToString()));
                        }
                        else // 单行注释
                        {
                            StringBuilder commentBuilder = new StringBuilder();
                            while (pos < len && input[pos] != '\n')
                            {
                                commentBuilder.Append(input[pos]);
                                pos++;
                            }
                            tokens.Add(new Token(TokenType.Comment, commentBuilder.ToString()));
                        }
                    }
                    else // 单行注释
                    {
                        StringBuilder commentBuilder = new StringBuilder();
                        while (pos < len && input[pos] != '\n')
                        {
                            commentBuilder.Append(input[pos]);
                            pos++;
                        }
                        tokens.Add(new Token(TokenType.Comment, commentBuilder.ToString()));
                    }
                }
                // 处理数字
                else if (IsDigit(currentChar))
                {
                    StringBuilder numBuilder = new StringBuilder();
                    while (pos < len && IsDigit(input[pos]))
                    {
                        numBuilder.Append(input[pos]);
                        pos++;
                    }
                    tokens.Add(new Token(TokenType.Number, numBuilder.ToString()));
                }
                // 处理标识符和关键字
                else if (IsLetterOrUnderscore(currentChar))
                {
                    StringBuilder identBuilder = new StringBuilder();
                    while (pos < len && (IsLetterOrUnderscore(input[pos]) || IsDigit(input[pos])))
                    {
                        identBuilder.Append(input[pos]);
                        pos++;
                    }
                    string ident = identBuilder.ToString();
                    if (IsKeyword(ident))
                    {
                        tokens.Add(new Token(TokenType.Keyword, ident));
                    }
                    else
                    {
                        tokens.Add(new Token(TokenType.Identifier, ident));
                    }
                }
                // 处理字符串
                else if (currentChar == '"' || currentChar == '\'')
                {
                    char quote = currentChar;
                    StringBuilder strBuilder = new StringBuilder();
                    pos++; // 跳过开头的引号
                    while (pos < len && input[pos] != quote)
                    {
                        strBuilder.Append(input[pos]);
                        pos++;
                    }
                    pos++; // 跳过结尾的引号
                    tokens.Add(new Token(TokenType.String, strBuilder.ToString()));
                }
                // 处理运算符
                else if (IsOperator(currentChar))
                {
                    tokens.Add(new Token(TokenType.Operator, currentChar.ToString()));
                    pos++;
                }
                // 处理未知字符
                else
                {
                    tokens.Add(new Token(TokenType.Unknown, currentChar.ToString()));
                    pos++;
                }
            }

            return tokens;
        }

    }
}
