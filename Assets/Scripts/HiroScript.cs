using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiroScript : MonoBehaviour
{

    #region Enums
    private enum direction { slide, left, rigth, up, down, jump };
    private direction Direction;
    #endregion

    #region Delegates
    #endregion

    #region Structures
    #endregion

    #region Classes
    #endregion

    #region Fields
    ///характеристики
    public float Speed;
    private bool jump;
    private bool fall = false;
    private bool slide = true;
    private Rigidbody rigBody;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 direct;
    private int gunNumberCurrent = 0;
    [SerializeField]
    private float JumpPower = 60;
    //оружие
    [SerializeField]
    private Weapon[] weapon;
    public Transform gunPoint;
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    private void Start()
    {
        rigBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        controllKey();
    }
    #region ///перемещения
    private void controllKey()
    {
        //кнопки контроля перемещения. влево вправо верх вниз вперед назад

        //если земля под ногами
        if (!fall)
        {
            if (Input.GetKey(KeyCode.D))
            {
                MovePlayer(direction.rigth); //идем в право

            }
            if (Input.GetKey(KeyCode.A))
            {
                MovePlayer(direction.left); //идем в лево
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                slide = true;
                MovePlayer(direction.slide); //скольжение
            }
        }
        if (Input.GetKey(KeyCode.Space)) //пробуем прыгнуть
        {
            if (isOnGround(Vector3.down)) //если земля под ногами
            {
                fall = false;
                MovePlayer(direction.jump); //прыгаем            
            }
        }
        if (Input.GetKey(KeyCode.RightControl))
        {
            if (weapon == null) { return; }
            Mathf.Clamp(gunNumberCurrent, 0, weapon.Length);
            onShoot(weapon[gunNumberCurrent]);
        }
    }
    #region ///стрелять из оружия
    private void onShoot(Weapon wepons)
    {
        wepons.gunShoot(gunPoint);
    }
    #endregion

    private bool isOnGround(Vector3 dir)//если земля под ногами
    {
        if (fall == true) return false;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, dir, 1);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Collider Hit = hit.transform.GetComponent<Collider>();
            Debug.DrawRay(transform.position, dir, Color.blue);
            if (Hit)
            {
                if (hit.collider.tag == "ground")
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void MovePlayer(direction dir)
    {
        switch (dir)
        {

            case direction.rigth:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                //transform.Translate(transform.forward * Time.deltaTime * Speed);
                rigBody.AddForce(Vector3.forward * Time.deltaTime * Speed);
                break;
            case direction.left:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                //transform.Translate(transform.forward * Time.deltaTime * -Speed);
                rigBody.AddForce(Vector3.forward * Time.deltaTime * -Speed);
                break;
            case direction.up:

                break;
            case direction.down:

                break;
            case direction.jump:
                if (!fall) //если не падаем
                {
                    fall = true;
                    if (Vector3.Dot(directionMove(transform.position, startPos), Vector3.forward) > 0)
                    {
                        rigBody.AddForce(new Vector3(0, JumpPower / rigBody.velocity.magnitude, rigBody.velocity.magnitude));
                    }
                    else if (Vector3.Dot(directionMove(transform.position, startPos), Vector3.forward) < 0)
                    {
                        rigBody.AddForce(new Vector3(0, JumpPower / rigBody.velocity.magnitude, -rigBody.velocity.magnitude));
                    }; //определяем направление движения                    
                    rigBody.AddForce(new Vector3(0, JumpPower, 0));

                }
                /*   если мы не в воздухе то 
                 *
                 *   если ПРЫЖОК то импульс вверх
                  *  если ПРЫЖОК и  ВПЕРЕД то прыжок вперед
                  *  если ИМПУЛЬС  не использован и ПАДЕНИЕ и ВПЕРЕД - то импульсв вперед                
                  *  ИМПУЛЬС - возможность однократного добавления ускорения в прыжке увеличивает дальность
                  *  ВПЕРЕД - движение в какую либо сторону.
                  *  ПАДЕНИЕ - Когда герой не касается земли
                  */
                break;
            case direction.slide:

                break;

        }
    }

    private Vector3 directionMove(Vector3 endingPos, Vector3 starterPos)
    {
        direct = endingPos - starterPos;
        return Vector3.Normalize(direct);
    }
    #endregion
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "ground")
        {
            fall = false;
        }
    }
    ///смотреть
    #region///выбирать оружиe
    #endregion

    ///использовать прыжковый двигатель
    ///использовать гарпун
    #endregion

    #region Event Handlers
    #endregion
}
