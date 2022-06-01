namespace ElCapo
{
    public class Grouping : Expr
    {
        public readonly Expr expression;
        public Grouping(Expr expression)
        {
            this.expression = expression;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitGroupingExpr(this);
        }
    }
}
