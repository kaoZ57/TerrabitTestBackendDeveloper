using System.Linq.Expressions;
using System.Reflection;

namespace TerrabitTest.Core
{
    public static class ExpressionExtension
    {
        public static IQueryable<TResult> ProjectTo<TSource, TResult>(this IQueryable<TSource> source) where TResult : class
        {
            // สร้าง ParameterExpression สำหรับอ้างอิงไปที่แหล่งข้อมูล TSource
            var parameter = Expression.Parameter(typeof(TSource), "x");

            // สร้าง MemberInitExpression เพื่อกำหนดแผลผลลัพธ์ที่ต้องการ
            var memberInit = Expression.MemberInit(
                Expression.New(typeof(TResult)),
                CreateBindings<TSource, TResult>(parameter));

            // สร้าง Lambda Expression สำหรับ Projection
            var lambda = Expression.Lambda<Func<TSource, TResult>>(memberInit, parameter);

            // นำ Lambda Expression มาใช้ใน IQueryable<TSource>
            return source.Select(lambda);
        }

        private static MemberBinding[] CreateBindings<TSource, TResult>(Expression parameter)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TResult);

            // สร้าง MemberBinding สำหรับแม็พแต่ละ Property ใน TResult
            var bindings = destinationType.GetProperties().Select(property =>
            {
                var sourceProperty = sourceType.GetProperty(property.Name);
                //if (matchingProperty != null && !matchingProperty.GetGetMethod().IsVirtual && !matchingProperty.GetGetMethod().IsFinal))

                if (sourceProperty != null && !sourceProperty.GetGetMethod().IsVirtual && !sourceProperty.GetGetMethod().IsFinal)
                {
                    var sourceExpression = Expression.Property(parameter, sourceProperty);
                    var convertedExpression = Expression.Convert(sourceExpression, property.PropertyType);
                    return Expression.Bind(property, convertedExpression);
                }
                else
                {
                    return null;
                }
            }).Where(binding => binding != null);

            return bindings.ToArray();
        }

    }
}
