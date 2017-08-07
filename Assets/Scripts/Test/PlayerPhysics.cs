using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour
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
    public LayerMask collisionMask;

    private BoxCollider collider;
    private Vector3 Size;
    private Vector3 Centre;

    private Vector3 originalSize;
    private Vector3 originalCentre;
    private float colliderScale;

    private int collisionDivisionX=3;
    private int collisionDivisionY=10;

    private float skin = .005f;

    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public bool movemenentStoped;

    Ray ray;
    RaycastHit hit;
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        colliderScale = transform.localScale.x;
        originalSize = collider.size;
        originalCentre = collider.center;
        SetCollider(originalSize, originalCentre);
    }
    public void Move(Vector2 moveAmout)
    {
        float deltaY = moveAmout.y;
        float deltaX = moveAmout.x;
        Vector2 positionHiro = transform.position;

        grounded = false;

        //ccheck collisions up down
        for (int i = 0; i < collisionDivisionX; i++)
        {
            float dir = Mathf.Sign(deltaY);
            float x = (positionHiro.x + Centre.x - Size.x / 2) + Size.x / (collisionDivisionX - 1) * i;
            float y = positionHiro.y + Centre.y + Size.y / 2 * dir;

            ray = new Ray(new Vector2(x, y), new Vector2(0, dir));
            Debug.DrawRay(ray.origin, ray.direction);

            if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + skin, collisionMask))
            {
                float dst = Vector3.Distance(ray.origin, hit.point);

                if (dst > skin)
                {
                    deltaY = dst * dir - skin * dir;
                }
                else
                {
                    deltaY = 0;
                }
                grounded = true;
                break;
            }
        }
        //check collisions lef and right
        movemenentStoped = false;
        for (int i = 0; i < collisionDivisionY; i++)
        {
            float dir = Mathf.Sign(deltaX);
            float x = positionHiro.x + Centre.x + Size.x / 2 * dir;
            float y = positionHiro.y + Centre.y - Size.y / 2+Size.y/ (collisionDivisionY - 1) * i;

            ray = new Ray(new Vector2(x, y), new Vector2(dir, 0));
            Debug.DrawRay(ray.origin, ray.direction);

            if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaX)+skin, collisionMask))
            {
                float dst = Vector3.Distance(ray.origin, hit.point);

                if (dst > skin)
                {
                    deltaX = dst * dir - skin * dir;
                }
                else
                {
                    deltaX = 0;
                }
                movemenentStoped = true;
                break;
            }
        }
        if (!grounded && movemenentStoped)
        {
            Vector3 playerDir = new Vector3(deltaX, deltaY);
            Vector3 o = new Vector3(positionHiro.x + Centre.x - Size.x / 2 + Size.x / 2 * Mathf.Sign(deltaX), positionHiro.y + Centre.y + Size.y / 2 * Mathf.Sign(deltaY));
            ray = new Ray(o, playerDir.normalized);

            if (Physics.Raycast(ray, Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY), collisionMask))
            {
                grounded = true;
                deltaY = 0;
            }
        }

        Vector2 finalTransform = new Vector2(deltaX, deltaY);
        transform.Translate(finalTransform);
    }
    public void SetCollider(Vector3 size, Vector3 center)
    {
        collider.size = size;
        collider.center = center;
        Size = size * colliderScale;
        Centre = center * colliderScale; 

    }

    #endregion


    #region Event Handlers
    #endregion
}
