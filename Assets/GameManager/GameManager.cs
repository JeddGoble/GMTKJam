using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static AudioSource start;

    public AudioSource startPrefab;

    public static GameManager instance;

	public EnemySpawner enemySpawner;

	public MainCharacterController PlayerCharacter;

    public CameraFollow camera;

	public HeartsManager HeartsManager;

	public int StartingLives;
	public int LivesLeft;

	public int CurrentLevel = 1;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

        if (start == null)
        {
            start = Instantiate<AudioSource>(startPrefab);
            DontDestroyOnLoad(start);
        }
	}

	// Use this for initialization
	void Start()
	{
		LivesLeft = StartingLives;

		if (HeartsManager != null)
		{
			HeartsManager.SetHearts(LivesLeft);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void PlayerGainHeart()
	{
		if (LivesLeft >= StartingLives)
		{
			print("Already at max health");
			return;
		}

		print("Adding a life");

		LivesLeft += 1;

		if (HeartsManager != null)
		{
			HeartsManager.SetHearts(LivesLeft);
		}
	}

	public void PlayerTakeDamage(int damageAmount, Vector2 attackerLocation)
	{

		print("Dealing player damage");

		// Handle state update
		if (PlayerCharacter.IsInvincible())
		{
			return;
		}
		
		LivesLeft -= damageAmount;

        if(LivesLeft <= 0)
        {
            PlayerCharacter.Kill();
            camera.Shake(1f, 2f);
            RestartLevelDelayed(2);
        }

		// Handle player damage & animation

		var playerPosition = new Vector2(PlayerCharacter.transform.position.x, PlayerCharacter.transform.position.y);
		var kickbackDirection = -(attackerLocation - playerPosition).normalized;
		PlayerCharacter.TakeDamage(kickbackDirection);

		if (HeartsManager != null)
		{
			HeartsManager.SetHearts(LivesLeft);
		}
	}


	public void RestartLevelDelayed(float delay)
	{
		StartCoroutine(RestartLevelDelay(delay));
	}

	private IEnumerator RestartLevelDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		RestartLevelNow();
	}

	public void RestartLevelNow()
	{
		print("Restarting level");
		SceneManager.LoadScene("MainScene");
		//enemySpawner.SpawnNextWave(CurrentLevel);
	}

    public void spawn()
    {
        enemySpawner.SpawnNextWave(1);
    }
}
