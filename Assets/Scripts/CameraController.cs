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
    private float tracSpeed=10;

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
       // Target = GameObject.FindGameObjectWithTag("Player");
    }
    private void LateUpdate()
    {
        if (Target == null) { return; }

        float x = IncrementTowards(transform.position.x, Target.transform.position.x, tracSpeed);
        float y = IncrementTowards(transform.position.y, Target.transform.position.y, tracSpeed);
        transform.position = new Vector3(x, y,transform.position.z);
    }

    
    public void setTarget(GameObject target)
    {
        Target = target;
    }

    private void cameraMove()
    {
        float newPositionX = Mathf.SmoothDamp(transform.position.x, Target.transform.position.x, ref Speed, smoothTime);
        float newPositionY = Mathf.SmoothDamp(transform.position.y, Target.transform.position.y, ref Speed, smoothTime);
        transform.position = new Vector3(newPositionX, newPositionY, transform.position.z);
    }
    private float IncrementTowards(float CurSpeed, float TSpeed, float Accsel)
    {
        if (CurSpeed == TSpeed)
        {
            return CurSpeed;
        }
        else
        {
            float dir = Mathf.Sign(TSpeed - CurSpeed);
            CurSpeed += Accsel * Time.deltaTime * dir;
            return (dir == Mathf.Sign(TSpeed - CurSpeed)) ? CurSpeed : TSpeed;
        }
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
