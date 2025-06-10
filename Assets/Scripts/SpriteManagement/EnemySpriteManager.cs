using UnityEngine;

public class EnemySpriteManager : IconManager
{
    [SerializeField]
    public GameObject[] models;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.enemySpriteManager = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetModel(int index)
    {
        // Debug.Log($"Getting Model index: {index}");
        return models[index];
    }
}
