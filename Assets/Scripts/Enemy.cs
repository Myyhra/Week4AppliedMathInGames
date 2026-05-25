using UnityEngine;

public class Enemy : MonoBehaviour, IDie
{
    public GameObject target;
    public int damage = 1;
    [SerializeField] float getHitRange = 1f;

    IDamageable damageable;
    void Start()
    {
        
    }
    void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        damageable = target.GetComponent<IDamageable>();
    }

    void Update()
    {
        if(target == null && damageable == null) return;
        HitTarget();
        
    }

    void HitTarget()
    {
        var dir = target.transform.position - transform.position;
        if(dir.magnitude <= getHitRange)
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
}

public interface IDie
{
    void Die();
}


