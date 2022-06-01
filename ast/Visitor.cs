using System;
namespace ElCapo
{
    public interface Visitor<T>
    {
        T VisitAssignExpr(Assign expr);
        T VisitArrayExpr(Array expr);
        T VisitAssignCollection(AssignCollection expr);
        T VisitVariableExpr(Variable expr);
        T VisitBinaryExpr(Binary expr);
        T VisitBinaryIncExpr(BinaryInc expr);
        T VisitCallExpr(Call expr);
        T VisitDictionaryExpr(Dictionary expr);
        T VisitEnumExpr(Enum expr);
        T VisitGetExpr(Get expr);
        T VisitGroupingExpr(Grouping expr);
        T VisitIndexExpr(Index expr);
        T VisitLiteralExpr(Literal expr);
        T VisitLogicalExpr(Logical expr);
        T VisitSelfExpr(Self expr);
        T VisitSetExpr(Set expr);
        T VisitStringFormatExpr(StringFormat expr);
        T VisitSuperExpr(Super expr);
        T VisitUnaryExpr(Unary expr);
    }
}
