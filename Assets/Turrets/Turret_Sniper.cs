using UnityEngine;
using System.Collections;


public class Turret_Sniper : Turret_Detector
{

    [SerializeField] GameObject model;
    public float rotationSpeed = 10f;
    public GameObject[] targets; 
    [Header("Shooting Settings")]
    public float projectileSpeed;
    public float shootInterval = 1f;
    float angle;
    float firingAngle;
    Vector3 targetDir;
    public Bullet bullet;

    bool isShooting;

    void Start()
    {
        isShooting = false;
        // _target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(FindGameObjectsWithTagDelay());
    }

    IEnumerator FindGameObjectsWithTagDelay()
    {
        
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(FindGameObjectsWithTagDelay());
        
    }

    void Update()
    {
        // if(_target == null) return;
        foreach(var t in targets)
        {
            if(t == null) continue;
            DetectionArea(t);
            StartShooting(t);
            
        }

    }

    void GetDistance(bool didhit, float distance)
    {
        if(didhit && distance <= rangeDistance)
        {
            isEnemyInRange = true;   
            Debug.Log($"In Range of {gameObject.name}");
        }
        else
        {
            isEnemyInRange = false;
            Debug.Log("Not In Range");
        }
       
    }

    void StartShooting(GameObject target)
    {
        if(isEnemyInRange && !isShooting)
        {
            isShooting = true;
            SniperShoot(target);
        }
    }

    void SniperShoot(GameObject target)
    {
        
        Debug.Log($"isShooting");
        var targetDir = (target.transform.position - transform.position).normalized;
        var angle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * rotationSpeed);
        StartCoroutine(ShootDelay(targetDir));
        
    }
    IEnumerator ShootDelay(Vector3 dir)
    {
        Bullet b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.speed = projectileSpeed;
        b.direction = dir;
        yield return new WaitForSecondsRealtime(shootInterval);
        isShooting = false;
    }


    void DetectionArea(GameObject targets)
    {
        var dir = targets.transform.position - transform.position;

        var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        var dot = Vector3.Dot(transform.forward.normalized, dir.normalized);

        // var hit = transform.eulerAngles.z <= angle+coneSize && transform.eulerAngles.z >= angle - coneSize;

        bool didhit = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y,angle)) <= coneSize;

        GetDistance(didhit, dir.magnitude);




        // transform.rotation = Quaternion.Slerp(transform.rotation,_target.rotation, Time.deltaTime);


        Debug.Log($"Turret: {gameObject.name}, Angle: {angle}, Dot: {Mathf.Acos(dot)}, Hit?: {didhit}" );
    }
}
