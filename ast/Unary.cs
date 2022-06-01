namespace ElCapo
{
    public class Unary : Expr
    {
        public readonly Token operador;
        public readonly Expr right;

        public Unary(Token operador, Expr right)
        {
            this.operador = operador;
            this.right = right;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitUnaryExpr(this);
        }
        
    }
}
