using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Turret_Shotgun : Turret_Detector
{
    [SerializeField] GameObject model;
    public GameObject[] targets; 
    [Header("Shotgun Settings")]
    public int numberOfProjectiles;
    public float projectileSpeed;
    public float shotgunSize;
    public Bullet bullet;

    bool isShooting;
    public float shootingInterval = 1f;
    private const float radius = 1f;
    void Start()
    {
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
        foreach(var t in targets)
        {
            if(t == null) continue;
            DetectionArea(t);
            Shoot(t);
        }
    }

    void Shoot(GameObject target)
    {
        if(isEnemyInRange && !isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootDelay(target));
        }
    }

    void Shotgun(Vector3 target)
    {
       Vector3 targetDir = (target - transform.position).normalized;

       float centerTowardsPlayer = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;

       float startAngle = centerTowardsPlayer - shotgunSize / 2f;
       float angleStep = shotgunSize / (numberOfProjectiles - 1);

       model.transform.rotation = Quaternion.Slerp(model.transform.rotation, Quaternion.Euler(0, centerTowardsPlayer, 0), Time.deltaTime * 5f);

       for(int i =0; i < numberOfProjectiles; i++)
        {
            float currentAngle = startAngle + (angleStep * i);

            float rad = currentAngle * Mathf.Deg2Rad;

            Vector3 dir = new Vector3(Mathf.Sin(rad ), 0f, Mathf.Cos(rad)).normalized;
            Bullet b = Instantiate(bullet, transform.position + dir * radius, Quaternion.identity);
            b.direction = dir;
            b.speed = projectileSpeed;
        }

    }
    IEnumerator ShootDelay(GameObject target)
    {
        Shotgun(target.transform.position);
        yield return new WaitForSecondsRealtime(shootingInterval);
        isShooting = false;
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
