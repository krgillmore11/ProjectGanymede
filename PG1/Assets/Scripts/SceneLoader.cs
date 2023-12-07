using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class SceneLoader : MonoBehaviour
{
    public void Quit(){
        Application.Quit();
    }

    public void LoadLevel(){
        SceneManager.LoadScene("Level 1");
    }

    public void LoadMain(){
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadDeath(){
        Cursor.lockState = CursorLockMode.None;//unlock cursor
        SceneManager.LoadScene("DeathScreen");
    }

    public void StartNewGame(){
        string saveFilePath = "SaveData.json";
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
        LoadLevel();
    }
}
