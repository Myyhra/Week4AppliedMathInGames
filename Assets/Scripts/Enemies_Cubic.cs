using UnityEngine;

public class Enemies_Cubic : MonoBehaviour
{

    public float duration = 10f;
    float timeTotal;
    public Transform _spawn;
    public Transform _anchor;
    public Transform _anchor2;
    public Transform _target;

    Cubic_Path cubic_Path;

    void Start()
    {
        cubic_Path = new Cubic_Path();
        // spawner = GetComponentInParent<Spawner>();
    }

    void Update()
    {
        var t = timeTotal / duration;
        if(t>1) return;
        transform.position = cubic_Path.CubicFast(_spawn.position, _anchor.position, _anchor2.position, _target.position, t);

        timeTotal += Time.deltaTime;
    }
}
