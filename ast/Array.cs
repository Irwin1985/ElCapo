using System.Collections.Generic;

namespace ElCapo
{
    public class Array : Expr
    {
        public readonly Token keyword;
        public List<Expr> elements;

        public Array(Token keyword, List<Expr> elements)
        {
            this.keyword = keyword;
            this.elements = elements;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitArrayExpr(this);
        }
    }
}
