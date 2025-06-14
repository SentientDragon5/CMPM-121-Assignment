using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public SpellCaster spellcaster;
    public List<SpellUI> spellui;

    // =======  events  ======== 
    // on take damage is in hp.
    /// <summary>
    /// called every second that the player stand still
    /// </summary>
    public UnityEvent<int> onStandStill = new();
    float standStillTimer = 0;
    public UnityEvent onMove = new();
    // called in line 98 on spell.cs
    public UnityEvent onKill = new();

    // ======= reactions =======
    /// <summary>
    /// adds to the current spellpower
    /// </summary>
    /// <param name="bonus"></param>
    public void SetBonusSpellpower(int bonus)
    {
        spellcaster.SetBonusSpellPower(bonus);
    }
    public void GainHealth(int amount)
    {
        hp.hp += amount;
    }
    public void GainMana(int amount) => spellcaster.GainMana(amount);

    public void OnTest(int a = 0) => Debug.Log(a);
    public void OnTest() => Debug.Log("a");

    //thought i was special w the movement speed relic but yall alr got that crazy
    public int speed;
    public void SpeedUp(int amount) => speed += amount;

    public Unit unit;

    // Player relic list
    public List<Relic> relics = new List<Relic>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //async Task Start()
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        unit = GetComponent<Unit>();
        GameManager.Instance.player = gameObject;
        GameManager.Instance.onNextWave.AddListener(OnNextWave);
        playerMovement.enabled = false;
    }

    public void StartLevel()
    {
        playerMovement.enabled = true;
        spellcaster = new SpellCaster(125, 8, Hittable.Team.PLAYER);
        StartCoroutine(spellcaster.ManaRegeneration());

        hp = new Hittable(100, Hittable.Team.PLAYER, gameObject);
        hp.OnDeath += Die;
        hp.team = Hittable.Team.PLAYER;

        // Add new spells here
        SpellBuilder spellBuilder = new SpellBuilder();
        spellcaster.AddSpell(spellBuilder.BuildSpell("arcane_bolt", spellcaster));
        spellcaster.AddSpell(spellBuilder.BuildSpell("arcane_spray", spellcaster));
        spellcaster.AddSpell(spellBuilder.BuildSpell("magic_missile", spellcaster));

        // testing modifiers here

        //spellcaster.AddSpell(spellBuilder.BuildSpell("frost_shard", spellcaster));

        // Test some modifier combinations
        // Spell arcaneBolt = spellBuilder.BuildSpell("arcane_bolt", spellcaster);
        // Spell slugBolt = new FreezeModifier(arcaneBolt, spellcaster);
        // spellcaster.AddSpell(slugBolt);

        // Spell magicMissile = spellBuilder.BuildSpell("arcane_blast", spellcaster);
        // Spell freezeMissile = new RapidModifier(magicMissile, spellcaster);
        // spellcaster.AddSpell(freezeMissile);

        // Spell arcaneSpray = spellBuilder.BuildSpell("arcane_bolt", spellcaster);
        // Spell freezeSpray = new SlugModifier(arcaneSpray, spellcaster);
        // spellcaster.AddSpell(freezeSpray);

        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellcaster);
        RefreshSpellUI();

        RelicSystem.Instance.Initialize(this);

        //Debug.Log("Started level with 5 spells. Press 1-5 to switch between them.");

    }

    public UnityEvent onDropSpell = new();
    public UnityEvent onTakeSpell = new();
    public bool CanCarryMoreSpells { get {return spellcaster.equippedSpells.Count < 4;} }
    void RefreshSpellUI()
    {
        var spells = spellcaster.equippedSpells;
        int i = 0;
        for (i = 0; i < spells.Count; i++)
        {
            spellui[i].gameObject.SetActive(true);
            spellui[i].SetSpell(spells[i]);
            if (spells.Count == 1)
                spellui[i].dropbutton.SetActive(false);
            else
                spellui[i].dropbutton.SetActive(true);
        }
        for (; i < 4; i++)
        {
            spellui[i].gameObject.SetActive(false);
        }
    }


    private Spell reward;
    public Spell Reward { get => reward; }
    public void RollReward()
    {
        reward = GameManager.Instance.spellBuilder.BuildRandomSpell(spellcaster);
    }
    public void ClaimReward()
    {
        
        if (CanCarryMoreSpells)
            AddSpell(reward);
        onTakeSpell.Invoke();
    }

    public void AddSpell(Spell spell)
    {
        spellcaster.AddSpell(spell);
        RefreshSpellUI();
    }
    public void DropSpell(int i)
    {
        spellcaster.RemoveSpell(i);
        RefreshSpellUI();
        onDropSpell.Invoke();
    }

    public UnityEvent onTakeRelic = new();
    private List<Relic> relic;
    public RelicUIManager relicsUI;
    public List<Relic> Relic { get => relic; }
    public void RollRelic()
    {
        relic = RelicSystem.Instance.GetRandomRelics(3);
    }
    public void TookRelic(int ind)
    {
        RelicSystem.Instance.ActivateRelic(relic[ind]);
        relicsUI.PutsRelic(relic[ind]);
        onTakeRelic.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        // testing purposes
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) &&
                spellcaster.equippedSpells.Count > i)
            {
                spellcaster.SelectSpell(i);
                Debug.Log($"Switched to spell: {spellcaster.activeSpell.GetName()}");

            }
        }

        if (unit.movement.magnitude > 0.01f)
        {
            onMove.Invoke();
            standStillTimer = 0;
        }
        else
        {
            // check if the new time just crosses the threshold of an int, then invoke event.
            float t = standStillTimer + Time.deltaTime;
            float nearestInt = Mathf.Round(standStillTimer);
            if (standStillTimer < nearestInt && t > Mathf.Round(standStillTimer))
                onStandStill.Invoke(Mathf.RoundToInt(nearestInt));
            standStillTimer = t;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Test relic system by giving player a random relic
            var availableRelics = RelicSystem.Instance.GetRandomRelics(1);
            if (availableRelics.Count > 0)
            {
                AddRelic(availableRelics[0]);
                Debug.Log($"TEST: Added relic {availableRelics[0].name}");
            }
            else
            {
                Debug.Log("No relics available to add");
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            // Simulate taking damage to test take-damage trigger
            Debug.Log("TEST: Simulating damage taken");
            hp.onTakeDamage.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            // Simulate killing an enemy to test on-kill trigger
            Debug.Log("TEST: Simulating enemy kill");
            onKill.Invoke();
        }
    }

    public void OnAttack(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME ||
        GameManager.Instance.state == GameManager.GameState.GAMEOVER ||
        GameManager.Instance.state == GameManager.GameState.VICTORY) return;
        Vector2 mouseScreen = Mouse.current.position.value;
        //Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        //mouseWorld.z = 0;
        //StartCoroutine(spellcaster.Cast(transform.position, mouseWorld));
        // Change this to come from the hand and go torward camera forward.

        // print("OnAttack " + Camera.main.gameObject.name);
        Vector3 spawnPos = transform.position + Vector3.up;
        Vector3 direction = Camera.main.transform.forward;// * 100 - spawnPos;// hubris.
        direction.Normalize();
        Debug.DrawRay(spawnPos, direction);
        StartCoroutine(spellcaster.Cast(spawnPos, direction));
    }

    public void OnMove(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME ||
        GameManager.Instance.state == GameManager.GameState.GAMEOVER ||
        GameManager.Instance.state == GameManager.GameState.VICTORY) return;
        unit.movement = value.Get<Vector2>() * speed;
    }

    void Die()
    {
        Debug.Log("You Lost");
        GameManager.Instance.GameOver();
    }


    public void OnNextWave()
    {

        float power_scaling_mult = 1.2f;
        hp.SetMaxHP(Mathf.FloorToInt(hp.max_hp * power_scaling_mult));
        spellcaster.SetSpellPower(Mathf.FloorToInt(spellcaster.spellPower * power_scaling_mult));
        spellcaster.SetMaxMana(Mathf.FloorToInt(spellcaster.max_mana * power_scaling_mult));
        spellcaster.mana_reg = Mathf.FloorToInt(spellcaster.mana_reg * power_scaling_mult);
    }

    public void AddRelic(Relic relic)
    {
        relics.Add(relic);
        RelicSystem.Instance.ActivateRelic(relic);
        Debug.Log($"Added relic: {relic.name}");
    }


}
