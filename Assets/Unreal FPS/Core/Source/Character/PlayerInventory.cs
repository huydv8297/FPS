/* ================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnrealFPS
{
    #region Structs
    [Serializable]
    public struct WeaponCompartment
    {
        public Weapon weapon;
        public KeyCode key;
    }

    [Serializable]
    public struct InventoryGroup
    {
        public string name;
        public List<WeaponCompartment> weaponCompartment;
    }
    #endregion

    public class PlayerInventory : MonoBehaviour, IInventory
    {
        [SerializeField] private Transform fpsCamera;
        [SerializeField] List<InventoryGroup> inventoryGroups = new List<InventoryGroup>();

        private bool isSelect;

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (Input.anyKeyDown)
                SelectWeaponByKey();

            if (UInput.GetButtonDown("DropWeapon") && GetActiveWeapon() != null)
                DropWeapon(GetActiveWeapon().GetComponent<WeaponIdentifier>().Weapon);
        }

        private void SelectWeaponByKey()
        {
            for (int i = 0; i < inventoryGroups.Count; i++)
            {
                for (int j = 0; j < inventoryGroups[i].weaponCompartment.Count; j++)
                {
                    if (Input.GetKeyDown(inventoryGroups[i].weaponCompartment[j].key) && inventoryGroups[i].weaponCompartment[j].weapon != null)
                    {
                        StartCoroutine(OnSelectWeapon(inventoryGroups[i].weaponCompartment[j].weapon));
                    }
                }
            }
        }

        /// <summary>
        /// Add weapon
        /// </summary>
        /// <param name="weapon"></param>
        public void AddWeapon(Weapon weapon)
        {
            for (int i = 0; i < inventoryGroups.Count; i++)
            {
                if (inventoryGroups[i].name == weapon.Group)
                {

                    for (int j = 0; j < inventoryGroups[i].weaponCompartment.Count; j++)
                    {
                        if (inventoryGroups[i].weaponCompartment[j].weapon == null)
                        {
                            WeaponCompartment weaponCompartment = new WeaponCompartment
                            {
                            weapon = weapon,
                            key = inventoryGroups[i].weaponCompartment[j].key
                            };
                            inventoryGroups[i].weaponCompartment[j] = weaponCompartment;
                            return;
                        }
                    }

                    if (GetActiveWeapon() == null)
                        return;
                        
                    string curGroup = GetActiveWeapon().GetComponent<WeaponIdentifier>().Weapon.Group;

                    if (weapon.Group == curGroup)
                    {
                        for (int j = 0; j < inventoryGroups[i].weaponCompartment.Count; j++)
                        {
                            if (inventoryGroups[i].weaponCompartment[j].weapon == GetActiveWeapon().GetComponent<WeaponIdentifier>().Weapon)
                            {
                                WeaponCompartment weaponCompartment = new WeaponCompartment
                                {
                                weapon = weapon,
                                key = inventoryGroups[i].weaponCompartment[j].key
                                };
                                inventoryGroups[i].weaponCompartment[j] = weaponCompartment;
                                StartCoroutine(OnElementDrop(GetActiveWeapon().GetComponent<WeaponIdentifier>().Weapon));
                                return;
                            }
                        }
                    }
                    else
                    {
                        WeaponCompartment weaponCompartment = new WeaponCompartment
                        {
                            weapon = weapon,
                            key = inventoryGroups[i].weaponCompartment[inventoryGroups[i].weaponCompartment.Count - 1].key
                        };
                        inventoryGroups[i].weaponCompartment[inventoryGroups[i].weaponCompartment.Count - 1] = weaponCompartment;
                        StartCoroutine(OnElementDrop(GetActiveWeapon().GetComponent<WeaponIdentifier>().Weapon));
                        return;
                    }
                }
            }

        }

        /// <summary>
        /// Drop weapon
        /// </summary>
        /// <param name="weapon"></param>
        public void DropWeapon(Weapon weapon)
        {
            for (int i = 0; i < inventoryGroups.Count; i++)
            {
                if (inventoryGroups[i].name == weapon.Group)
                {
                    for (int j = 0; j < inventoryGroups[i].weaponCompartment.Count; j++)
                    {
                        if (inventoryGroups[i].weaponCompartment[j].weapon == weapon)
                        {
                            WeaponCompartment weaponCompartment = new WeaponCompartment
                            {
                            weapon = null,
                            key = inventoryGroups[i].weaponCompartment[j].key
                            };
                            inventoryGroups[i].weaponCompartment[j] = weaponCompartment;
                            StartCoroutine(OnElementDrop(weapon));
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Activate weapon by id
        /// </summary>
        /// <param name="id"></param>
        public void ActivateWeapon(string id)
        {
            for (int i = 0; i < fpsCamera.childCount; i++)
            {
                if (fpsCamera.GetChild(i).CompareTag("Weapon"))
                {
                    Transform weapon = fpsCamera.GetChild(i);
                    if (weapon.GetComponent<WeaponIdentifier>() != null && weapon.GetComponent<WeaponIdentifier>().Weapon.Id == id)
                        weapon.gameObject.SetActive(true);
                    else
                        weapon.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Deactivate weapon by id
        /// </summary>
        /// <param name="id"></param>
        public void DeactivateWeapon(string id)
        {
            for (int i = 0; i < fpsCamera.childCount; i++)
            {
                if (fpsCamera.GetChild(i).CompareTag("Weapon"))
                {
                    //saving weapon for easy access
                    Transform weapon = fpsCamera.GetChild(i);
                    if (weapon.GetComponent<WeaponIdentifier>().Weapon.Id == id)
                        weapon.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Deactivate all weapons
        /// </summary>
        public void DeactivateAllWeapons()
        {
            for (int i = 0; i < fpsCamera.childCount; i++)
            {
                if (fpsCamera.GetChild(i).CompareTag("Weapon"))
                    fpsCamera.GetChild(i).gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Deactivate current active weapon
        /// </summary>
        public void DeactivateActiveWeapon()
        {
            StartCoroutine(DeactivateActiveWeaponHandler());
        }

        public void SelectWeapon(Weapon weapon)
        {
            StartCoroutine(OnSelectWeapon(weapon));
        }

        /// <summary>
        /// Get weapon by unique identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Transform GetWeapon(string id)
        {
            for (int i = 0; i < fpsCamera.childCount; i++)
            {
                if (fpsCamera.GetChild(i).CompareTag("Weapon"))
                {
                    //saving weapon for easy access
                    Transform weapon = fpsCamera.GetChild(i);
                    if (weapon.GetComponent<WeaponIdentifier>().Weapon.Id == id)
                        return weapon;
                }
            }
            return null;
        }

        /// <summary>
        /// Get weapon by index in FPCamera child
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Transform GetWeapon(int index)
        {
            return (fpsCamera.transform.childCount > index) ? fpsCamera.transform.GetChild(index) : null;
        }

        /// <summary>
        /// Get current active weapon
        /// </summary>
        /// <returns></returns>
        public Transform GetActiveWeapon()
        {
            for (int i = 0; i < fpsCamera.childCount; i++)
            {
                if (fpsCamera.GetChild(i).CompareTag("Weapon") && fpsCamera.GetChild(i).gameObject.activeSelf)
                {
                    return fpsCamera.GetChild(i);
                }
            }
            return null;
        }

        /// <summary>
        /// When weapon dropped create weapon gameobject on scene 
        /// </summary>
        /// <param name="weapon"></param>
        public IEnumerator OnElementDrop(Weapon weapon)
        {
            isSelect = true;
            yield return new WaitForSeconds(0.3f);
            Transform playerWeapon;
            playerWeapon = GetWeapon(weapon.Id);
            playerWeapon.gameObject.SetActive(false);
            Vector3 pos = fpsCamera.position + fpsCamera.forward * 1;
            GameObject dropWeapon = Instantiate(weapon.Drop, pos, Quaternion.identity);
            if (dropWeapon.GetComponent<Rigidbody>())
                dropWeapon.GetComponent<Rigidbody>().AddForce(fpsCamera.forward * 0.5f, ForceMode.Impulse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IEnumerator OnSelectWeapon(Weapon weapon)
        {
            isSelect = true;
            yield return new WaitForSeconds(0.1f);
            isSelect = false;
            yield return new WaitForSeconds(0.2f);
            ActivateWeapon(weapon.Id);
            yield break;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator DeactivateActiveWeaponHandler()
        {
            isSelect = true;
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < fpsCamera.childCount; i++)
            {
                if (fpsCamera.GetChild(i).CompareTag("Weapon") && fpsCamera.GetChild(i).gameObject.activeSelf)
                {
                    fpsCamera.GetChild(i).gameObject.SetActive(false);
                }
            }
            yield break;
        }

        public List<InventoryGroup> GetGroups()
        {
            return inventoryGroups;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsSelect { get { return isSelect; } }
    }
}