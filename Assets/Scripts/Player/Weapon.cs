using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int bulletInWeapon;
    public int bulletCount;
    public int bulletInBox;
    public float speed;
    public AudioSource audio;
    public AudioClip reloadSound;

    void Start() {
    }


    public void Reload()
    {
        bulletCount = bulletInWeapon < bulletInBox ? bulletInWeapon : bulletInBox;
        bulletInWeapon = bulletInWeapon - bulletInBox < 0 ? 0 : bulletInWeapon - bulletInBox;
        audio.PlayOneShot(reloadSound);
    }
}
