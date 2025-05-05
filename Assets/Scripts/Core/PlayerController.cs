using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public SpellCaster spellcaster;
    public List<SpellUI> spellui;

    public int speed;

    public Unit unit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit = GetComponent<Unit>();
        GameManager.Instance.player = gameObject;
    }

    public void StartLevel()
    {
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
        spellcaster.AddSpell(spellBuilder.BuildSpell("arcane_blast", spellcaster));

        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellcaster);
        RefreshSpellUI();

        Debug.Log("Started level with 5 spells. Press 1-5 to switch between them.");

    }


    public UnityEvent onDropSpell = new();
    public bool CanCarryMoreSpells {get => spellcaster.equippedSpells.Count < 4;}
    void RefreshSpellUI(){

        var spells = spellcaster.equippedSpells;
        int i=0;
        for (i = 0; i < spells.Count; i++)
        {
            spellui[i].gameObject.SetActive(true);
            spellui[i].SetSpell(spells[i]);
            if(spells.Count == 1)
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
    public Spell Reward {get => reward;}
    public void RollReward(){
        reward = GameManager.Instance.spellBuilder.BuildRandomSpell(spellcaster);
    }
    public void ClaimReward(){
        if (CanCarryMoreSpells)
            AddSpell(reward);
    }

    public void AddSpell(Spell spell){
        spellcaster.AddSpell(spell);
        RefreshSpellUI();
    }
    public void DropSpell(int i){
        spellcaster.RemoveSpell(i);
        RefreshSpellUI();
        onDropSpell.Invoke();
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
    }

    void OnAttack(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || 
        GameManager.Instance.state == GameManager.GameState.GAMEOVER || 
        GameManager.Instance.state == GameManager.GameState.VICTORY) return;
        Vector2 mouseScreen = Mouse.current.position.value;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0;
        StartCoroutine(spellcaster.Cast(transform.position, mouseWorld));
    }

    void OnMove(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || 
        GameManager.Instance.state == GameManager.GameState.GAMEOVER || 
        GameManager.Instance.state == GameManager.GameState.VICTORY) return;
        unit.movement = value.Get<Vector2>()*speed;
    }

    void Die()
    {
        Debug.Log("You Lost");
        GameManager.Instance.GameOver();
    }

}
