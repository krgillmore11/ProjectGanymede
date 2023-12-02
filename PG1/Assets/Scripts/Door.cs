using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
    }

public void Interact(){
    Debug.Log("Door Opened");
    anim.SetTrigger("Open");
}
}
