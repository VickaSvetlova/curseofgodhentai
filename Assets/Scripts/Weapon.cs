using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    #region Enums

    #endregion

    #region Delegates
    #endregion

    #region Structures
    #endregion

    #region Classes


    #endregion

    #region Fields
    #region weapon preferense
    public float gunRate, gunTimeReload, gunCountAmmo, gunDamage, gunPayImpuse, gunClipResidue;
    public bool gunReady;
    public string gunName;
    #endregion

    private float maxDistance=1000f;
    private RaycastHit rayHitObject;
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    private void Update()
    {
        controllKeys();
    }
    private void controllKeys()//клавиши контроля
    {
        if (Input.GetKey(KeyCode.RightControl))
        {
           // gunShoot(Weapons[WeaponNumber]);
        }
    }
    private void gunShoot()//выстрел
    {
        if (gunReady && gunCountAmmos())//если оружие готово и есть патроны
        {
            createBoolet();
            gunReady = false;
            StartCoroutine(gunFireRate());//такт затвора
        }
    }
    private void createBoolet()
    {
        if (checkHit(transform.forward))
        {
            var player =rayHitObject.collider.GetComponent<IDamageble>();
            if (player != null)
            {
                ///1.какое оружие?
                ///2.урон врагу 
                //var shoot = GetComponent<Shoot>();
                //shoot.gunShoot(gun);
                player.onDamage(gunDamage);
            }
        }
        //GameObject booletClone = Instantiate(boolet, gunSpot.position, Quaternion.identity);
        //booletClone.transform.forward = transform.forward;
    }
    IEnumerator gunReload()//перезарядка
    {
        yield return new WaitForSeconds(gunTimeReload);
        Debug.Log("Weapon redy toFire");
        gunClipResidue = gunCountAmmo;
        gunReady = true;
    }
    //учет боезапаса
    private bool gunCountAmmos()
    {
        if ((gunClipResidue - gunPayImpuse) > 0)//если в обойме минус цена выстрела заряда больше нуля
        {
            gunClipResidue -= gunPayImpuse;
            return true;
        }
        Debug.Log("Energo colls exhausted, wait regeneration");
        gunReady = false;
        StartCoroutine(gunReload());
        //если меньше нуля включаем перезарядку
        return false;
    } 
    //такт затвора
    IEnumerator gunFireRate()
    {
        yield return new WaitForSeconds(gunRate);
        Debug.Log("next boolet");
        gunReady = true;
    }
    //проверка попадания пули
    //-переписать на единичный луч
    private bool checkHit(Vector3 dir)
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, dir, maxDistance);
        Debug.DrawRay(transform.position, dir * maxDistance, Color.green);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Collider Hit = hit.transform.GetComponent<Collider>();
            if (Hit)
            {
                if (hit.collider.tag == "Player")
                {
                    rayHitObject = hit;
                    return Hit;
                }
            }
        }
        return false;
    }
    #endregion
    #region Event Handlers
    #endregion
}
