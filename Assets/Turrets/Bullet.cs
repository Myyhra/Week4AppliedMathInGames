using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{

    public GameObject[] enemy;
    public Vector3 direction;
    public float speed;
    public float getHitRange = 1f;

    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        transform.position += direction * (speed * Time.deltaTime);
        HitEnemy();
        // HitPlayer();
    }

    /* void HitPlayer()
    {
        if(enemy == null) return;

        var dir = enemy.transform.position - transform.position;

        if(dir.magnitude <= getHitRange)
        {
            Debug.Log("Player Hit");
            int current = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(current);
        }
    } */

    void HitEnemy()
    {
       foreach(var e in enemy)
        {
            var dir = e.transform.position - transform.position;

            if(dir.magnitude <= getHitRange)
            {
                Debug.Log("Enemy Hit");
                IDie die = e.GetComponent<IDie>();
                die.Die();
            }
        }
    }
}
