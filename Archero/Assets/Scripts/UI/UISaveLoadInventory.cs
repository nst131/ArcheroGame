using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

public class UISaveLoadInventory : MonoBehaviour
{
    [SerializeField] private UISensor _uISensor;
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private List<GameObject> _allSlots;
    [SerializeField] private List<GameObject> _allClothesInTheGame;
    private Dictionary<GameObject, int> _getClothes;
    private AmountClothesHasPlayer _allPlayerClothes;

    private string _filePath;
    private static bool _saveNewThing = true;
    public static bool LoadScene = false;

    private void Awake()
    {
        _allPlayerClothes = new AmountClothesHasPlayer();
        _getClothes = new Dictionary<GameObject, int>();

        LoadPlayerInventory();
        SaveNewThing();
    }

    private void Start()
    {
        _filePath = Application.persistentDataPath + "/save.invertorysave";
    }


    private void SaveNewThing()
    {
        if (LoadScene)
        {
            LoadScene = false;
            SavePlayerInventory();

            SceneManager.LoadScene(AllScence.Inventory.ToString());
        }
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
            UIInventory.GameScore = loadInventory.score;

            if (loadInventory.availableСlothesName == null)
                return;
           
            foreach (var item in _allClothesInTheGame)
            {
                for (int j = 0; j < loadInventory.availableСlothesName.Count; j++)
                {
                    if (item.name == loadInventory.availableСlothesName[j])
                    {
                        _getClothes.Add(item, loadInventory.availableСlothesValue[j]);
                        break;
                    }
                }
            }

            foreach (var item in _getClothes)
            {
                if(item.Value >= 1)
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                        GameObject currentItem = Instantiate<GameObject>(item.Key);

                        for (int j = 0; j < loadInventory.clothesActive.Count; j++)
                        {
                            if (currentItem.name == loadInventory.clothesActive[j])
                            {
                                Clothe(currentItem);
                                break;
                            }
                            else if(j==loadInventory.clothesActive.Count - 1)
                            {
                                ToPlaceInCell(currentItem);
                            }
                        }
                    }
                }
            }

            if (!_saveNewThing)
                return;

            Type type = _allPlayerClothes.GetType();
            FieldInfo[] _allClothes = type.GetFields();
            
            foreach (var item in _allClothes)
            {
                for (int i = 0; i < loadInventory.availableСlothesName.Count; i++)
                {
                    if(item.Name == loadInventory.availableСlothesName[i])
                    {
                        if((int)item.GetValue(_allPlayerClothes) != loadInventory.availableСlothesValue[i])
                        {
                            typeof(AmountClothesHasPlayer).InvokeMember(item.Name, 
                                BindingFlags.SetField, null, _allPlayerClothes, new object[] { loadInventory.availableСlothesValue[i] });
                            typeof(AmountClothesHasPlayer).InvokeMember(item.Name,
                                BindingFlags.GetField, null, _allPlayerClothes, new object[] { });
                        }
                        break;
                    }
                }
            }
            _saveNewThing = false;
        }
    }

    public void Clothe(GameObject clothes)
    {
        ClothesData myitem = clothes.GetComponentInChildren<ClothesData>();
        InvertoryCell typeClothes = myitem.TypeCltothes;
        Dictionary<GameObject, InvertoryCell> allSlots = _characterStats.AllSlotsWithKey;
        foreach (var item in allSlots)
        {
            if (item.Value == typeClothes)
            {
                myitem.gameObject.transform.SetParent(item.Key.transform);
                myitem.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            }
        }
    }

    public void ToPlaceInCell(GameObject item)
    {
        for (int i = 0; i < _uISensor.AllCell.Count; i++)
        {
            if (_uISensor.AllCell[i].transform.childCount == 0)
            {
                item.transform.SetParent(_uISensor.AllCell[i].transform);
                item.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                break;
            }
        }
    }

    public void SavePlayerInventory()
    {
        SaveInventory save = new SaveInventory();
        save.score = UIInventory.GameScore;
        save.CheckClothes(_allSlots);

        PutInListClothes(_allPlayerClothes, save);

        string json = JsonUtility.ToJson(save);
        PlayerPrefs.SetString("Inventory", json);
    }

    private void PutInListClothes(AmountClothesHasPlayer clothes, SaveInventory save)
    {
        Type type = clothes.GetType();
        FieldInfo[] Name = type.GetFields();
        List<string> clothesName = new List<string>();
        List<int> clothesValue = new List<int>();


        foreach (var item in Name)
        {
            clothesName.Add(item.Name);
            clothesValue.Add((int)item.GetValue(_allPlayerClothes));
        }

        save.InstalClothes(clothesName, clothesValue);
    }
}

public enum AllClothes
{
    Bow,
    Arrow,
    Pants,
    Boots,
    Armor,
    Helm
}

public class AmountClothesHasPlayer
{
   public static int Bow = 1;
   public static int Arrow = 1;
   public static int Pants = 1;
   public static int Boots = 1;
   public static int Armor = 0;
   public static int Helm = 0;
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
    public int score;

    public List<string> clothesActive = new List<string>();
    
    public void CheckClothes(List<GameObject> items)
    {
        foreach (var item in items)
        {
            if(item.transform.childCount == 1)
            {
                clothesActive.Add(item.transform.GetChild(0).name);
            }
        }
    }

    public List<string> availableСlothesName = new List<string>();
    public List<int> availableСlothesValue = new List<int>();

    public void InstalClothes(List<string> itemsName, List<int> itemsValue)
    {
        for (int i = 0; i < itemsName.Count; i++)
        {
            availableСlothesName.Add(itemsName[i]);
            availableСlothesValue.Add(itemsValue[i]);
        }
    }
}
