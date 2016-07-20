using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Zueva_Natalia_Expression
{
    /// <summary>
    /// Класс для трансформации
    /// </summary>
    public class QueryTransformer : ExpressionVisitor
    {
        /// <summary>
        /// При просмотре бинарного выражения
        /// </summary>
        /// <param name="node">выражение</param>
        /// <returns>измененное выражение</returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Expression newLeft = node.Left;
            Expression newRight = node.Right;

            bool IsRightTypeLeft = node.Left is ConstantExpression || node.Left is ParameterExpression;
            bool IsRightTypeRight = node.Right is ConstantExpression || node.Right is ParameterExpression;
            if (!IsRightTypeLeft)
                newLeft = Visit(node.Left);
            if (!IsRightTypeRight)
                newRight = Visit(node.Right);


            if (node.NodeType == ExpressionType.Add)
            {
                ConstantExpression constant = null;
                ParameterExpression param = null;
                if (newLeft is ConstantExpression)
                    constant = (ConstantExpression)newLeft;
                else if (newRight is ConstantExpression)
                    constant = (ConstantExpression)newRight;
                if (newLeft is ParameterExpression)
                    param = (ParameterExpression)newLeft;
                else if (newRight is ParameterExpression)
                    param = (ParameterExpression)newRight;

                if ((int)constant.Value == 1 && param != null)
                {
                    return Expression.PreIncrementAssign(param);
                }
            }

            return node.Update(newLeft, null, newRight);

        }
    }
}
