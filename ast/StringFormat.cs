using System.Collections.Generic;

namespace ElCapo
{
    public class StringFormat : Expr
    {
        public readonly string source;
        public readonly List<Expr> variables;
        
        public StringFormat(string source, List<Expr> variables)
        {
            this.source = source;
            this.variables = variables;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitStringFormatExpr(this);
        }
    }
}
