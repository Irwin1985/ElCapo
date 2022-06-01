namespace ElCapo
{
    public class AssignCollection : Expr
    {
        public readonly Token keyword;
        public readonly Variable left;
        public readonly Expr index;
        public readonly Expr value;

        public AssignCollection(Token keyword, Variable left, Expr index, Expr value)
        {
            this.keyword = keyword;
            this.left = left;
            this.index = index;
            this.value = value;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitAssignCollection(this);
        }
    }
}
