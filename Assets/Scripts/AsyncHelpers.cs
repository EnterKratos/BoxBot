using System;
using System.Collections;
using UnityEngine;

public static class AsyncHelpers
{
    public static IEnumerator Defer(Action action, float delay = 0.1f)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}