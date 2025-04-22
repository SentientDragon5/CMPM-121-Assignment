// DataLoader.cs
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class DataLoader : MonoBehaviour
{
    private static DataLoader instance;
    public static DataLoader Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("DataLoader");
                instance = obj.AddComponent<DataLoader>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    public List<Enemy> enemies { get; private set; }
    public List<Level> levels { get; private set; }

    public Enemy FindEnemy(string name) => enemies.Find((Enemy x) => x.name == name);

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        LoadData();
    }

    public void LoadData()
    {
        TextAsset enemiesJson = Resources.Load<TextAsset>("enemies");
        TextAsset levelsJson = Resources.Load<TextAsset>("levels");
        
        if (enemiesJson == null || levelsJson == null)
        {
            Debug.LogError("JSON files not found in Resources folder!");
            return;
        }

        enemies = JsonConvert.DeserializeObject<List<Enemy>>(enemiesJson.text);
        levels = JsonConvert.DeserializeObject<List<Level>>(levelsJson.text);
        
        Debug.Log($"Loaded {enemies.Count} enemies and {levels.Count} levels");
    }

    public Enemy GetEnemyByName(string name)
    {
        return enemies.Find(e => e.name == name);
    }
}