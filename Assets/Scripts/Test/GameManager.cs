using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
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
    public GameObject player;
    public Transform spawnPoint;
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    private void Start()
    {
        GameObject play = (GameObject)FindObjectOfType(typeof(HiroScript));
        if (play == null)
        {
            Camera.main.GetComponent<CameraController>().setTarget(Instantiate(player,spawnPoint.position,Quaternion.identity));
        }
    }
    #endregion

    #region Event Handlers
    #endregion
}
