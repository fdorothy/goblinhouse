using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Retroverse
{
    public enum TokenType
    {
        TEXT, NEWLINE
    }

    public class Token
    {
        public Token(string text = "")
        {
            this.text = text;
        }
        public TokenType type;
        public string text;
    }

    public class Parser
    {
        int index = 0;
        public Story story;
        public string text;
        public List<Token> tokens;

        public Parser(string text)
        {
            story = new Story();
            this.text = text;
        }

        public void Tokenize()
        {
            index = 0;
            tokens = new List<Token>();
            Token currentToken = null;
            foreach (char c in text)
            {
                if (!IsWhitespace(c))
                {
                    if (currentToken == null)
                    {
                        currentToken = new Token();
                    }
                    currentToken.text += c;
                }
                else
                {
                    if (currentToken != null)
                    {
                        tokens.Add(currentToken);
                        currentToken = null;
                    }
                    if (c == '\n')
                    {
                        Token t = new Token();
                        t.type = TokenType.NEWLINE;
                        tokens.Add(t);
                    }
                }
            }
        }

        public void BuildStory()
        {
            story = new Story();
            Conversation c = new Conversation();
            story.AddSection("_default", c);
            index = 0;
            Token token = NextToken();
            bool startOfLine = true;
            while (token != null)
            {
                // check for a new section
                if (startOfLine)
                {
                    Match match = Regex.Match(token.text, @"^\\.(\w+)");
                    if (match.Success)
                    {
                        c = new Conversation();
                        story.AddSection(match.Captures[0].Value, c);
                    }
                    if (token.text == "->")
                    {
                        token = NextToken();
                        if (token != null)
                        {
                            c.Goto(token.text);
                        }
                        else
                        {
                            throw new System.Exception("Parse error: missing label in goto statement");
                        }
                    }
                    token = NextToken();
                }
                else if (token.type != TokenType.NEWLINE)
                {
                    // slurp the rest of the tokens on the line
                    string text = token.text;
                    token = NextToken();
                    while (token != null && token.type != TokenType.NEWLINE)
                    {
                        text += " " + token.text;
                        token = NextToken();
                    }
                    c.Line(text);
                    token = NextToken();
                }
            }
        }

        public Token NextToken()
        {
            if (index < tokens.Count)
                return tokens[index++];
            else
                return null;
        }

        public bool IsWhitespace(char c)
        {
            return c == ' ' || c == '\r' || c == '\n' || c == '\t';
        }

        public char NextCharacter()
        {
            return text[index];
        }

        public string TokensToString()
        {
            return String.Join(", ", tokens.Select(token => "(" + token.type.ToString() + ":" + token.text + ")"));
        }
    }
}
