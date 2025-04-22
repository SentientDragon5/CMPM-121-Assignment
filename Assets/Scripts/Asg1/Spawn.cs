using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RPNEvaluator;


/// <summary>
/// to read some of the fields of this class, you should use the evaluate functions,
/// as their values depend on variables like wave or a base value.
/// </summary>
[System.Serializable]
public class Spawn
{
    public string enemy;
    public string count;
    public int EvalCount( Dictionary<string, int> variables = null) => Evaluate(count, variables);

    public string hp;
    public int EvalHp( Dictionary<string, int> variables = null) => Evaluate(hp, variables);

    public string damage;
    public int EvalDamage( Dictionary<string, int> variables = null) => Evaluate(damage, variables);
    public string delay = "2";
    public int EvalDelay( Dictionary<string, int> variables = null) => Evaluate(delay, variables);

    public List<int> sequence = new List<int> { 1 };
    public string location = "random";
    public List<string> Locations {get => location.Split(' ').ToList<string>(); }
}
