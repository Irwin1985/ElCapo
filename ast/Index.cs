namespace ElCapo
{
    public class Index : Expr
    {
        public readonly Token keyword;
        public readonly Expr left; // owner
        public readonly Expr index; // the collection index e.g: persons[index]

        public Index(Token keyword, Expr left, Expr index)
        {
            this.keyword = keyword;
            this.left = left;
            this.index = index;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitIndexExpr(this);
        }
    }
}
