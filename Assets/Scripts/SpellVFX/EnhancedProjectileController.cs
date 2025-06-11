using UnityEngine;
using System;
using System.Collections;

public class EnhancedProjectileController : MonoBehaviour
{
    [Header("Core Settings")]
    public Vector3 direction;
    public float lifetime;
    public event Action<Hittable, Vector3> OnHit;
    public ProjectileMovement movement;
    
    [Header("Visual Components")]
    public Renderer projectileRenderer;
    public Transform visualContainer;
    public TrailRenderer trailRenderer;

    public Material trailMaterial; 
    public Color trailColorStart;
    public Color trailColorEnd;
    public float trailWidth = 0.3f;

    
    // Visual effect references
    private GameObject activeTrailVFX;
    private GameObject activePersistentVFX;
    private VisualConfig currentVisualConfig;
    
    // Animation variables
    private Vector3 originalScale;
    private float animationTime = 0f;

    private bool hasHit = false;

    
    void Start()
    {
        originalScale = transform.localScale;
        
        // Find renderer on this object or children
        if (projectileRenderer == null)
        {
            projectileRenderer = GetComponent<Renderer>();
            if (projectileRenderer == null)
            {
                projectileRenderer = GetComponentInChildren<Renderer>();
            }
        }
        
        // Find trail renderer on this object or children
        if (trailRenderer == null)
        {
            trailRenderer = GetComponent<TrailRenderer>();
            if (trailRenderer == null)
            {
                trailRenderer = GetComponentInChildren<TrailRenderer>();
            }
        }
        
        Debug.Log($"Projectile Setup - Renderer: {(projectileRenderer != null ? "Found" : "NULL")}, Trail: {(trailRenderer != null ? "Found" : "NULL")}");
    }
    
    void Update()
    {
        // Handle movement
        if (movement != null)
            movement.Movement(transform, direction.normalized);
            
        // Handle visual animations
        if (currentVisualConfig != null)
        {
            HandleVisualAnimations();
        }
        
        animationTime += Time.deltaTime;
    }
    
    public void SetupVisuals(string damageType, string spellName, bool isModified = false, string[] modifiers = null)
    {
        currentVisualConfig = SpellVisualManager.Instance.GetVisualConfig(damageType, spellName);
        
        if (currentVisualConfig != null)
        {
            ApplyVisualConfig(currentVisualConfig);
            
            // Apply modifier overlays
            if (isModified && modifiers != null)
            {
                //ApplyModifierVisuals(modifiers);
            }
        }
    }
    
    public void ApplyVisualConfig(VisualConfig config)
    {
        if (!projectileRenderer) projectileRenderer = GetComponent<Renderer>();
        if (!trailRenderer) trailRenderer = GetComponent<TrailRenderer>();

        if (projectileRenderer && config.material)
        {
            projectileRenderer.material = config.material;
            projectileRenderer.material.color = config.primaryColor;

            if (projectileRenderer.material.HasProperty("_EmissionColor"))
            {
                projectileRenderer.material.SetColor("_EmissionColor", config.primaryColor * config.emissionIntensity);
            }
        }

        if (trailRenderer)
        {
            // Set the material
            if (config.trailMaterial) trailRenderer.material = config.trailMaterial;

            // Set the color gradient
            Gradient g = new Gradient();
            g.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(config.trailColorStart, 0f),
                    new GradientColorKey(config.trailColorEnd, 1f)
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(config.trailColorStart.a, 0f),
                    new GradientAlphaKey(config.trailColorEnd.a, 1f)
                }
            );
            trailRenderer.colorGradient = g;

            // Set width & time
            trailRenderer.startWidth = config.trailStartWidth;
            trailRenderer.endWidth = config.trailEndWidth;
            trailRenderer.time = config.trailTime;
        }
    }

    // private void ApplyModifierVisuals(string[] modifiers)
    // {
    //     foreach (string modifier in modifiers)
    //     {
    //         switch (modifier.ToLower())
    //         {
    //             case "homing":
    //                 if (SpellVisualManager.Instance.honingOverlayMaterial != null)
    //                 {
    //                     // Add glow effect for homing
    //                     var glowRenderer = gameObject.AddComponent<Renderer>();
    //                     glowRenderer.material = SpellVisualManager.Instance.honingOverlayMaterial;
    //                 }
    //                 break;
                    
    //             case "chaos":
    //             case "chaotic":
    //                 if (SpellVisualManager.Instance.chaosTrailVFX != null)
    //                 {
    //                     Instantiate(SpellVisualManager.Instance.chaosTrailVFX, transform);
    //                 }
    //                 break;
                    
    //             case "rapid":
    //                 if (SpellVisualManager.Instance.rapidFireVFX != null)
    //                 {
    //                     Instantiate(SpellVisualManager.Instance.rapidFireVFX, transform);
    //                 }
    //                 break;
                    
    //             case "huge":
    //                 if (SpellVisualManager.Instance.hugeSizeMaterial != null && projectileRenderer != null)
    //                 {
    //                     projectileRenderer.material = SpellVisualManager.Instance.hugeSizeMaterial;
    //                 }
    //                 break;
    //         }
    //     }
    // }
    
    private void HandleVisualAnimations()
    {
        if (currentVisualConfig.rotateProjectile)
        {
            transform.Rotate(currentVisualConfig.rotationSpeed * Time.deltaTime);
        }
        
        if (currentVisualConfig.pulseScale)
        {
            float pulse = 1f + Mathf.Sin(animationTime * currentVisualConfig.pulseSpeed) * currentVisualConfig.pulseIntensity;
            transform.localScale = originalScale * pulse;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("projectile")) return;
        
        if (hasHit) return;
        hasHit = true;

        if (movement is RicochetProjectileMovement ricochet)
        {
            if (ricochet.TryBounce(collision, transform)) 
            {
                hasHit = false; 
                return;
            }
        }

        if (collision.gameObject.CompareTag("unit"))
        {
            var ec = collision.gameObject.GetComponent<EnemyController>();
            if (ec != null)
            {
                if (currentVisualConfig?.impactVFX != null)
                {
                    Instantiate(currentVisualConfig.impactVFX, transform.position, Quaternion.identity);
                }
                
                if (currentVisualConfig?.impactSound != null)
                {
                    AudioSource.PlayClipAtPoint(currentVisualConfig.impactSound, transform.position);
                }
                
                OnHit(ec.hp, transform.position);
            }
            else
            {
                var pc = collision.gameObject.GetComponent<PlayerController>();
                if (pc != null)
                {
                    if (currentVisualConfig?.impactVFX != null)
                    {
                        Instantiate(currentVisualConfig.impactVFX, transform.position, Quaternion.identity);
                    }
                    
                    if (currentVisualConfig?.impactSound != null)
                    {
                        AudioSource.PlayClipAtPoint(currentVisualConfig.impactSound, transform.position);
                    }
                    
                    OnHit(pc.hp, transform.position);
                }
            }
        }
        
        Destroy(gameObject);
    }
    
    public void SetLifetime(float lifetime)
    {
        StartCoroutine(Expire(lifetime));
    }

    IEnumerator Expire(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        // Clean up VFX that might persist
        if (activeTrailVFX != null && activeTrailVFX.transform.parent == transform)
        {
            activeTrailVFX.transform.parent = null;
            Destroy(activeTrailVFX, 2f); // Let trail fade out
        }
    }
}