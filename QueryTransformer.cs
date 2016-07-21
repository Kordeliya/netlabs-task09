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
            Expression newExpression = null;

            if (node.NodeType == ExpressionType.Add)
            {
                bool isRightTypeLeft = node.Left is ConstantExpression || node.Left is ParameterExpression;
                bool isRightTypeRight = node.Right is ConstantExpression || node.Right is ParameterExpression;

                if (!isRightTypeLeft || !isRightTypeRight)
                    newExpression = base.VisitBinary(node);

                ConstantExpression constant = null;
                ParameterExpression param = null;
                if (node.Left is ConstantExpression)
                    constant = (ConstantExpression)node.Left;
                else if (node.Right is ConstantExpression)
                    constant = (ConstantExpression)node.Right;
                if (node.Left is ParameterExpression)
                    param = (ParameterExpression)node.Left;
                else if (node.Right is ParameterExpression)
                    param = (ParameterExpression)node.Right;

                if ((int)constant.Value == 1 && param != null)
                {
                    return Expression.PreIncrementAssign(param);
                }
            }
            else
                newExpression = base.VisitBinary(node);

            return newExpression;

        }
    }
}
