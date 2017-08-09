using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptIKAim : MonoBehaviour 
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
    public Transform Target;
    public Vector3 Offset;

    private Animator anim;
    private Transform chest;

    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    private void Start()
    {
        anim = GetComponent<Animator>();
        chest = anim.GetBoneTransform(HumanBodyBones.Head);
    }
    private void LateUpdate()
    {
        chest.LookAt(Target.position);
        chest.rotation = chest.rotation * Quaternion.Euler(Offset);
    }
    #endregion

    #region Event Handlers
    #endregion
}
