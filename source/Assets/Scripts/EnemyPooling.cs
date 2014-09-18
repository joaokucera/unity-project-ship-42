using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPooling : GenericPooling
{
    private static EnemyPooling instance;
    public static EnemyPooling Instance
    {
        get
        {
            if (EnemyPooling.instance == null)
            {
                EnemyPooling.instance = GameObject.Find("Generic Pooling").GetComponent<EnemyPooling>();
            }

            return EnemyPooling.instance;
        }
    }

    [SerializeField]
    private List<EnemySpawn> spawnEnemyPoints = null;

    /// <summary>
    /// BALANCE: Spawn de 7 a 2 segundos, diminuindo a cada 21 segundos.
    /// </summary>
    private float spawnTime;
    private float firstSpawnTime = 7;
    private float lastSpawnTime = 2;
    private const float timeToImprove = 21;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        base.Initialize();

        if (spawnEnemyPoints == null || spawnEnemyPoints.Count == 0)
        {
            Debug.LogError("There are no spawn points available!");
        }

        spawnTime = firstSpawnTime;

        StartCoroutine(Improve());
    }

    private IEnumerator Improve()
    {
        if (GameSettings.Instance.SailedTime % timeToImprove == 0)
        {
            spawnTime--;
            spawnTime = Mathf.Clamp(spawnTime, firstSpawnTime, lastSpawnTime);

            print("ENEMY: " + spawnTime);
        }

        yield return 0;
    }

    void Update()
    {
        Invoke("SpawnEnemy", spawnTime);
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, spawnEnemyPoints.Count);

        var enemySpawn = spawnEnemyPoints[index];
        enemySpawn.ReverseTranslate();

        SpawnEnemyFromPool(enemySpawn.transform.position, enemySpawn.side);

        CancelInvoke("SpawnEnemy");
    }

    public void SpawnEnemyFromPool(Vector2 position, MovementSide side)
    {
        GameObject enemy = GetObjectFromPool(position);

        if (enemy != null)
        {
            if (side == MovementSide.LEFTorDOWN)
            {
                enemy.GetComponent<Enemy>().side = MovementSide.RIGHTorUP;
                enemy.transform.localScale = new Vector2(-1, 1);
            }
            else if (side == MovementSide.RIGHTorUP)
            {
                enemy.GetComponent<Enemy>().side = MovementSide.LEFTorDOWN;
                enemy.transform.localScale = new Vector2(1, 1);
            }

            enemy.renderer.sortingLayerName = "Foreground";
            enemy.renderer.sortingOrder = 1;
        }
    }
}