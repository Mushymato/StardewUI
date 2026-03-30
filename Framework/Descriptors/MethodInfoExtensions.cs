using System.Linq.Expressions;
using System.Reflection;

namespace StardewUI.Framework.Descriptors;

internal static class MethodInfoExtensions
{
    public static T SafeCreateDelegate<T>(this MethodInfo method)
        where T : Delegate
    {
        if (
            method.IsStatic
            || method.DeclaringType is null
            || (!method.DeclaringType.IsValueType && method.DeclaringType != typeof(ValueType))
        )
        {
            return method.CreateDelegate<T>();
        }
        // Compiling an expression here is substantially slower than MethodInfo.CreateDelegate, but it is the only
        // mechanism (other than IL emit, which is even worse) that seems to work correctly for value types.
        var instanceType = method.DeclaringType == typeof(ValueType) ? method.ReflectedType! : method.DeclaringType;
        var instanceParam = Expression.Parameter(instanceType, "instance");
        var parameters = method.GetParameters();
        var arguments = new Expression[parameters.Length];
        var argumentsWithInstance = new ParameterExpression[parameters.Length + 1];
        argumentsWithInstance[0] = instanceParam;
        for (int i = 0; i < parameters.Length; i++)
        {
            arguments[i] = argumentsWithInstance[i + 1] = Expression.Parameter(parameters[i].ParameterType, "arg" + i);
        }
        return Expression.Lambda<T>(Expression.Call(instanceParam, method, arguments), argumentsWithInstance).Compile();
    }
}
