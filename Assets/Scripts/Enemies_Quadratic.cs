using UnityEngine;

public class Enemies_Quadratic : MonoBehaviour
{
    // [SerializeField] private Spawner spawner;
    public float duration = 10f;
    float timeTotal;
    public Transform _spawn;
    public Transform _anchor;
    public Transform _target;
    Quadratic_Path quadratic_Path;

    void Start()
    {
        quadratic_Path = new Quadratic_Path();
        // spawner = GetComponentInParent<Spawner>();
    }

    void Update()
    {
        var t = timeTotal / duration;
        if(t>1) return;
        transform.position = quadratic_Path.QuadraticBezier(_spawn.position, _anchor.position, _target.position, t);

        timeTotal += Time.deltaTime;
    }
}
