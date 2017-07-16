using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Laser;

    public GameObject Saw;

	public GameObject SpawnPoints;

	public GameObject batPrefab;

    public GameObject bossBat;

    public GameObject Player;

    private BaseEnemy currBoss = null;

    private bool bossActive = false;

    private int numberOfBats = 0;

    private Stats currBatStats = new Stats();

    private int currentWave = 1;

    private GameObject currHazard = null;

	// Use this for initialization
	void Start()
	{
        SpawnNextWave(currentWave);
	}

	// Update is called once per frame
	void Update()
	{
        if(currBoss == null && bossActive)
        {
            bossActive = false;
        } else if(currBoss != null && bossActive)
        {
            int size = FindObjectsOfType(typeof(BatController)).Length;
           while(numberOfBats > size)
           {
                spawnBat(currBatStats.hp, currBatStats.moveSpeed);
                size++;
           }
        }

        if (!bossActive && !enemiesAlive())
        {
            currentWave++;
            SpawnNextWave(currentWave);
        }
	}

	public void SpawnNextWave(int wave)
	{
        if(currHazard != null)
        {
            Destroy(currHazard);
        }
		switch (wave)
		{
			case 1:
                spawnLaser();
                currBatStats.hp = 1;
                currBatStats.moveSpeed = 30;
                numberOfBats = 3;
                for (int i = 0; i < 3; i++)
				{
                    spawnBat(currBatStats.hp, currBatStats.moveSpeed);
                }
                spawnBossBat(5, 20);
                break;
            case 2:
                spawnSaw();
                currBatStats.hp = 1;
                currBatStats.moveSpeed = 30;
                numberOfBats = 6;
                for (int i = 0; i < 3; i++)
                {
                    spawnBat(currBatStats.hp, currBatStats.moveSpeed);
                }
                spawnBossBat(5, 20);
                break;
            case 3:
                spawnLaser();
                currBatStats.hp = 1;
                currBatStats.moveSpeed = 30;
                numberOfBats = 6;
                for (int i = 0; i < 3; i++)
                {
                    spawnBat(currBatStats.hp, currBatStats.moveSpeed);
                }
                spawnBossBat(5, 20);
                break;
            default:
				break;
		}

	}

    private void spawnBat(int hp, int moveSpeed)
    {
        var spawnPositions = SpawnPoints.GetComponentsInChildren<Transform>();
        var numberOfSpawns = spawnPositions.Length;
        var usedSpawns = new Transform[numberOfSpawns];

        var batSpawnNumber = (int)Random.Range(0, numberOfSpawns);
        Transform spawnPoint = spawnPositions[batSpawnNumber];
        Vector3 pos = new Vector3(spawnPoint.position.x + Random.Range(-1, 1), spawnPoint.position.y + Random.Range(-1, 1));
        GameObject bat = Instantiate(batPrefab, pos, spawnPoint.rotation);
        BatController controller = (BatController)bat.GetComponent("BatController");
        controller.hp = hp;
        controller.MoveSpeed = moveSpeed;
        controller.PlayerTarget = Player.transform;
    }

    private void spawnBossBat(int hp, int moveSpeed)
    {
        var spawnPositions = SpawnPoints.GetComponentsInChildren<Transform>();
        var numberOfSpawns = spawnPositions.Length;
        var usedSpawns = new Transform[numberOfSpawns];

        var batSpawnNumber = (int)Random.Range(0, numberOfSpawns);
        Transform spawnPoint = spawnPositions[batSpawnNumber];
        Vector3 pos = new Vector3(spawnPoint.position.x + Random.Range(-1, 1), spawnPoint.position.y + Random.Range(-1, 1));
        GameObject bat = Instantiate(bossBat, pos, spawnPoint.rotation);
        BossBatController controller = (BossBatController)bat.GetComponent("BossBatController");
        controller.hp = hp;
        controller.MoveSpeed = moveSpeed;
        controller.PlayerTarget = Player.transform;
        currBoss = controller;
        bossActive = true;
    }

    private void spawnSaw()
    {
        currHazard = Instantiate(Saw);
    }

    private void spawnLaser()
    {
        currHazard = Instantiate(Laser);
    }

    private bool enemiesAlive()
    {
        int bats = FindObjectsOfType(typeof(BatController)).Length;
        return bats != 0;
    }

    private class Stats
    {
        public int hp = 0;
        public int moveSpeed = 0;
    }
}
