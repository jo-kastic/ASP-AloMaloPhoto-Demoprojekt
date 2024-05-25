﻿using Spg.AloMalo.DomainModel.Interfaces;
using System;
using System.Linq.Expressions;

public class StartsWithFilter<T> : IFilter<T>
{
    private readonly string _property;
    private readonly string _value;

    public StartsWithFilter(string property, string value)
    {
        _property = property;
        _value = value;
    }

    public Expression<Func<T, bool>> Apply()
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var member = Expression.Property(parameter, _property);
        var constant = Expression.Constant(_value);
        var body = Expression.Call(member, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), constant);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}