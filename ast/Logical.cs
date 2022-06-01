namespace ElCapo
{
    public class Logical : Expr
    {
        public readonly Expr left;
        public readonly Token operador;
        public readonly Expr right;
        public Logical(Expr left, Token operador, Expr right)
        {
            this.left = left;
            this.operador = operador;
            this.right = right;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitLogicalExpr(this);
        }
    }
}
