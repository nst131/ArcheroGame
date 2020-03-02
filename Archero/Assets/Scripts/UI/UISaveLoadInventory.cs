using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UISaveLoadInventory : MonoBehaviour
{
    [SerializeField] private List<ClothesData> _allClothes;
    [SerializeField] private List<UIInventoryCell> _allSlots;
    private string _filePath;

    private void Awake()
    {
        LoadPlayerInventory();
    }

    private void Start()
    {
        _filePath = Application.persistentDataPath + "/save.invertorysave";
    }

    public void SavePlayerStats()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream stream = new FileStream(_filePath, FileMode.Create);

        SaveStats save = new SaveStats();
        save.Damage = CharacterStats.PlayerCurrentDamage;
        save.Health = CharacterStats.PlayerCurrentHealth;

        binary.Serialize(stream, save);
        stream.Close();
    }

    public void LoadPlayerStats()
    {
        if (!File.Exists(_filePath))
            return;

        BinaryFormatter binary = new BinaryFormatter();
        FileStream stream = new FileStream(_filePath, FileMode.Open);

        SaveStats load = binary.Deserialize(stream) as SaveStats;
        stream.Close();
        PlayerStats.Damage = load.Damage;
        PlayerStats.Health = load.Health;
    }

    public void LoadPlayerInventory()
    {
        string load = PlayerPrefs.GetString("Inventory");
        if(load != null)
        {
            SaveInventory loadInventory = JsonUtility.FromJson<SaveInventory>(load);

            int i = 0;
            foreach (var item in _allClothes)
            {
                item.ClothesActive = loadInventory.clothesActive[i];
                i++;
            }

            foreach (var item in _allSlots)
            {
                item.CheckClothes();
            }
        }
    }

    public void SavePlayerInventory()
    {
        SaveInventory save = new SaveInventory();

        save.CheckClothes(_allClothes);

        string json = JsonUtility.ToJson(save);
        PlayerPrefs.SetString("Inventory", json);
    }
}
[Serializable]
public class SaveStats
{
    public float Damage;
    public float Health;
}
[Serializable]
public class SaveInventory
{
    public List<int> clothesActive = new List<int>();

    public void CheckClothes(List<ClothesData> items)
    {
        foreach (var item in items)
        {
            clothesActive.Add(item.ClothesActive);
        }
    }
}
