using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour {
    public AudioSource bossKill;
    public static AudioSource bossKillStatic;

    public AudioSource enemyHit;
    public static AudioSource enemyHitStatic;

    public AudioSource enemyKill;
    public static AudioSource enemyKillStatic;

    public AudioSource playerHit;
    public static AudioSource playerHitStatic;

    public AudioSource playerKill;
    public static AudioSource playerKillStatic;

    public AudioSource heartPickup;
    public static AudioSource heartPickupStatic;

    private void Awake()
    {
        if (bossKillStatic == null)
        {
            bossKillStatic = Instantiate<AudioSource>(bossKill);
            DontDestroyOnLoad(bossKillStatic);
        }

        if (enemyHitStatic == null)
        {
            enemyHitStatic = Instantiate<AudioSource>(enemyHit);
            DontDestroyOnLoad(enemyHitStatic);
        }

        if (enemyKillStatic == null)
        {
            enemyKillStatic = Instantiate<AudioSource>(enemyKill);
            DontDestroyOnLoad(enemyKillStatic);
        }

        if (playerHitStatic == null)
        {
            playerHitStatic = Instantiate<AudioSource>(playerHit);
            DontDestroyOnLoad(playerHitStatic);
        }

        if (playerKillStatic == null)
        {
            playerKillStatic = Instantiate<AudioSource>(playerKill);
            DontDestroyOnLoad(playerKillStatic);
        }

        if (heartPickupStatic == null)
        {
            heartPickupStatic = Instantiate<AudioSource>(heartPickup);
            DontDestroyOnLoad(heartPickupStatic);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playEnemyHit()
    {
        enemyHitStatic.Play();
    }

    public void playEnemyKill()
    {
        enemyKillStatic.Play();
    }

    public void playBossKill()
    {
        bossKillStatic.Play();
    }

    public void playPlayerHit()
    {
        playerHitStatic.Play();
    }

    public void playPlayerKill()
    {
        playerKillStatic.Play();
    }

    public void playHeartPickup()
    {
        heartPickupStatic.Play();
    }
}
