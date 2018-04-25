// Author(s): Paul Calande
// Helpful functions that work across many different types.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq.Expressions;

public static class UtilGeneric
{
    // Returns true if the two given variables of the same type are equal.
    public static bool IsEqualTo<T>(T first, T second)
    {
        return EqualityComparer<T>.Default.Equals(first, second);
    }

    // Returns true if the first variable is greater than the second variable.
    public static bool IsGreaterThan<T>(T first, T second)
        where T : IComparable<T>
    {
        return first.CompareTo(second) > 0;
    }

    // Returns true if the first variable is less than the second variable.
    public static bool IsLessThan<T>(T first, T second)
        where T : IComparable<T>
    {
        return first.CompareTo(second) < 0;
    }

    // Returns true if the first variable is greater than or equal to the second variable.
    public static bool IsGreaterThanOrEqualTo<T>(T first, T second)
        where T : IComparable<T>
    {
        return !IsLessThan(first, second);
    }

    // Returns true if the first variable is less than or equal to the second variable.
    public static bool IsLessThanOrEqualTo<T>(T first, T second)
        where T : IComparable<T>
    {
        return !IsGreaterThan(first, second);
    }

    // Returns true if the variable is positive.
    public static bool IsPositive<T>(T variable)
        where T : IComparable<T>
    {
        return variable.CompareTo(default(T)) > 0;
    }

    // Returns true if the variable is negative.
    public static bool IsNegative<T>(T variable)
        where T : IComparable<T>
    {
        return variable.CompareTo(default(T)) < 0;
    }

    // Returns true if the variable equals zero.
    public static bool IsZero<T>(T variable)
        where T : struct, IComparable<T>
    {
        return variable.CompareTo(default(T)) == 0;
    }

    // A static collection of operations associated with a particular type T.
    // Operations that are not supported by type T will throw exceptions when executed.
    static class Operations<T>
    {
        public static readonly Func<T, T> Negate;

        // The static constructor populates all of the operations.
        // This is useful because each operation only has to be compiled once,
        // thanks to the operations existing statically.
        static Operations()
        {
            Negate = Create<T, T>(Expression.Negate);
        }

        // Returns a generic function delegate representing a given unary operation.
        // The unary operation is passed as the argument in expression form.
        // The first type parameter is the argument type.
        // The second type parameter is the return type.
        static Func<TArg, TReturn> Create<TArg, TReturn>
            (Func<Expression, UnaryExpression> expression)
        {
            // Create the unary operation's parameter.
            ParameterExpression param = Expression.Parameter(typeof(T), "param");
            try
            {
                // Try to compile the expression tree and return it.
                return Expression.Lambda<Func<TArg, TReturn>>
                    (expression(param), param).Compile();
            }
            catch (Exception e)
            {
                // If program control reaches this point, the expression tree failed to
                // compile. This likely means that the given operation is not possible
                // for the given type.
                string error = e.Message;
                // Return a delegate that throws an exception.
                return delegate { throw new InvalidOperationException(error); };
            }
        }

        // Like the unary delegate creation function, but returns a binary delegate instead.
        // Since it's a binary delegate, it takes two arguments instead of one.
        static Func<TArg1, TArg2, TReturn> Create<TArg1, TArg2, TReturn>
            (Func<Expression, Expression, BinaryExpression> expression)
        {
            ParameterExpression lhs = Expression.Parameter(typeof(TArg1), "lhs");
            ParameterExpression rhs = Expression.Parameter(typeof(TArg1), "rhs");
            try
            {
                return Expression.Lambda<Func<TArg1, TArg2, TReturn>>
                    (expression(lhs, rhs), lhs, rhs).Compile();
            }
            catch (Exception e)
            {
                string error = e.Message;
                return delegate { throw new InvalidOperationException(error); };
            }
        }
    }

    // Unary negation: negates the given value.
    // Example: 42 becomes -42 when negated.
    public static T Negate<T>(T value)
    {
        return Operations<T>.Negate(value);
    }
}