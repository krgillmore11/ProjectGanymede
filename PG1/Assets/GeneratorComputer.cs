using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorComputer : MonoBehaviour, IInteractable
{
    [SerializeField] Door finalDoor;
    [SerializeField] DialougueManager dm;
    [SerializeField] PlayerManager pm;
public void Interact(){
    if(pm.Level1Cleared){
        finalDoor.unlocked = true;
        dm.ShowPopup("Module B is now safe for entry", 3);
    }
    else{
        dm.ShowPopup("Module B Life Support needs parts", 3);
    }
}
}
