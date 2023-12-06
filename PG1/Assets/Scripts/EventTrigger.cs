using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public AudioClip soundEffect;
    public string popupMessage;
    public float popupDuration = 3f;
    public string flagName;
    [SerializeField] FlagManager flagManager;

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            if (soundEffect != null){
                AudioSource.PlayClipAtPoint(soundEffect, transform.position);
            }

            if (!string.IsNullOrEmpty(popupMessage)){
                DialougueManager.Instance.ShowPopup(popupMessage, popupDuration);
            }

            if (!string.IsNullOrEmpty(flagName)){
            flagManager.flags[flagName] = true;
            }

            //disable after triggered
            gameObject.SetActive(false);
        }
    }
}
