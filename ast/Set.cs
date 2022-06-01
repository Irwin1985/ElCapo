namespace ElCapo
{
    public class Set : Expr
    {
        public readonly Expr owner;
        public readonly Token name;
        public readonly Expr value;

        public Set(Expr owner, Token name, Expr value)
        {
            this.owner = owner;
            this.name = name;
            this.value = value;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitSetExpr(this);
        }
    }
}
