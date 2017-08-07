using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerPhysics))]

public class PlayerController : MonoBehaviour
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
    public float Gravity = 20;
    public float WalkSpeed=8;
    public float RunSpeed = 16;
    public float Accseleration=30;
    public float jumpHeight=12;

    private float currentSpeed;
    private float targetSpeed;
    private Vector2 amoutToMove;
    private PlayerPhysics playerPhusics;
    

    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    private void Start()
    {
        playerPhusics = GetComponent<PlayerPhysics>();
    }
    private void Update()
    {
        if (playerPhusics.movemenentStoped)
        {
            targetSpeed = 0;
            currentSpeed = 0;
        }
        //input
        float speed = ((Input.GetButton("Run")) ? RunSpeed :WalkSpeed);
        targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
       // Debug.Log(Input.GetAxisRaw("Horizontal") * Speed);
        currentSpeed = IncrementTowards(currentSpeed, targetSpeed, Accseleration);
        


        if (playerPhusics.grounded)
        {
            amoutToMove.y = 0;
            //jump
            if (Input.GetButton("Jump"))
            {
                amoutToMove.y = jumpHeight;
            }
        }

        amoutToMove.x = currentSpeed;
        amoutToMove.y -= Gravity * Time.deltaTime;
        playerPhusics.Move(amoutToMove * Time.deltaTime);

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
    #endregion
    #region Event Handlers
    #endregion
}

