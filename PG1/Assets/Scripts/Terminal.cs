using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable
{

    public Animator anim;
    public PauseMenu saveStation;

    void Start(){
        anim = GetComponent<Animator>();
    }

public void Interact(){
    Debug.Log("Terminal opened");
    anim.SetTrigger("Open");

    if(saveStation != null){
        saveStation.ToggleSaveMenu();
    }
}
}
