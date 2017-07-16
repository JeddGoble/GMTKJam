using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Laser;

    public GameObject Saw;

	public GameObject SpawnPoints;

	public GameObject batPrefab;

    public GameObject gobPrefab;

    public GameObject bossGob;

    public GameObject bossBat;

    public GameObject Player;

    private BaseEnemy currBoss = null;

    private bool bossActive = false;

    private int numberOfBats = 0;

    private int numberOfGoblins = 0;

    private Stats currBatStats = new Stats();

    private Stats currGobStats = new Stats();

    private int currentWave = 1;

    private List<GameObject> currHazards = new List<GameObject>();

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
                spawnBat(currBatStats.hp, currBatStats.moveSpeed, 1);
                size++;
            }

            size = FindObjectsOfType(typeof(GoblinArcherController)).Length;
            while (numberOfGoblins > size)
            {
                spawnGobs(currGobStats.hp, currGobStats.moveSpeed, 1);
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
        if(currHazards.Count > 0)
        {
            foreach (GameObject h in currHazards)
            {
                Destroy(h);
            }
        }
		switch (wave)
		{
			case 1:
                spawnLaser();
                
                currBatStats.hp = 1;
                currBatStats.moveSpeed = 30;
                numberOfBats = 3;
                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);

                currGobStats.hp = 2;
                currGobStats.moveSpeed = 40;
                numberOfGoblins = 1;
                spawnGobs(currBatStats.hp, currBatStats.moveSpeed, numberOfGoblins);

                spawnBossBat(5, 20);
                break;
            case 2:
                spawnSaw();
                currBatStats.hp = 1;
                currBatStats.moveSpeed = 30;
                numberOfBats = 6;
                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);

                spawnBossGob(5, 20);
                break;
            case 3:
                spawnLaser();
                currBatStats.hp = 1;
                currBatStats.moveSpeed = 30;
                numberOfBats = 6;

                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);
                spawnBossBat(5, 20);
                break;
            default:
				break;
		}

	}

    private void spawnGobs(int hp, int moveSpeed, int n)
    {
        for (int i = 0; i < n; i++)
        {
            var spawnPositions = SpawnPoints.GetComponentsInChildren<Transform>();
            var numberOfSpawns = spawnPositions.Length;
            var usedSpawns = new Transform[numberOfSpawns];

            var batSpawnNumber = (int)Random.Range(0, numberOfSpawns);
            Transform spawnPoint = spawnPositions[batSpawnNumber];
            Vector3 pos = new Vector3(spawnPoint.position.x + Random.Range(-1, 1), spawnPoint.position.y + Random.Range(-1, 1));
            GameObject bat = Instantiate(gobPrefab, pos, spawnPoint.rotation);
            GoblinArcherController controller = (GoblinArcherController)bat.GetComponent("GoblinArcherController");
            controller.hp = hp;
            controller.MoveSpeed = moveSpeed;
            controller.PlayerTarget = Player.transform;
        }
    }

    private void spawnBat(int hp, int moveSpeed, int n)
    {
        for (int i = 0; i < n; i++)
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

    private void spawnBossGob(int hp, int moveSpeed)
    {
        var spawnPositions = SpawnPoints.GetComponentsInChildren<Transform>();
        var numberOfSpawns = spawnPositions.Length;
        var usedSpawns = new Transform[numberOfSpawns];

        var batSpawnNumber = (int)Random.Range(0, numberOfSpawns);
        Transform spawnPoint = spawnPositions[batSpawnNumber];
        Vector3 pos = new Vector3(spawnPoint.position.x + Random.Range(-1, 1), spawnPoint.position.y + Random.Range(-1, 1));
        GameObject bat = Instantiate(bossGob, pos, spawnPoint.rotation);
        GoblinBossController controller = (GoblinBossController)bat.GetComponent("GoblinBossController");
        controller.hp = hp;
        controller.MoveSpeed = moveSpeed;
        controller.PlayerTarget = Player.transform;
        currBoss = controller;
        bossActive = true;
    }

    private void spawnSaw()
    {
        currHazards.Add(Instantiate(Saw));
    }

    private void spawnLaser()
    {
        currHazards.Add(Instantiate(Laser));
    }

    private bool enemiesAlive()
    {
        int bats = FindObjectsOfType(typeof(BatController)).Length;
        int gobs = FindObjectsOfType(typeof(GoblinBossController)).Length;
        return bats + gobs != 0;
    }

    private class Stats
    {
        public int hp = 0;
        public int moveSpeed = 0;
    }
}
