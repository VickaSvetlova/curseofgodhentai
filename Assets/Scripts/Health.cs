using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageble
{
    #region Enums
    #endregion

    #region Delegates
    #endregion

    #region Structures
    #endregion

    #region Classes
    class Person
    {
        public float HealthMax, HealthCurrent, SheeldMax, SheeldCurrent;
        public Person(float healthMax, float healthCurrent, float sheeldMax, float sheeldCurrent)
        {
            HealthMax = healthMax;
            HealthCurrent = healthCurrent;
            SheeldMax = sheeldMax;
            SheeldCurrent = sheeldCurrent;
        }
    }
    #endregion

    #region Fields
    /*
     * максимально щит
     * текущий щит
     * максимально жизнь
     * текущая жизнь
     * 
     * 
    */
    Person Pers;


    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    /*
     * повреждение
     * 
     * 
     * смерть
     * лечение
     * востановление щитов
     */
    private void Start()
    {
        Person Hiro = new Person(100, 100, 50, 50);
        Pers = Hiro;
    }
    public void onDamage(float rangeDamage)
    {
        if (Dead(Pers, rangeDamage))
        {
            
            Pers.HealthCurrent -= rangeDamage;
        }
    }
    private bool Dead(Person pers,float damage)
    {
        if ((pers.HealthCurrent - damage) > 0)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region Event Handlers
    #endregion
}
