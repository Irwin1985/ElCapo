using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElCapo
{
    public enum TokenType
    {
        // Operators
        Plus,
        PlusEqual,
        Minus,
        MinusEqual,
        Star,
        StarEqual,
        Slash,
        SlashEqual,
        Modulo,
        Power,
        Bang,
        BangEqual,
        Equal,
        EqualEqual,
        Greater,
        GreaterEqual,
        Less,
        LessEqual,

        // Symbols
        LeftParen,
        RightParen,
        LeftBrace,
        RightBrace,
        LeftBracket,
        RightBracket,
        Comma,
        Colon,
        Dot,
        Ellipsis,
        Semicolon,

        // Keywords
        As,
        And,
        Break,
        Continue,
        Class,
        Else,
        Enum,
        False,
        Def,
        Defer,
        For,
        Format,
        From,
        Import,
        If,
        In,
        Null,
        Or,
        Pass,
        Return,
        Super,
        Self,
        True,
        Let,
        While,

        // Identifiers and literals
        Identifier,
        String,
        StringFormat,
        Number,

        // Others
        Illegal,
        Eof
    }
    public class Token
    {
        public readonly string lexeme;
        public readonly int line;
        public readonly int col;
        public readonly TokenType type;
        public readonly object literal;

        public Token(TokenType type, string lexeme, object literal, int line, int col)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.literal = literal;
            this.line = line;
            this.col = col;
        }

        public Token(TokenType type, int line, int col)
        {
            this.type = type;
            this.line = line;
            this.col = col;
        }

        public override string ToString()
        {
            return $"Token({type}, '{literal}')";
        }
    }    
}
