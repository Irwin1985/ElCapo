namespace ElCapo
{
    public class Binary : Expr
    {
        public readonly Expr left;
        public readonly Token operador;
        public readonly Expr right;
        public Binary(Expr left, Token operador, Expr right)
        {
            this.left = left;
            this.operador = operador;
            this.right = right;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitBinaryExpr(this);
        }
    }
}
