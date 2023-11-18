using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAnimationToggle : MonoBehaviour
{
    public PlayerController playerController;

    public void OnAttackAnimationComplete(){
        if (playerController != null)
        {
            playerController.isPlayingAnimation = false;
        }
    }
}
