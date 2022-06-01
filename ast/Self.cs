namespace ElCapo
{
    public class Self : Expr
    {
        public readonly Token keyword;
        public Self(Token keyword)
        {
            this.keyword = keyword;
        }
        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitSelfExpr(this);
        }
    }
}
