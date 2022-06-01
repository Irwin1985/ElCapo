namespace ElCapo
{
    public class Get : Expr
    {
        public readonly Expr owner; // the owner of the property e.g: person
        public readonly Token name; // the property name e.g: person.age where are is the prop.
        
        public Get(Expr owner, Token name)
        {
            this.owner = owner;
            this.name = name;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitGetExpr(this);
        }
    }
}
