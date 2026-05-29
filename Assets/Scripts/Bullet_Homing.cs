using UnityEngine;

public class Bullet_Homing : Bullet
{

    public Transform target;
    public float rotationSpeed = 5f;
    void Start()
    {
       
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        HomingTarget(target);
        HitEnemy();
    }

    public void HomingTarget(Transform target)
    {
        Vector3 targetDir = (target.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotationSpeed); 
        transform.position += transform.forward * (speed * Time.deltaTime); //insert tween
    }

    void HitEnemy()
    {
        var distance = transform.position - target.position;
        // float distance = Vector3.Distance(transform.position, target.position);

        if(distance.magnitude <= getHitRange)
        {
            Debug.Log("Enemy Hit");

            IDie die = target.GetComponent<IDie>();

            if(die != null)
            {
                die.Die();
            }
        }
         /* foreach(var e in enemy)
          {
                var dir = e.transform.position - transform.position;
    
                if(dir.magnitude <= getHitRange)
                {
                 Debug.Log("Enemy Hit");
                 IDie die = e.GetComponent<IDie>();
                 die.Die();
                }
          } */
    }
}
