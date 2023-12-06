using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Kit : MonoBehaviour, IInteractable
{
    public PlayerManager pm;
    [SerializeField] int healAmount;

    public void Interact(){
        pm.heal(healAmount);
        Destroy(gameObject);
    }
}
