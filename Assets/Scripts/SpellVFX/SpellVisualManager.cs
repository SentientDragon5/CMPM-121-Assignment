using UnityEngine;

public class SpellVisualManager : MonoBehaviour
{
    [Header("Visual Configurations")]
    public VisualConfig arcaneConfig;
    public VisualConfig iceConfig;
    public VisualConfig lightningConfig;
    public VisualConfig defaultConfig;
    
    [Header("Modifier Overlays")]
    public Material honingOverlayMaterial;
    public GameObject chaosTrailVFX;
    public GameObject rapidFireVFX;
    public Material hugeSizeMaterial;
    
    private static SpellVisualManager _instance;
    public static SpellVisualManager Instance => _instance;
    
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    
    public VisualConfig GetVisualConfig(string damageType, string spellName = "")
    {
        Debug.Log($"GetVisualConfig called with damageType: '{damageType}', spellName: '{spellName}'");
        
        // Handle special spell cases first
        if (spellName.ToLower().Contains("chain"))
        {
            Debug.Log("Returning lightning config for chain spell");
            return lightningConfig;
        }
            
        // Then handle by damage type
        var config = damageType.ToLower() switch
        {
            "ice" => iceConfig,
            "lightning" => lightningConfig,
            "arcane" => arcaneConfig,
            _ => defaultConfig
        };
        
        Debug.Log($"Returning config: {(config != null ? "Found" : "NULL")} for damage type: {damageType}");
        return config;
    }
}