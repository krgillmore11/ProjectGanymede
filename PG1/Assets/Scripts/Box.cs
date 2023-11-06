using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField] Animator anim;

public void Interact(){
    Debug.Log("Box opened");
    anim.SetTrigger("Open");
}
}
