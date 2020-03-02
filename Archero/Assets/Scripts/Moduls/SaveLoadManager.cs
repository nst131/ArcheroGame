using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField]private UILevelUp _uiLevelUP;
    private event Action _game;
    private string _filePath;

    private void Start()
    {
        _filePath = Application.persistentDataPath + "/save.gamesave";
        _game += SaveGame;
        _game += LoadGame;
    }

    public void InvokeEventGame()
    {
        _game.Invoke();
    }

    public void SaveGame()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream stream = new FileStream(_filePath, FileMode.Create);

        Save save = new Save();
        save.Score = _uiLevelUP.CurrentCoinGold;

        binary.Serialize(stream, save);
        stream.Close();
    }

    public void LoadGame()
    {
        if (!File.Exists(_filePath))
            return;

        BinaryFormatter binary = new BinaryFormatter();
        FileStream stream = new FileStream(_filePath, FileMode.Open);

        Save save = binary.Deserialize(stream) as Save;
        stream.Close();
        UIInventory.WinScore = save.Score;
    }
}

[Serializable]
public class Save
{
    public int Score;
}

