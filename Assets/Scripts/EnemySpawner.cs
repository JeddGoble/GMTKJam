using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject SFX;

    public GameObject Laser;

    public GameObject Saw;

	public GameObject SpawnPoints;

	public GameObject batPrefab;

    public GameObject gobPrefab;

    public GameObject bossGob;

    public GameObject bossBat;

    public GameObject Player;

    public GameObject Spikes;

    private BaseEnemy currBoss = null;

    private bool bossActive = false;

    private int numberOfBats = 0;

    private int numberOfGoblins = 0;

    private Stats currBatStats = new Stats();

    private Stats currGobStats = new Stats();

    private int currentWave = 8;

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
                spawnSaw();
                
                currBatStats.hp = 1;
                currBatStats.moveSpeed = 30;
                numberOfBats = 2;
                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);

                spawnBossBat(3, 20);
                break;
            case 2:
                spawnLaser();

                currBatStats.hp = 1;
                currBatStats.moveSpeed = 35;
                numberOfBats = 4;
                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);

                spawnBossBat(5, 20);
                break;
            case 3:
                spawnSaw();

                currGobStats.hp = 2;
                currGobStats.moveSpeed = 40;
                numberOfGoblins = 1;
                spawnGobs(currBatStats.hp, currBatStats.moveSpeed, numberOfGoblins);

                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);
                spawnBossBat(5, 20);
                break;
            case 4:
                spawnSaw();
                spawnSpikes(2);

                currBatStats.hp = 1;
                currBatStats.moveSpeed = 35;
                numberOfBats = 2;

                currGobStats.hp = 1;
                currGobStats.moveSpeed = 40;
                numberOfGoblins = 2;
                spawnGobs(currBatStats.hp, currBatStats.moveSpeed, numberOfGoblins);

                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);
                spawnBossGob(5, 20);
                break;
            case 5:
                spawnSpikes(4);

                currBatStats.hp = 1;
                currBatStats.moveSpeed = 30;
                numberOfBats = 4;

                currGobStats.hp = 1;
                currGobStats.moveSpeed = 40;
                numberOfGoblins = 2;
                spawnGobs(currBatStats.hp, currBatStats.moveSpeed, numberOfGoblins);

                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);
                spawnBossBat(6, 30);
                break;
            case 6:
                spawnSaw();

                currBatStats.hp = 2;
                currBatStats.moveSpeed = 40;
                numberOfBats = 2;

                currGobStats.hp = 2;
                currGobStats.moveSpeed = 40;
                numberOfGoblins = 2;
                spawnGobs(currBatStats.hp, currBatStats.moveSpeed, numberOfGoblins);

                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);
                spawnBossGob(5, 30);
                break;
            case 7:
                spawnSpikes(6);
                spawnSaw();

                currBatStats.hp = 2;
                currBatStats.moveSpeed = 40;
                numberOfBats = 2;

                currGobStats.hp = 2;
                currGobStats.moveSpeed = 40;
                numberOfGoblins = 2;
                spawnGobs(currBatStats.hp, currBatStats.moveSpeed, numberOfGoblins);

                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);
                spawnBossGob(5, 30);
                break;
            case 8:
                spawnLaser();
                spawnSaw();
                spawnSpikes(5);

                currBatStats.hp = 2;
                currBatStats.moveSpeed = 40;
                numberOfBats = 2;

                currGobStats.hp = 2;
                currGobStats.moveSpeed = 40;
                numberOfGoblins = 2;
                spawnGobs(currBatStats.hp, currBatStats.moveSpeed, numberOfGoblins);

                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);
                spawnBossGob(5, 30);
                break;
            case 9:
                spawnLaser();
                spawnSpikes(8);

                currBatStats.hp = 2;
                currBatStats.moveSpeed = 40;
                numberOfBats = 3;

                currGobStats.hp = 2;
                currGobStats.moveSpeed = 40;
                numberOfGoblins = 2;
                spawnGobs(currBatStats.hp, currBatStats.moveSpeed, numberOfGoblins);

                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);
                spawnBossBat(6, 30);
                break;
            case 10:
                spawnLaser();
                spawnSaw();
                spawnSpikes(10);

                currBatStats.hp = 2;
                currBatStats.moveSpeed = 40;
                numberOfBats = 4;

                currGobStats.hp = 2;
                currGobStats.moveSpeed = 40;
                numberOfGoblins = 2;
                spawnGobs(currBatStats.hp, currBatStats.moveSpeed, numberOfGoblins);

                spawnBat(currBatStats.hp, currBatStats.moveSpeed, numberOfBats);
                spawnBossGob(6, 30);
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
            controller.sfx = (SFXController)SFX.GetComponent("SFXController");
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
            controller.sfx = (SFXController)SFX.GetComponent("SFXController");
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
        controller.sfx = (SFXController)SFX.GetComponent("SFXController");
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
        controller.sfx = (SFXController) SFX.GetComponent("SFXController");
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

    private void spawnSpikes(int n)
    {
        List<int> used = new List<int>();
        for(int i = 0;i < n;i++)
        {
            var spawnPositions = SpawnPoints.GetComponentsInChildren<Transform>();
            var numberOfSpawns = spawnPositions.Length;
            var usedSpawns = new Transform[numberOfSpawns];

            var batSpawnNumber = (int)Random.Range(0, numberOfSpawns);
            while(!used.Contains(batSpawnNumber))
            {
                batSpawnNumber = (int)Random.Range(0, numberOfSpawns);
                used.Add(batSpawnNumber);
            }

            Transform spawnPoint = spawnPositions[batSpawnNumber];
            Vector3 pos = new Vector3(spawnPoint.position.x, spawnPoint.position.y);
            GameObject bat = Instantiate(Spikes, pos, spawnPoint.rotation);
            currHazards.Add(bat);
        }
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
