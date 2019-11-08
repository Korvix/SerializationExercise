using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    private static readonly string SAVE_NAME = "SAVE";
    [SerializeField]
    private List<GameObjectSerializer> gameObjectSerializers;
    [SerializeField]
    private SaveType saveType;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save(SaveType.REGISTER);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Save(SaveType.FILE);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load(SaveType.REGISTER);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Load(SaveType.FILE);
        }
    }
    public void Save(SaveType saveType)
    {
        if (saveType == SaveType.REGISTER)
        {
            Save save = SerializeObject();
            string json = JsonConvert.SerializeObject(save);
            PlayerPrefs.SetString(SAVE_NAME, json);
            Debug.Log("Saved in register");
        }
        if (saveType == SaveType.FILE)
        {
            Save save = SerializeObject();
            string json = JsonConvert.SerializeObject(save);


            string destination = Application.persistentDataPath + "/save.dat";
            FileStream file;
            if (File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, json);
            file.Close();


            
            Debug.Log("Saved in file");
        }

    }

    public void Load(SaveType saveType)
    {
        if (saveType == SaveType.REGISTER)
        {
            string json = PlayerPrefs.GetString(SAVE_NAME);
            Save save = JsonConvert.DeserializeObject<Save>(json);
            DeserializeObject(save);
            Debug.Log("Loaded from register");
        }
        if (saveType == SaveType.FILE)
        {
            string destination = Application.persistentDataPath + "/save.dat";
            FileStream file;

            if (File.Exists(destination)) file = File.OpenRead(destination);
            else
            {
                Debug.LogError("File not found");
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            string json = (string)bf.Deserialize(file);
            file.Close();

            Save save = JsonConvert.DeserializeObject<Save>(json);
            DeserializeObject(save);

            Debug.Log("Loaded from file");
        }
    }

    private Save SerializeObject()
    {
        Save save = new Save();
        gameObjectSerializers.RemoveAll((element) => element == null);
        for (int i = 0; i < gameObjectSerializers.Count; i++)
        {
            if (gameObjectSerializers[i] != null)
            {
                save = gameObjectSerializers[i].Serialize(save);
            }
        }
        return save;
    }

    private void DeserializeObject(Save save)
    {
        for (int i = 0; i < gameObjectSerializers.Count; i++)
        {
            if (gameObjectSerializers[i] != null)
            {
                gameObjectSerializers[i].CreateObject(save);
            }
        }
    }
}
