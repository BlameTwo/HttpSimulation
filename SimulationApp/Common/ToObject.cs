using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WinUIExtentions.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using CommunityToolkit.Mvvm.ComponentModel;
    using HttpSimulation.Models;

    public static class TransExp<TIn, TOut>
        where TIn : ObservableObject, InterfaceType
        where TOut : ObservableObject, InterfaceType
    {
        private static readonly Func<TIn, TOut> cache = GetFunc();

        private static Func<TIn, TOut> GetFunc()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");

            var bindings = typeof(TOut)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(pi =>
                    pi.CanWrite && pi.GetCustomAttribute<ExcludeFromCodeCoverageAttribute>() == null
                )
                .Select(pi =>
                {
                    PropertyInfo sourceProperty = typeof(TIn).GetProperty(
                        pi.Name,
                        BindingFlags.Public | BindingFlags.Instance
                    );
                    if (sourceProperty != null && sourceProperty.CanRead)
                    {
                        return Expression.Bind(
                            pi,
                            Expression.Property(parameterExpression, sourceProperty)
                        );
                    }
                    return null;
                })
                .Where(b => b != null);

            MemberInitExpression memberInitExpression = Expression.MemberInit(
                Expression.New(typeof(TOut)),
                bindings
            );

            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(
                memberInitExpression,
                parameterExpression
            );

            return lambda.Compile();
        }

        [return: NotNullIfNotNull("tIn")]
        public static TOut Trans(TIn tIn)
        {
            TOut clone = cache(tIn);
            DeepCloneProperties(tIn, clone);
            return clone;
        }

        private static void DeepCloneProperties(TIn source, TOut target)
        {
            var properties = typeof(TIn)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(pi => pi.CanRead && pi.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source);
                if (value is IEnumerable<object> enumerable)
                {
                    // 如果是集合类型，进行深拷贝
                    var elementType = enumerable.GetType().GetGenericArguments()[0];
                    var listType = typeof(List<>).MakeGenericType(elementType);
                    var newList = (IList<object>)Activator.CreateInstance(listType)!;
                    foreach (var item in enumerable)
                    {
                        var newItem = item as InterfaceType;
                        if (newItem != null)
                        {
                            var newItemClone = newItem.Clone();
                            newList.Add(newItemClone);
                        }
                        else
                        {
                            newList.Add(item);
                        }
                    }
                    prop.SetValue(target, newList);
                }
                else
                {
                    // 对于非集合类型，直接复制值
                    prop.SetValue(target, value);
                }
            }
        }
    }

    // 用于排除代码覆盖的属性
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcludeFromCodeCoverageAttribute : Attribute { }
}
