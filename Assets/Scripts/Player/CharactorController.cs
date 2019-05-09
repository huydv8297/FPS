using FPS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour {

    public Animator animator;
    public int state;
    public const int STAND = 0;
    public const int WALK = 1;
    public const int LEFT = 2;
    public const int RIGHT = 3;
    public const int BACK = 4;
    public const int FRONT = 5;
    public const int RELOAD = 6;
    
    public GameObject bullet;
    public AudioSource audio;
    public AudioClip fireAudio;
    public Player player;

    public Transform playerTransform;

    public float deltaTime = 0;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        deltaTime += Time.deltaTime;
        GetCurretState();
        if (Input.GetKey(KeyCode.Mouse0) && deltaTime > player.weapon.speed)
        {
            
            if (player.CheckBulletAmount())
                player.Reload();
            if(player.weapon.bulletCount > 0)
                fire();
            Debug.Log("fire");
        }
        else {
            animator.SetInteger("state", state);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
            animator.SetBool("fire", false);
        
    }

    public void GetCurretState()
    {

        if (Input.GetKey(KeyCode.A))
        {
            WalkLeft();
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            WalkRight();

        }
        else
        if (Input.GetKey(KeyCode.W))
        {
            Run();
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            WalkBack();
        }
        else
            state = STAND;

        if (Input.GetKey(KeyCode.R))
        {
            player.weapon.Reload();
        }

    }

    public void fire() {
        animator.SetBool("fire", true);
        deltaTime = 0;
        bullet.SetActive(true);
        player.weapon.bulletCount--;
        Invoke("disableBullet", 0.1f);
        
    }

    public void disableBullet()
    {
        bullet.SetActive(false);
        
        audio.PlayOneShot(fireAudio);
    }

    public void Run()
    {
        state = FRONT;
        playerTransform.Translate(Vector3.forward * player.speed);
    }

    public void WalkLeft()
    {
        state = LEFT;
        playerTransform.Translate(Vector3.left * player.speed/2);
    }

    public void WalkRight()
    {
        state = RIGHT;
        playerTransform.Translate(Vector3.right * player.speed/2);
    }

    public void WalkBack()
    {
        state = BACK;
        playerTransform.Translate(Vector3.back * player.speed /2);
    }


}
