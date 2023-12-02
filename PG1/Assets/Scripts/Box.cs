using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{

    public Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
    }

public void Interact(){
    Debug.Log("Box opened");
    anim.SetTrigger("Open");
}
}
