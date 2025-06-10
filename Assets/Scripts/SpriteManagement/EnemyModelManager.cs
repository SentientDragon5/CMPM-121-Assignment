using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyModelManager", menuName = "Game/Enemy Model Manager")]
public class EnemyModelManager : ScriptableObject
{
    [System.Serializable]
    public class EnemyModelEntry
    {
        public string name;
        public GameObject prefab;
    }

    public List<EnemyModelEntry> models;

    private Dictionary<string, GameObject> modelLookup;

    public GameObject Get(string name)
    {
        if (modelLookup == null)
        {
            modelLookup = new Dictionary<string, GameObject>();
            foreach (var entry in models)
            {
                if (!modelLookup.ContainsKey(entry.name))
                    modelLookup.Add(entry.name, entry.prefab);
            }
        }

        if (modelLookup.TryGetValue(name, out var prefab))
            return prefab;

        Debug.LogWarning($"Model '{name}' not found in EnemyModelManager.");
        return null;
    }
}