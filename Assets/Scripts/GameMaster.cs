using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour, ICreate
{
    ///синглтон создающий игровые объекты
    ///знает все про всех в рамках своей задачи.
    ///Имеет интерфейс ICreatebel реализующий метод принятия
    ///списка необходимых элементов конечного объекта.
	#region Enums
    #endregion

    #region Delegates
    #endregion

    #region Structures
    #endregion

    #region Classes
    private Person person;
    private State state;
    private Weapon weapons;
    private Harpoon harpoon;
    private JumpEnginea jumpEngine;
    private WeaponSelector weaponSelector;
    private Health health;
    private HiroControll hiroControl;

    public void getTemplateList(TemplateModules template)
    {

    }
    #endregion

    #region Fields
    public GameObject PrefabPeson;
    public Transform spawnPoint;
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    private void Start()
    {
        onBuld();
    }
    private void onBuld()
    {
        var pers = Instantiate(PrefabPeson, spawnPoint.position, Quaternion.identity);
        pers.AddComponent<Person>();
        pers.AddComponent<State>();
        pers.AddComponent<WeaponSelector>();
        pers.AddComponent<JumpEnginea>();
        var Rig = pers.AddComponent<Rigidbody>();
        Rig.freezeRotation = true;
    }
    #endregion

    #region Event Handlers
    #endregion
}