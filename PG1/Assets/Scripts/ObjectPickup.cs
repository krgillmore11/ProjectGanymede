using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    public bool holdingItem = false;
    private GameObject heldItem;
    [SerializeField] float throwForce = 10f;
    [SerializeField] float pickupDistance = 5f;

    void Update(){
        if (holdingItem){
            UpdateHeldItemPosition();
        }
    }

    public void Throw(){
        ThrowItem();
    }
       
    public void PickupItem(GameObject item){
        holdingItem = true;
        heldItem = item;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.parent = transform;//attach to player
    }

    public void DropItem(){
        holdingItem = false;
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
        heldItem.transform.parent = null;
        heldItem = null;
    }

    void UpdateHeldItemPosition(){
        heldItem.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 2f;
    }

    void ThrowItem(){
        holdingItem = false;
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
        heldItem.transform.parent = null;

        heldItem.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse);

        heldItem = null;
    }
}
