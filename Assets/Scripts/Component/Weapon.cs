using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{

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
    [SerializeField]
    private string gunName;
    [SerializeField]
    private float gunRate, gunTimeReload, gunCountAmmo, gunClipResidue, gunDamage, gunPayImpuse;
    //[SerializeField]
    private bool gunReady = true;
    private Transform gunSpot;
    #endregion

    private float maxDistance = 1000f;
    private RaycastHit rayHitObject;
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    public void gunShoot(Transform gunPoint)//выстрел
    {
        gunSpot = gunPoint;
        if (gunReady && gunCountAmmos())//если оружие готово и есть патроны
        {            
            createBoolet();                          
        }
    }
    private void createBoolet()
    {

        if (checkHit(transform.forward))
        {
            var objects = rayHitObject.collider.GetComponent<IDamageble>();
            if (objects != null)
            {               
                objects.onDamage(gunDamage);
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
            gunReady = false;
            StartCoroutine(gunFireRate());//такт затвора
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
        hits = Physics.RaycastAll(gunSpot.transform.position, dir, maxDistance);
        Debug.DrawRay(gunSpot.transform.position, dir * maxDistance, Color.red);
        //for (int i = 0; i < hits.Length; i++)
        //{
        RaycastHit hit = hits[0];
        Collider Hit = hit.transform.GetComponent<Collider>();
        if (Hit)
        {
            rayHitObject = hit;
            return Hit;
        }
        // }        
        return false;
    }
    #endregion
    #region Event Handlers
    #endregion
}
