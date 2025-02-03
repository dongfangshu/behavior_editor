using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaAnalyzer
{
    public enum TokenType
    {
        Keyword,//关键字
        Identifier,//标识符
        Number,//数字
        String,//字符
        Operator,//运算符
        Punctuation,//标点符号
        Comment,//注释
        Whitespace,//空格
        Unknown,//未知
        EmmyLuaComment//注解
    }
}
