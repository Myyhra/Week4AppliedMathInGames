using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Turret_Flamethrower : Turret_Detector
{
    public GameObject[] targets; 
    [Header("Flamethrower Settings")]
    public int numberOfProjectiles;
    public float projectileSpeed;
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
            Fire();
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


    void DetectionArea(GameObject target)
    {
        var dir = target.transform.position - transform.position;

        var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        var dot = Vector3.Dot(transform.forward.normalized, dir.normalized);

        // var hit = transform.eulerAngles.z <= angle+coneSize && transform.eulerAngles.z >= angle - coneSize;

        bool didhit = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y,angle)) <= coneSize;

        GetDistance(didhit, dir.magnitude);

        // transform.rotation = Quaternion.Slerp(transform.rotation,_target.rotation, Time.deltaTime);


        Debug.Log($"Turret: {gameObject.name}, Angle: {angle}, Dot: {Mathf.Acos(dot)}, Hit?: {didhit}" );
    }

    void Fire()
    {
        if(isEnemyInRange && !isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootDelay());
        }
    }

    IEnumerator ShootDelay()
    {
        Shoot();
        yield return new WaitForSecondsRealtime(shootingInterval);
        isShooting = false;
    }
    void Shoot()
    {
        if(!isEnemyInRange) return;


        float angleStep = (coneSize * 2) / (numberOfProjectiles-1);

        for (int i = 0; i< numberOfProjectiles ;i++)
        {
            float currentAngle =  -coneSize + (angleStep * i);


            Quaternion rotation = Quaternion.Euler(0f, currentAngle, 0f);

            Vector3 shootDir = rotation * transform.forward;

            Bullet b = Instantiate(bullet, transform.position + shootDir * radius, Quaternion.identity);
            b.direction = shootDir.normalized;
            b.speed = projectileSpeed;
        }
        
    }

}
