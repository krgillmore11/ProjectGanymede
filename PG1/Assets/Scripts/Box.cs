using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{

    public Animator anim;
    public DialougueManager dm;
    public PlayerManager pm;

    void Start(){
        anim = GetComponent<Animator>();
    }

public void Interact(){
    Debug.Log("Box opened");
    anim.SetTrigger("Open");
    dm.ShowPopup("Generator Component found", 3);
    pm.genParts++;
}
}
