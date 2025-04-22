using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public Image level_selector;
    public GameObject button;
    public GameObject enemy;
    public SpawnPoint[] spawnPoints;

    /// <summary>
    /// Find the spawn point by the name. This uses the lowercase name of the enum, that should match in levels.json
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public SpawnPoint FindSpawnPoint(string name) => spawnPoints.ToList().Find((SpawnPoint s) => s.StringName == name);

    public int level = 0;
    public int wave = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject selector = Instantiate(button, level_selector.transform);
        selector.transform.localPosition = new Vector3(0, 130);
        selector.GetComponent<MenuSelectorController>().spawner = this;
        selector.GetComponent<MenuSelectorController>().SetLevel("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(string levelname)
    {
        level_selector.gameObject.SetActive(false);
        // this is not nice: we should not have to be required to tell the player directly that the level is starting
        GameManager.Instance.player.GetComponent<PlayerController>().StartLevel();
        StartCoroutine(SpawnWave());
    }

    public void NextWave()
    {
        StartCoroutine(SpawnWave());
    }


    IEnumerator SpawnWave()
    {
        GameManager.Instance.state = GameManager.GameState.COUNTDOWN;
        GameManager.Instance.countdown = 3;
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.countdown--;
        }
        GameManager.Instance.state = GameManager.GameState.INWAVE;
        var levels = DataLoader.Instance.levels;
        var spawns = levels[level].spawns;
        var skele = DataLoader.Instance.FindEnemy("");
        for (int s = 0; s < spawns.Count; s++)
        {
            var spawn = spawns[s];
            Dictionary<string,int> rpnArgs = new();
            rpnArgs.Add("wave", wave);
            
            int count = spawn.EvalCount(rpnArgs);
            
            Enemy enemy = DataLoader.Instance.FindEnemy(spawn.enemy);
            
            rpnArgs.Add("base", enemy.hp);
            int hp = spawn.EvalHp(rpnArgs);

            rpnArgs.Remove("base");
            rpnArgs.Add("base", enemy.damage);
            int damage = spawn.EvalDamage(rpnArgs);

            for (int i = 0; i < count; ++i)
            {                
                string location = spawn.Locations[ i % spawn.Locations.Count];
                yield return SpawnEnemy(enemy, location, hp, damage);
            }
        }
        
        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
    }

    IEnumerator SpawnEnemy(Enemy enemyInfo, string location, int? hpOverride=null,int? damageOverride=null)
    {
        SpawnPoint spawn_point = location == "random" ? spawnPoints[Random.Range(0, spawnPoints.Length)] : FindSpawnPoint(location);
        Vector2 offset = Random.insideUnitCircle * 1.8f;
                
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);
        new_enemy.name = "Enemy ("+ enemyInfo.name +")";

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(enemyInfo.sprite);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        en.hp = new Hittable(hpOverride==null ? enemyInfo.hp : (int)hpOverride, Hittable.Team.MONSTERS, new_enemy);
        en.speed = enemyInfo.speed;
        en.damage = damageOverride ==null? enemyInfo.damage : (int)damageOverride;
        GameManager.Instance.AddEnemy(new_enemy);
        yield return new WaitForSeconds(0.5f);
    }
}
