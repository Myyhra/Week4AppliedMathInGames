using System.Collections;
using UnityEngine;

public class Turret_Homing : Turret_Detector
{
    [SerializeField] GameObject model;
    public float rotationSpeed = 10f;
    public GameObject[] targets; 
    [Header("Shooting Settings")]
    public float projectileSpeed;
    public float shootInterval = 1f;
    float firingAngle;
    Vector3 targetDir;
    public Bullet_Homing bulletHoming;

    bool isShooting;

    //Detection Variables
    float angle;
    float dot;
    bool didhit;
    GameObject currentTarget;

    void Start()
    {
        isShooting = false;
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
            if(isEnemyInRange)
            {
                currentTarget = t;
                StartShooting();
                break;
            }
        }    
    }


    void DetectionArea(GameObject targets)
    {
        targetDir = targets.transform.position - transform.position;

        angle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;

        dot = Vector3.Dot(transform.forward.normalized, targetDir.normalized);

        didhit = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y,angle)) <= coneSize;

        GetDistance(didhit, targetDir.magnitude);

        Debug.Log($"Turret: {gameObject.name}, Angle: {angle}, Dot: {Mathf.Acos(dot)}, Hit?: {didhit}" );
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

    void StartShooting()
    {
        if(!isShooting)
        {
            isShooting = true;
            HomingShoot();
        }
    }

    void HomingShoot()
    {
       Debug.Log($"isShooting");
        model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * rotationSpeed); //Insert Tween
        StartCoroutine(ShootDelay(targetDir));
        
    }
    IEnumerator ShootDelay(Vector3 targetDir)
    {
        Bullet_Homing b = Instantiate(bulletHoming, transform.position, Quaternion.identity);
        b.speed = projectileSpeed;
        b.target = currentTarget.transform;
        yield return new WaitForSeconds(shootInterval);
        isShooting = false;
        
    }
}
