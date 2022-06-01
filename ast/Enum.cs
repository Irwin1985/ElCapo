using System.Collections.Generic;

namespace ElCapo
{
    public class Enum : Expr
    {
        public readonly Token keyword;
        public readonly Dictionary<string, int> elements;

        public Enum(Token keyword, Dictionary<string, int> elements)
        {
            this.keyword = keyword;
            this.elements = elements;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitEnumExpr(this);
        }
    }
}
