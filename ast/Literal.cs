namespace ElCapo
{
    public class Literal : Expr
    {
        public readonly object value;

        public Literal(object value)
        {
            this.value = value;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitLiteralExpr(this);
        }
    }
}
