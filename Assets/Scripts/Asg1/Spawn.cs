using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawn
{
    public string enemy;
    public string count;
    public string hp;
    public string speed;
    public string damage;
    public string delay = "2";
    public List<int> sequence = new List<int> { 1 };
    public string location = "random";
}
