using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;

	public MainCharacterController PlayerCharacter;

    public CameraFollow camera;

	public int StartingLives;
	public int LivesLeft;

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
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start()
	{
		LivesLeft = StartingLives;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void PlayerTakeDamage(int damageAmount, Vector2 attackerLocation)
	{

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
            RestartLevel(2);
        }

		// Handle player damage & animation

		var playerPosition = new Vector2(PlayerCharacter.transform.position.x, PlayerCharacter.transform.position.y);
		var kickbackDirection = -(attackerLocation - playerPosition).normalized;
		PlayerCharacter.TakeDamage(kickbackDirection);


		// Handle UI changes
		GameObject tempObject = GameObject.Find("LivesLeft");

		if (tempObject != null)
		{
			Text livesText = tempObject.GetComponent<Text>();
			livesText.text = LivesLeft.ToString();
		}
	}


	public void RestartLevel(float delay)
	{
		StartCoroutine(RestartLevelDelay(delay));
	}

	private IEnumerator RestartLevelDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene("MainScene");
	}
}
