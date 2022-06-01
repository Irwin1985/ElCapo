namespace ElCapo
{
    public class Variable : Expr
    {
        public readonly Token name;

        public Variable(Token name)
        {
            this.name = name;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitVariableExpr(this);
        }
    }
}
