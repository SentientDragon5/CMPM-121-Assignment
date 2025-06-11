using UnityEngine;

[System.Serializable]
public class VisualConfig
{
    [Header("Material Settings")]
    public Material material;
    public Color primaryColor = Color.white;
    public Color secondaryColor = Color.white;
    public float emissionIntensity = 1f;

    [Header("Trail Settings")]
    public Material trailMaterial;
    public Color trailColorStart = Color.white;
    public Color trailColorEnd = Color.white;
    public float trailStartWidth = 0.3f;
    public float trailEndWidth = 0.1f;
    public float trailTime = 0.5f;
    
    
    [Header("VFX Settings")]
    public GameObject impactVFX;
    public GameObject persistentVFX; 
    
    [Header("Audio")]
    public AudioClip launchSound;
    public AudioClip impactSound;
    
    [Header("Animation")]
    public bool rotateProjectile = false;
    public Vector3 rotationSpeed = Vector3.zero;
    public bool pulseScale = false;
    public float pulseSpeed = 2f;
    public float pulseIntensity = 0.1f;
}