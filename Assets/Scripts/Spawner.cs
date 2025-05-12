using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] GameObject[] Enemys;
    [SerializeField] int enemys;
    public static int enemyCount;
    GameObject enemy;
    Transform spawnPoint;
    
    void Start()
    {
        enemyCount = enemys;
    }

    void Update()
    {
        enemyCount = enemys;
    }

    void OnEnable()
    {
        enemyCount = enemys;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < enemys; i++)
        {
            yield return new WaitForSeconds(0.5f);
            EnemySelector();
            SpawnPointSelector();
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);  
        }
    }
    
    void EnemySelector()
    {
        int rnd = Random.Range(0, Enemys.Length);
        enemy = Enemys[rnd];
    }
    void SpawnPointSelector()
    {
        int rnd = Random.Range(0, SpawnPoints.Length);
        spawnPoint = SpawnPoints[rnd];
    }
}
