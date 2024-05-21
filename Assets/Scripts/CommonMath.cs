using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonMath
{
    public static bool ProbabilityMethod(int percent)
    {
        int randomNum = Random.Range(0, 100);
        if (randomNum <= percent) { return true; }
        else { return false; }
    }
}
