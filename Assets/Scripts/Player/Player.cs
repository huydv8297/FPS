using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FPS
{
    public class Player : MonoBehaviour
    {

        public int maxHP = 100;
        public int currentHP;
        public float speed = 10f;
        public Weapon weapon;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool CheckBulletAmount()
        {
            if (weapon.bulletCount <= 0 && weapon.bulletInWeapon > 0)
                return true;
            else return false;
        }

        public void Reload() {
            PreReload();
            //Invoke("PreReload", 1f);
        }

        public void PreReload()
        {
            weapon.Reload();
        }
    }
}

