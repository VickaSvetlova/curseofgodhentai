using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
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
    ///
    ///
    ///
    [SerializeField]
    private float maxDistance=100;
    private RaycastHit rayHitObject;

    #endregion 

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    private void Update()
    {
        lookAt();
        

    }

    private void lookAt()
    {
        if (RayCaster(transform.forward))
        {
            var player = rayHitObject.collider.GetComponent<IDamageble>();
            if (player!=null)
            {
                ///1.какое оружие?
                ///2.урон врагу 
                //var shoot = GetComponent<Shoot>();
                //shoot.gunShoot(gun);
                player.onDamage(booletDamage());
            }
        }
    }

    private float booletDamage()
    {
       return 10;
    }

    private bool RayCaster(Vector3 dir)
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, dir, maxDistance);
        Debug.DrawRay(transform.position, dir*maxDistance, Color.green);
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


            ///стрельба
            /// -рейкаст
            /// -проверка в кого
            /// -передача повреждления

            #endregion

            #region Event Handlers
            #endregion
        }
