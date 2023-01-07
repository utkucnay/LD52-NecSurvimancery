using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static Queue<T> Clone<T>(this Queue<T> listToClone) where T : ICloneable
    {
        return new Queue<T>(listToClone.Select(item => (T)item.Clone()).ToList());
    }
}