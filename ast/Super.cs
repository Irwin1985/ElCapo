namespace ElCapo
{
    public class Super : Expr
    {
        public readonly Token keyword;
        public readonly Token method;

        public Super(Token keyword, Token method)
        {
            this.keyword = keyword;
            this.method = method;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitSuperExpr(this);
        }
    }
}
