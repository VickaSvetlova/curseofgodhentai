using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class booletScript : MonoBehaviour
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
    /// <summary>
    /// дамаг
    /// скорость
    /// 
    /// </summary>
    #endregion

    #region Events
    public UnityEvent destroy;
    #endregion

    #region Properties
    #endregion

    #region Methods
    #region shakeCam;
  
    #endregion

    private void Start()
    {
        // Destroy(gameObject, 2);
        GetComponent<Rigidbody>().AddForce(transform.forward*1000);
    }
    private void Update()
    {
       
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            float shakeAmt = GetComponent<Rigidbody>().velocity.magnitude;
            Camera.main.GetComponent<CameraController>().cameraShaker(shakeAmt, collision);
            Destroy(gameObject);
        }
        
    }
    #endregion

    #region Event Handlers
    #endregion
}
