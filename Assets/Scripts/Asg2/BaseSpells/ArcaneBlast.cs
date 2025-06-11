// Create a new file called ArcaneBlast.cs
using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class ArcaneBlast : Spell
{
    public ArcaneBlast(SpellCaster owner) : base(owner)
    {
    }

    protected override void InitializeAttributes()
    {
        attributes = new SpellAttributes();
    }

    public override IEnumerator Cast(Vector3 spawnPos, Vector3 direction, Hittable.Team team)
    {
        this.team = team;
        last_cast = Time.time;

        GameManager.Instance.projectileManager.CreateProjectile(
            attributes.projectileSprite,
            GetTrajectory(),
            spawnPos,
            direction,
            GetSpeed(),
            OnBlastHit,
            lifetime,
            GetSize(),
            GetDamageType(),  
            GetName(),        
            GetAppliedModifiers()
        );

        yield return new WaitForEndOfFrame();
    }

    private void OnBlastHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            // Deal damage with the main projectile
            int damage = GetDamage();
            Damage.Type damageType = Damage.TypeFromString(attributes.damageType);
            other.Damage(new Damage(damage, damageType));

            GameManager.Instance.totalDamageDealt += damage;
        }

        // Spawn secondary projectiles in all directions
        SpawnSecondaryProjectiles(impact);
    }

    private void SpawnSecondaryProjectiles(Vector3 position)
    {
        int numProjectiles = attributes.GetFinalNumProjectiles();
        float angleStep = 360f / numProjectiles;
        
        float speed = 20f;
        float lifetime = 1.0f;
        int sprite = attributes.projectileSprite;

        // Create projectiles in a circle around the impact point
        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            
            Vector3 direction = new Vector3(
                Mathf.Cos(angle),
                0f,
                Mathf.Sin(angle)
            );

            GameManager.Instance.projectileManager.CreateProjectile(
                sprite,
                "straight",
                position,
                direction,
                speed,
                OnSecondaryHit,
                lifetime,
                GetSize(),
                GetDamageType(), 
                "Secondary " + GetName(),  
                GetAppliedModifiers()  
            );
        }
    }
    private void OnSecondaryHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            int damage = attributes.GetFinalSecondaryDamage(owner.spellPower);
            Damage.Type damageType = Damage.TypeFromString(attributes.damageType);
            other.Damage(new Damage(damage, damageType));

            GameManager.Instance.totalDamageDealt += damage;
        }
    }
}