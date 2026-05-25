using UnityEngine;

public class Quadratic_Path
{
    public Transform _target;

    public Transform _control;
    public Transform _control2;

    public Transform _spawn;
    
    public Vector3 QuadraticBezier(Vector3 p1,Vector3 p2, Vector3 p3, float t)
    {
        float u  = 1f - t;
        return u * u * p1  +  2f * u * t * p2  +  t * t * p3;

    }
}
