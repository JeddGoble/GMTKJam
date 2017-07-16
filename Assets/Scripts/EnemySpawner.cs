using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject SpawnPoints;

	public GameObject batPrefab;

    public GameObject bossBat;

    public GameObject Player;

    private BaseEnemy currBoss = null;

    private bool bossActive = false;

    private int numberOfBats = 0;

	// Use this for initialization
	void Start()
	{
		SpawnNextWave(1);
	}

	// Update is called once per frame
	void Update()
	{
        if(currBoss == null && bossActive)
        {
            Debug.Log("enemy boss killed");
        } else if(currBoss != null && bossActive)
        {
            int size = FindObjectsOfType(typeof(BatController)).Length;
           while(numberOfBats > size)
           {
                spawnBat();
                size++;
           }
        }
	}

	public void SpawnNextWave(int wave)
	{
		switch (wave)
		{
			case 1:
                numberOfBats = 3;
                for (int i = 0; i < 3; i++)
				{
                    spawnBat();
                }
                spawnBossBat();
                break;
			default:
				break;
		}

	}

    private void spawnBat()
    {
        var spawnPositions = SpawnPoints.GetComponentsInChildren<Transform>();
        var numberOfSpawns = spawnPositions.Length;
        var usedSpawns = new Transform[numberOfSpawns];

        var batSpawnNumber = (int)Random.Range(0, numberOfSpawns);
        Transform spawnPoint = spawnPositions[batSpawnNumber];
        Vector3 pos = new Vector3(spawnPoint.position.x + Random.Range(-1, 1), spawnPoint.position.y + Random.Range(-1, 1));
        GameObject bat = Instantiate(batPrefab, pos, spawnPoint.rotation);
        BatController controller = (BatController)bat.GetComponent("BatController");
        controller.PlayerTarget = Player.transform;
    }

    private void spawnBossBat()
    {
        var spawnPositions = SpawnPoints.GetComponentsInChildren<Transform>();
        var numberOfSpawns = spawnPositions.Length;
        var usedSpawns = new Transform[numberOfSpawns];

        var batSpawnNumber = (int)Random.Range(0, numberOfSpawns);
        Transform spawnPoint = spawnPositions[batSpawnNumber];
        Vector3 pos = new Vector3(spawnPoint.position.x + Random.Range(-1, 1), spawnPoint.position.y + Random.Range(-1, 1));
        GameObject bat = Instantiate(bossBat, pos, spawnPoint.rotation);
        BossBatController controller = (BossBatController)bat.GetComponent("BossBatController");
        controller.PlayerTarget = Player.transform;
        currBoss = controller;
        bossActive = true;
    }
}
