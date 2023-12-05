using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;//file saving

[Serializable]
public class SaveData
{
    public float playerX;
    public float playerY;
    public float playerZ;

    //Need to Add Enemies, flags, throwables and interactables
}

public class GameSaveManager : MonoBehaviour{
    public GameObject player;

    public void SaveGameState()
    {
        SaveData data = new SaveData();
        data.playerX = player.transform.position.x;
        data.playerY = player.transform.position.y;
        data.playerZ = player.transform.position.z;


        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText("SaveData.json", jsonData);
    }
}
