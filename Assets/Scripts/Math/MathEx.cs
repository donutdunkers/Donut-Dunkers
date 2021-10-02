using System;
using UnityEngine;


public static class MathEx
{ 
public const float sqrt2 = 1.41421354f;

public const float sincos45 = 0.707106769f;

public static float Cosd(float f)
{ 
return Mathf.Cos(f * 0.0174532924f);
}

public static float Sind(float f)
{ 
return Mathf.Sin(f * 0.0174532924f);
}

public static float AngleNormalized(Vector3 v1, Vector3 v2)
{ 
return Mathf.Acos(Vector3.Dot(v1, v2)) * 57.29578f;
}

public static int Repeat(int a, int n)
{ 
while (a > n)
{ 
a -= n;
}
while (a < 0)
{ 
a += n;
}
return a;
}

public static float GetBallisticY(float y0, float v0, float t, float g)
{ 
return y0 + v0 * t + g * t * t / 2f;
}

public static Vector3 DivideVectors(Vector3 v1, Vector3 v2)
{ 
return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
}

public static bool RollADie(int n)
{ 
return UnityEngine.Random.Range(0, n) == 0;
}

public static float InverseLerpRange(float src, float start0, float start1, float end1, float end0)
{ 
if (src >= end0)
{ 
return 0f;
}
if (src <= start0)
{ 
return 0f;
}
if (src <= start1)
{ 
return (src - start0) / (start1 - start0);
}
if (src > end1)
{ 
return Mathf.Clamp01((src - end1) / (end0 - end1));
}
return 1f;
}
}
