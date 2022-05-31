using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElCapo
{
    public class Lexer
    {
        private readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>();
        private readonly string input;
		private int curPos;
		private int peekPos;
		private int line;
		private int col;
		private int beginTokenCol;
		private char c;
		private TokenType tokAnt; // last recognised token type.
        public Lexer(string input)
        {
            FillKeywords();
            this.input = input;
			line = 1;
			Advance();
        }
        private void FillKeywords()
        {
			keywords.Add("as", TokenType.As);
			keywords.Add("and", TokenType.And);
			keywords.Add("break", TokenType.Break);
			keywords.Add("continue", TokenType.Continue);
			keywords.Add("class", TokenType.Class);
			keywords.Add("else", TokenType.Else);
			keywords.Add("false", TokenType.False);
			keywords.Add("def", TokenType.Def);
			keywords.Add("defer", TokenType.Defer);
			keywords.Add("for", TokenType.For);
			keywords.Add("from", TokenType.From);
			keywords.Add("import", TokenType.Import);
			keywords.Add("if", TokenType.If);
			keywords.Add("in", TokenType.In);
			keywords.Add("null", TokenType.Null);
			keywords.Add("or", TokenType.Or);
			keywords.Add("pass", TokenType.Pass);
			// keywords.Add("print", TokenType.Print);
			keywords.Add("return", TokenType.Return);
			keywords.Add("super", TokenType.Super);
			keywords.Add("self", TokenType.Self);
			keywords.Add("true", TokenType.True);
			keywords.Add("let", TokenType.Let);
			keywords.Add("while", TokenType.While);
		}
		public Token NextToken()
        {
			while (c != '\0')
            {
				beginTokenCol = col;
				if (IsSpace(c))
                {
					SkipWhitespace();
					continue;
                }
				if (c == '#')
                {
					SkipSingleComment();
					continue;
                }
				if (IsMultipleComment())
                {
					SkipMultipleComment();
					continue;
                }
				if (IsDigit(c)) return ReadNumber();

				// raw string (without escaping characters)
				if (c == 'r' && IsString(Peek())) return ReadRawString();
				// format string
				if (c == 'f' && IsString(Peek()))
                {
					Advance();
					return ReadString(TokenType.StringFormat);
                }
				if (IsLetter(c)) return ReadIdentifier();

				if (IsString(c)) return ReadString(TokenType.String);

				// single character
				if (c == '(')
                {
					Advance(); return NewToken(TokenType.LeftParen);
                }
				if (c == ')')
                {
					Advance(); return NewToken(TokenType.RightParen);
                }
				if (c == '{')
				{
					Advance(); return NewToken(TokenType.LeftBrace);
				}
				if (c == '}')
				{
					Advance(); return NewToken(TokenType.RightBrace);
				}
				if (c == '[')
				{
					Advance(); return NewToken(TokenType.LeftBracket);
				}
				if (c == ']')
				{
					Advance(); return NewToken(TokenType.RightBracket);
				}
				if (c == ',')
				{
					Advance(); return NewToken(TokenType.Comma);
				}
				if (c == ';')
				{
					Advance(); return NewToken(TokenType.Semicolon);
				}
				if (c == '\\')
                {
					Advance();
					SkipBackSlash();
					continue;
                }
				if (c == ':')
                {
					Advance(); return NewToken(TokenType.Colon);
                }
				if (c == '.')
                {
					if (Peek() == '.' && PeekNext() == '.')
                    {
						Advance(); Advance(); Advance();
						return NewToken(TokenType.Ellipsis);
                    }
					Advance(); return NewToken(TokenType.Dot);
                }
				if (c == '%')
				{
					Advance(); return NewToken(TokenType.Modulo);
				}
				if (c == '^')
				{
					Advance(); return NewToken(TokenType.Power);
				}
				// double characters
				if (c == '+')
				{
					if (Peek() == '=')
					{
						Advance(); Advance(); return NewToken(TokenType.PlusEqual);
					}
					Advance(); return NewToken(TokenType.Plus);
				}
				if (c == '-')
				{
					if (Peek() == '=')
					{
						Advance(); Advance(); return NewToken(TokenType.MinusEqual);
					}
					Advance(); return NewToken(TokenType.Minus);
				}
				if (c == '*')
				{
					if (Peek() == '=')
					{
						Advance(); Advance(); return NewToken(TokenType.StarEqual);
					}
					Advance(); return NewToken(TokenType.Star);
				}
				if (c == '/')
				{
					if (Peek() == '=')
					{
						Advance(); Advance(); return NewToken(TokenType.SlashEqual);
					}
					Advance(); return NewToken(TokenType.Slash);
				}
				if (c == '=')
				{
					if (Peek() == '=')
					{
						Advance(); Advance(); return NewToken(TokenType.EqualEqual);
					}
					Advance(); return NewToken(TokenType.Equal);
				}
				if (c == '!')
				{
					if (Peek() == '=')
					{
						Advance(); Advance(); return NewToken(TokenType.BangEqual);
					}
					Advance(); return NewToken(TokenType.Bang);
				}
				if (c == '<')
				{
					if (Peek() == '=')
					{
						Advance(); Advance(); return NewToken(TokenType.LessEqual);
					}
					Advance();
					return NewToken(TokenType.Less);
				}
				if (c == '>')
				{
					if (Peek() == '=')
					{
						Advance(); Advance();
						return NewToken(TokenType.GreaterEqual);
					}
					Advance();
					return NewToken(TokenType.Greater);
				}
				// check for new lines
				if (c == '\n')
				{
					Advance();
					if (tokAnt != TokenType.Semicolon && tokAnt != TokenType.Colon)
					{
						return NewToken(TokenType.Semicolon);
					}
					else
					{
						continue;
					}
				}
				// illegal character
				Token tokIllegal = NewToken(TokenType.Illegal, c.ToString());
				Advance();
				return tokIllegal;

			}
			return NewToken(TokenType.Eof);
        }

		Token ReadNumber()
        {
			StringBuilder builder = new StringBuilder();
			while (IsDigit(c) || c == '_')
            {
				if (c != '_') builder.Append(c);
				Advance();
            }
			// check for '.' seperator
			if (c == '.')
            {
				builder.Append(c);
				Advance(); // skip '.' separator
				while (IsDigit(c))
                {
					builder.Append(c);
					Advance();
                }
            }
			string lexeme = builder.ToString();
			return NewToken(TokenType.Number, lexeme);
        }

		Token ReadIdentifier()
        {
			int left = curPos;
			while (IsIdentifier(c))
            {
				Advance();
            }
			int right = curPos;
			string lexeme = input.Substring(left, right-left);
			TokenType type = TokenType.Identifier;
			if (keywords.ContainsKey(lexeme))
            {
				type = keywords[lexeme];
            }
			return NewToken(type, lexeme);
        }

		Token ReadString(TokenType type)
        {
			char strEnd = c;
			bool scanningStringFinished = false;
			StringBuilder lexeme = new StringBuilder();
			Advance();

			while (c != '\0')
            {
				if (c == '\\')
                {
					Advance();
					switch (c)
                    {
					case '\\':
						lexeme.Append('\\'); break;
					case 't':
						lexeme.Append('\t'); break;
					case 'r':
						lexeme.Append('\r'); break;
					case 'n':
						lexeme.Append('\n'); break;
					case '\'':
						lexeme.Append('\''); break;
					case '"':
						lexeme.Append('"'); break;
					default:
						Error.ShowError(line, col, "Invalid escape character sequence.", true);
						break;
                    }
					Advance();
                } else
                {
					if (c == strEnd)
                    {
						Advance();
						scanningStringFinished = true;
						break;
                    } else
                    {
						lexeme.Append(c);
						Advance();
                    }
                }
            }
			if (!scanningStringFinished)
				Error.ShowError(line, col, "unterminated string.", true);

			return NewToken(type, lexeme.ToString());
        }

		Token ReadRawString()
        {
			Advance(); // eat the 'r' letter
			char strEnd = c;
			Advance(); // eat the string delimiter
			int startPos = curPos;
			while (c != strEnd) Advance();
			if (c == '\0')
				Error.ShowError(line, col, "unterminated string.", true);
			int endPos = curPos;
			Advance(); // skip closing string
			string lexeme = input.Substring(startPos, endPos-startPos);

			return NewToken(TokenType.String, lexeme);
        }

		void Advance()
        {
			if (c == '\n')
            {
				line += 1;
				col = 0;
            }
			if (peekPos >= input.Length)
            {
				c = '\0';
            } else
            {
				c = input[peekPos];
				curPos = peekPos;
				peekPos += 1;
				col += 1;
            }
        }

		char Peek()
        {
			if (peekPos >= input.Length) return '\0';
			return input[peekPos];
        }

		char PeekNext()
        {
			int pos = peekPos + 1;
			if (pos >= input.Length) return '\0';
			return input[pos];
        }

		void SkipWhitespace()
        {
			while (c != '\0' && IsSpace(c))
            {
				Advance();
            }
        }

		bool IsSpace(char c)
        {
			return c == ' ' || c == '\t' || c == '\r';
        }

		bool IsMultipleComment()
        {
			return c == '"' && Peek() == '"' && PeekNext() == '"';
        }

		bool IsDigit(char c)
        {
			return '0' <= c && c <= '9';
        }

		bool IsLetter(char c)
        {
			return 'a' <= c && c <= 'z' || 'A' <= c && c <= 'Z';
        }

		bool IsIdentifier(char c)
        {
			return IsLetter(c) || IsDigit(c) || c == '_';
        }

		bool IsString(char c)
        {
			return c == '"' || c == '\'';
        }

		// skip comments like: # this is a single line of comment.
		void SkipSingleComment()
        {
			while (c != '\0' && c != '\n')
				Advance();
        }

		void SkipMultipleComment()
        {
			Advance(); Advance(); Advance(); // skip the begining """
			while (c != '\0' && !IsMultipleComment()) Advance();
			if (!IsMultipleComment())
				Error.ShowError(line, col, "Unexpected end of file.", true);
            
			Advance(); Advance(); Advance(); // skip the final """
        }

		void SkipBackSlash()
        {
			SkipWhitespace();
			if (c != '\n')
				Error.ShowError(line, col, "Invalid use of the `\\` character.", true);

			Advance();
		}

		Token NewToken(TokenType type)
        {
			tokAnt = type;
			return new Token(type, line, beginTokenCol);
        }

		Token NewToken(TokenType type, string lexeme)
        {
			tokAnt = type;
			// convert from String to Double (NUMBER token type)
			object value = lexeme;
			if (type == TokenType.Number)
            {
				value = double.Parse(lexeme);
            }
			return new Token(type, lexeme, value, line, beginTokenCol);
        }

	}
}
