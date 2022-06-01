using System.Collections.Generic;

namespace ElCapo
{
    public class Dictionary : Expr
    {
        public readonly Token keyword;
        public readonly Dictionary<Expr, Expr> elements;

        public Dictionary(Token keyword, Dictionary<Expr, Expr> elements)
        {
            this.keyword = keyword;
            this.elements = elements;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitDictionaryExpr(this);
        }
    }
}
