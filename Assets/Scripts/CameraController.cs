using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
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
    public float Speed = 3;
    [Range(0, 10)]
    public float smoothTime;
    private Vector3 originalCameraPosition;
    private bool shake=false;
    [SerializeField]
    public GameObject Target { get; private set; }
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Target == null) { return; }
        if (shake) { return;}
        cameraMove();
    }

    private void cameraMove()
    {
        float newPosition = Mathf.SmoothDamp(transform.position.z, Target.transform.position.z, ref Speed, smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);
    }
    #region ShakeCam
   public void cameraShaker(float shakeAmt,Collision coll)
    {
        return;
        
        originalCameraPosition = transform.position;
        shakeAmt = coll.relativeVelocity.magnitude * .0025f;
        StartCoroutine(CameraShake(shakeAmt));
        //InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);
        shake = true;
    }

    IEnumerator CameraShake(float shakeAmt)
    {
        if(shakeAmt > 0)
        {
            float quakeAmt = UnityEngine.Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp =transform.position;
            pp.y += quakeAmt; // can also add to x and/or z
            transform.position = pp;
            yield return null;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        transform.position = originalCameraPosition;
        shake = false;
    }


    #endregion

    #endregion

    #region Event Handlers
    #endregion
}
