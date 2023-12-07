using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public Animator anim;
    public bool unlocked = true;
    public DialougueManager dm;
    public string messageString = "Door is locked";

    void Start(){
        anim = GetComponent<Animator>();
    }

    public void Interact(){
        if(unlocked){
            Debug.Log("Door Opened");
            anim.SetTrigger("Open");
            gameObject.layer = 2;
        }   
        else{
            if(dm != null){
                dm.ShowPopup(messageString, 3);
            }
        }
    }
}
