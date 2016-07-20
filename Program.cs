using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Zueva_Natalia_Expression
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Замена x+1 значения на инкремент
                Console.WriteLine("Замена x+1 значения на инкремент");
                Expression<Func<int, int, double>> expForVisit = (x, y) => (double)(x + 1) * (1 + y) / 2;
                Console.WriteLine(expForVisit.ToString());
                QueryTransformer transformer = new QueryTransformer();
                Expression newexp = transformer.VisitAndConvert<Expression>(expForVisit, "TEST");
                Console.WriteLine(newexp.ToString());
                Console.WriteLine();


                //составление формулы с помощью Expression выражений
                ParameterExpression xParam = Expression.Parameter(typeof(int), "x");
                ParameterExpression yParam = Expression.Parameter(typeof(int), "y");
                ConstantExpression constant = Expression.Constant(2.0);

                Expression sum = Expression.Add(xParam, yParam);
                sum = Expression.Convert(sum, typeof(System.Double));
                Expression divide = Expression.Divide(sum, constant);
                LambdaExpression lambdaExpression = Expression.Lambda(divide, xParam, yParam);
                var newLambda = (Func<int, int, double>)lambdaExpression.Compile();
                Console.WriteLine("составление формулы с помощью Expression выражений. {0}", lambdaExpression.ToString());
                Console.WriteLine("Результат: newLambda(10, 1)= {0}", newLambda(10, 1));
                Console.WriteLine();


                //составление Expression с помощью лямбда выражения
                Expression<Func<double, double, double>> exp = (x, y) => (x + y) / 2.0;
                var del = exp.Compile();
                Console.WriteLine("составление Expression с помощью лямбда выржанения. {0}", exp.ToString());
                Console.WriteLine("Результат: del(10, 1) = {0}", del(10, 1));
                Console.WriteLine();
            }
            catch(Exception)
            {
                Console.WriteLine("Произошла ошибка");
            }
            Console.ReadKey();
        }
    }


   
}
