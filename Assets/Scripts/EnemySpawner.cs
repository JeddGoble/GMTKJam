using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public GameObject SpawnPoints;

	public GameObject batPrefab;

    public GameObject Player;

	// Use this for initialization
	void Start()
	{
		SpawnNextWave(1);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SpawnNextWave(int wave)
	{

		var spawnPositions = SpawnPoints.GetComponentsInChildren<Transform>();
		var numberOfSpawns = spawnPositions.Length;
		var usedSpawns = new Transform[numberOfSpawns];

		switch (wave)
		{
			case 1:
				for (int i = 0; i < 3; i++)
				{
                    var batSpawnNumber = (int)Random.Range(0, numberOfSpawns);
                    Transform spawnPoint = spawnPositions[batSpawnNumber];
                    Vector3 pos = new Vector3(spawnPoint.position.x + Random.Range(-1, 1), spawnPoint.position.y + Random.Range(-1, 1));
                    GameObject bat = Instantiate(batPrefab, pos, spawnPoint.rotation);
                    BatController controller = (BatController) bat.GetComponent("BatController");
                    controller.PlayerTarget = Player.transform;
				}

				break;
			default:
				break;
		}

	}

}
