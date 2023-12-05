using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameLoadManager : MonoBehaviour
{
    public GameObject player;

    /*void Start()
    {
        LoadGameState();
    }*/

    public void LoadGameState(){
        if (File.Exists("SaveData.json")){
            string jsonData = File.ReadAllText("SaveData.json");
            SaveData data = JsonUtility.FromJson<SaveData>(jsonData);

            //Load Player position
            player.transform.position = new Vector3(data.playerX, data.playerY, data.playerZ);
        }
    }
}
