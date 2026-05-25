using UnityEngine;

public class Lerp : MonoBehaviour
{
    public Transform _target;

    public Transform _control;
    public Transform _control2;

    public Transform _origin;

    public float duration =3f;
    float timeTotal;
    void Start()
    {
        timeTotal = 0;
    }

    void Update()
    {
        if(_origin == null || _target ==null || _control ==null) return;
        
        var t = timeTotal / duration;

        var tsquared = Mathf.Pow(t, 2);

        var tSquareRootf = Mathf.Pow(t, 0.5f);

        var tSquareRoot = Mathf.Sqrt(t);

        // transform.position = Vector3.Lerp(_origin.position,_target.position,tsquared);
        if(t>1) return;
        // transform.position = QuadraticBezier(_origin.position,_control.position,_target.position, t);
        

        transform.position = CubicFast(_origin.position,_control.position,_control2.position,_target.position, t);
        
        
        timeTotal += Time.deltaTime;
    }

    private Vector3 QuadraticBezier(Vector3 p1,Vector3 p2, Vector3 p3, float t)
    {
        float u  = 1f - t;
        return u * u * p1  +  2f * u * t * p2  +  t * t * p3;

    }
    public static Vector3 CubicFast(Vector3 p0, Vector3 p1,
                                     Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;
        return u*u*u*p0  +  3*u*u*t*p1  +  3*u*t*t*p2  +  t*t*t*p3;
    }

}
