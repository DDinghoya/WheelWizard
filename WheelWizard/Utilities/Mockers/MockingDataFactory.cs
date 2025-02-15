﻿using System;
using System.Collections.Generic;

namespace WheelWizard.Utilities.Mockers;

public abstract class MockingDataFactory<T, U> where U: MockingDataFactory<T,U>, new()
{
    public static U Instance { get; } = new U();
    public abstract T Create();

    protected virtual string DictionaryKeyGenerator(T value) => value.ToString();

    public T[] CreateMultiple(int count = 5)
    {
        var result = new T[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = Create();
        }
        return result;
    }
    
    public Dictionary<string, T> CreateAsDictionary(int count = 5)
    {
        var result = new Dictionary<string, T>();
        var list = CreateMultiple(count);
        foreach (var item in list)
        {
            result.Add(DictionaryKeyGenerator(item), item);
        }
        return result;
    }
}
