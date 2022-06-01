namespace ElCapo
{
    public abstract class Expr
    {
        public abstract T Accept<T>(Visitor<T> visitor);
    }
}
