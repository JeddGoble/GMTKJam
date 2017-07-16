using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public GameObject SpawnPoints;

	public GameObject batPrefab;

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
				var batSpawnNumber = (int)Random.Range(0, numberOfSpawns);

				for (int i = 0; i < 3; i++)
				{
					Transform spawnPoint = spawnPositions[batSpawnNumber];
					Instantiate(batPrefab, spawnPoint);
				}

				break;
			default:
				break;
		}

	}

}
