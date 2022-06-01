namespace ElCapo
{
    public class Assign : Expr
    {
        public readonly Token name;
        public Expr value;

        public Assign(Token name, Expr value)
        {
            this.name = name;
            this.value = value;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitAssignExpr(this);
        }
    }
}
