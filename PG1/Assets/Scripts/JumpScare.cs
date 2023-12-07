using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public AudioClip triggerSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            PlaySoundAndDisappear();
        }
    }

    private void PlaySoundAndDisappear()
    {
        if (triggerSound != null){
            AudioSource.PlayClipAtPoint(triggerSound, transform.position);
        }

        Destroy(gameObject);
    }
}
