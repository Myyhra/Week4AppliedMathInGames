using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int numberOfEnemies = 20;
    public float spawnRate = 1f;
    [SerializeField] private GameObject quadraticEnemyPrefab;
    [SerializeField] private GameObject cubicEnemyPrefab;
    private Quadratic_Path quadratic_Path;
    private Cubic_Path cubic_Path;


    [SerializeField] private bool spawnQuadraticEnemy;
    public Transform quadraticSpawnPoint;
    public Transform quadraticControlPoint;

    [SerializeField] private bool spawnCubicEnemy;
    public Transform cubicSpawnPoint;
    public Transform cubicControlPoint1;
    public Transform cubicControlPoint2;
    
    
    public Transform TargetPoint;

    void Awake()
    {
        /* quadratic_Path = GetComponentInChildren<Quadratic_Path>();
        cubic_Path = GetComponentInChildren<Cubic_Path>(); */
        
    }
    void Start()
    {
        
    }

    public void StartGame()
    {
        if(spawnQuadraticEnemy)
        {
        QuadraticEnemy();
        }
        if(spawnCubicEnemy)
        {
        CubicEnemy();
            
        }
    }


    void QuadraticEnemy()
    {
        StartCoroutine(SpawnQuadraticEnemy(quadraticSpawnPoint, quadraticControlPoint, TargetPoint));
        
    }

    void CubicEnemy()
    {
        StartCoroutine(SpawnCubicEnemy(cubicSpawnPoint, cubicControlPoint1, cubicControlPoint2, TargetPoint));
        
    }
    IEnumerator SpawnQuadraticEnemy(Transform spawnPoint, Transform controlPoint, Transform targetPoint)
    {
        for (int i = 0; i <= numberOfEnemies; i++)
        {
        var enemy = Instantiate(quadraticEnemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.GetComponent<Enemies_Quadratic>()._spawn = spawnPoint;
        enemy.GetComponent<Enemies_Quadratic>()._anchor = controlPoint;
        enemy.GetComponent<Enemies_Quadratic>()._target = targetPoint;

        yield return new WaitForSeconds(spawnRate);
        }


    }
    IEnumerator SpawnCubicEnemy(Transform spawnPoint, Transform controlPoint1, Transform controlPoint2, Transform targetPoint)
    {
        for (int i = 0; i <= numberOfEnemies; i++)
        {
            var enemy = Instantiate(cubicEnemyPrefab, spawnPoint.position, Quaternion.identity);
            enemy.GetComponent<Enemies_Cubic>()._spawn = spawnPoint;
            enemy.GetComponent<Enemies_Cubic>()._anchor = controlPoint1;
            enemy.GetComponent<Enemies_Cubic>()._anchor2 = controlPoint2;
            enemy.GetComponent<Enemies_Cubic>()._target = targetPoint;        
            
            yield return new WaitForSeconds(spawnRate);
        }
        
        


    }

}
