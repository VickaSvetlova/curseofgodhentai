using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    #region Enums

    #endregion

    #region Delegates
    #endregion

    #region Structures
    #endregion

    #region Classes
    class gun
    {
        public float gunRate, gunTimeReload, gunCountAmmo, gunDamage, gunPayImpuse, gunClipResidue;
        public bool gunReady;
        public string gunName;
        public gun(string GunName, float GunRate, float GunTimeReload, float GunCountAmmo, float GunClipResidue, float GunDamage, bool GunReady, float GunPayImpuse)
        {
            gunRate = GunRate;
            gunTimeReload = GunTimeReload;
            gunCountAmmo = GunCountAmmo;
            gunDamage = GunDamage;
            gunReady = GunReady;
            gunClipResidue = GunClipResidue;
            gunPayImpuse = GunPayImpuse;
            gunName = GunName;
        }
    }
    #endregion

    #region Fields
    [SerializeField]
    private Transform gunSpot;
    [SerializeField]
    private GameObject boolet;
    private int WeaponNumber = 0;
    //private gun guns1;
    [SerializeField]
    private List<gun> Weapons = new List<gun>();

    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    private void Start()
    {
        Weapons.Add(new gun("Hand Gun", 0.6f, 2f, 12, 12, 7, true, 2));
        Weapons.Add(new gun("Rifle", 0.2f, 5f, 30, 30, 5, true, 5));
    }
    private void Update()
    {
        controllKeys();
    }
    private void controllKeys()//клавиши контроля
    {
        if (Input.GetKey(KeyCode.RightControl))
        {
            gunShoot(Weapons[WeaponNumber]);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            switchWeapon(0);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            switchWeapon(1);
        }
    }
    private void gunShoot(gun gunCurrent)//выстрел
    {
        if (gunCurrent.gunReady && gunCountAmmo(gunCurrent))//если оружие готово и есть патроны
        {
            createBoolet();
            gunCurrent.gunReady = false;
            StartCoroutine(gunFireRate(gunCurrent));//такт затвора
        }
    }
    private void createBoolet()
    {
        GameObject booletClone = Instantiate(boolet, gunSpot.position, Quaternion.identity);
        booletClone.transform.forward = transform.forward;
    }
    IEnumerator gunReload(gun gunCurrent)//перезарядка
    {
        yield return new WaitForSeconds(gunCurrent.gunTimeReload);
        Debug.Log("Weapon redy toFire");
        gunCurrent.gunClipResidue = gunCurrent.gunCountAmmo;
        gunCurrent.gunReady = true;
    }
    private bool gunCountAmmo(gun gunCurrent)
    {
        if ((gunCurrent.gunClipResidue - gunCurrent.gunPayImpuse) > 0)//если в обойме минус цена выстрела заряда больше нуля
        {
            gunCurrent.gunClipResidue -= gunCurrent.gunPayImpuse;
            return true;
        }
        Debug.Log("Energo colls exhausted, wait regeneration");
        gunCurrent.gunReady = false;
        StartCoroutine(gunReload(gunCurrent));
        //если меньше нуля включаем перезарядку
        return false;
    } //учет боезапаса
    IEnumerator gunFireRate(gun gunCurrent)
    {
        yield return new WaitForSeconds(gunCurrent.gunRate);
        Debug.Log("next boolet");
        gunCurrent.gunReady = true;
    } //такт затвора
    public void switchWeapon(int weaponNumber)//выбраное оружие
    {
        if (WeaponNumber == weaponNumber) { return; }
        Mathf.Clamp(weaponNumber, 0, Weapons.Count);
        WeaponNumber = weaponNumber;
        Debug.Log(Weapons[WeaponNumber].gunName.ToString());
    }
    #endregion

    #region Event Handlers
    #endregion
}
