using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject saveMenu;

    private bool isPaused = false;

    public void TogglePauseMenu(){
        isPaused = !isPaused;

        if (isPaused){
            Time.timeScale = 0f; 
            pauseMenu.SetActive(true); 
            Cursor.lockState = CursorLockMode.None;//unlock cursor

        }
        else{
            Time.timeScale = 1f; 
            pauseMenu.SetActive(false); 
            Cursor.lockState = CursorLockMode.Locked;//Lock cursor off screen

        }
    }

    public void ToggleSaveMenu(){
        isPaused = !isPaused;

        if (isPaused){
            Time.timeScale = 0f; 
            saveMenu.SetActive(true); 
            Cursor.lockState = CursorLockMode.None;//unlock cursor

        }
        else{
            Time.timeScale = 1f; 
            saveMenu.SetActive(false); 
            Cursor.lockState = CursorLockMode.Locked;//Lock cursor off screen

        }
    }


}
