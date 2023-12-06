using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialougueManager : MonoBehaviour
{
    public static DialougueManager Instance;//Singleton, should implement in save/load
    //accessible by all scenes

    [SerializeField] TMP_Text popupText;

    private void Awake(){//Deletes itself if there is already an instance
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void ShowPopup(string message, float duration){
        popupText.text = message;
        StartCoroutine(HidePopupAfterDelay(duration));
    }

    private IEnumerator HidePopupAfterDelay(float delay){
        yield return new WaitForSeconds(delay);
        popupText.text = "";
    }
}
