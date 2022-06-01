using System.Collections.Generic;

namespace ElCapo
{
    public class Call : Expr
    {
        public readonly Expr callee;
        public readonly Token paren;
        public readonly List<Expr> arguments;
        
        public Call(Expr callee, Token paren, List<Expr> arguments)
        {
            this.callee = callee;
            this.paren = paren;
            this.arguments = arguments;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitCallExpr(this);
        }
    }
}
