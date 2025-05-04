using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class SpellBuilder 
{
    private Dictionary<string, JObject> spellDefinitions;
    private List<string> baseSpellKeys;
    private List<string> modifierSpellKeys;
    private System.Random random;

    public SpellBuilder()
    {
        random = new System.Random();
        LoadSpellDefinitions();
    }
    
    private void LoadSpellDefinitions()
    {
        spellDefinitions = new Dictionary<string, JObject>();
        baseSpellKeys = new List<string>();
        modifierSpellKeys = new List<string>();
        
        JObject spellsJson = DataLoader.Instance.spells;
        if (spellsJson == null)
        {
            Debug.LogError("Failed to load spells.json");
            return;
        }
        
        foreach (var property in spellsJson.Properties())
        {
            string key = property.Name;
            JObject spellObj = property.Value as JObject;
            
            if (spellObj != null)
            {
                spellDefinitions[key] = spellObj;
                
                // Determine if it's a base spell or a modifier based on name pattern
                if (key == "damage_amp" || key == "speed_amp" || key == "doubler" || 
                    key == "splitter" || key == "chaos" || key == "homing")
                {
                    modifierSpellKeys.Add(key);
                }
                else
                {
                    baseSpellKeys.Add(key);
                }
            }
        }
        
        Debug.Log($"Loaded {baseSpellKeys.Count} base spells and {modifierSpellKeys.Count} modifier spells");
    }
    
    public Spell Build(SpellCaster owner)
    {
        // This is the compatibility method for the original SpellBuilder
        // Just return an ArcaneBolt for now
        return BuildSpell("arcane_bolt", owner);
    }
    
    // Build a spell from a specific key
    public Spell BuildSpell(string key, SpellCaster owner)
    {
        if (!spellDefinitions.ContainsKey(key))
        {
            Debug.LogError($"Spell definition not found for key: {key}");
            return new ArcaneBolt(owner); // Fallback
        }
        
        JObject spellObj = spellDefinitions[key];
        
        // Check if it's a modifier spell
        if (modifierSpellKeys.Contains(key))
        {
            Debug.LogError($"Cannot build a modifier spell directly: {key}");
            return new ArcaneBolt(owner); // Fallback
        }
        
        // Create the base spell based on the key
        Spell spell = CreateSpellInstance(key, owner);
        
        // Set attributes from JSON
        if (spell != null)
        {
            spell.SetAttributesFromJson(spellObj);
            Debug.Log($"Created spell: {spell.GetName()} with damage: {spell.GetDamage()}");
        }
        
        return spell ?? new ArcaneBolt(owner); // Fallback if creation failed
    }
    
    // Build a random spell (possibly with modifiers)
    public Spell BuildRandomSpell(SpellCaster owner)
    {
        // Choose a random base spell
        string baseSpellKey = baseSpellKeys[random.Next(baseSpellKeys.Count)];
        Spell baseSpell = BuildSpell(baseSpellKey, owner);
        
        // Decide how many modifiers to apply (0-3)
        int numModifiers = random.Next(4); // 0 to 3 modifiers
        
        // Apply modifiers
        Spell result = baseSpell;
        for (int i = 0; i < numModifiers; i++)
        {
            if (modifierSpellKeys.Count > 0)
            {
                string modifierKey = modifierSpellKeys[random.Next(modifierSpellKeys.Count)];
                result = ApplyModifier(modifierKey, result, owner);
            }
        }
        
        return result;
    }
    
    // Apply a modifier to a spell
    private Spell ApplyModifier(string modifierKey, Spell baseSpell, SpellCaster owner)
    {
        if (!spellDefinitions.ContainsKey(modifierKey))
        {
            Debug.LogError($"Modifier definition not found for key: {modifierKey}");
            return baseSpell;
        }
        
        JObject modifierObj = spellDefinitions[modifierKey];
        
        // Create the modifier instance
        Spell modifierSpell = CreateModifierInstance(modifierKey, baseSpell, owner);
        
        // Set attributes from JSON
        if (modifierSpell != null)
        {
            modifierSpell.SetAttributesFromJson(modifierObj);
        }
        
        return modifierSpell ?? baseSpell;
    }
    
    // Create a spell instance based on the key
    private Spell CreateSpellInstance(string key, SpellCaster owner)
    {
        // Map spell keys to their class implementations
        switch (key.ToLower())
        {
            case "arcane_bolt":
                return new ArcaneBolt(owner);
            case "arcane_spray":
                return new ArcaneSpray(owner);
            default:
                Debug.LogError($"Unknown base spell key: {key}");
                return new ArcaneBolt(owner); // currently working spell
        }
    }
    
    // Create a modifier instance based on the key
    private Spell CreateModifierInstance(string key, Spell baseSpell, SpellCaster owner)
    {
        // You'll implement this later when you add modifier spells
        Debug.LogError($"Modifier spells not implemented yet: {key}");
        return null;
    }
}