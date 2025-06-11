using UnityEngine;
using System;

public class ProjectileManager : MonoBehaviour
{
    public GameObject[] projectiles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.projectileManager = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // public void CreateProjectile(int which, string trajectory, Vector3 where, Vector3 direction, float speed, Action<Hittable,Vector3> onHit)
    // {
    //     GameObject new_projectile = Instantiate(projectiles[which], where + direction.normalized*1.1f, Quaternion.Euler(0,0,Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg));
    //     new_projectile.GetComponent<ProjectileController>().movement = MakeMovement(trajectory, speed);
    //     new_projectile.GetComponent<ProjectileController>().OnHit += onHit;
    // }

    // public void CreateProjectile(int which, string trajectory, Vector3 where, Vector3 direction, float speed, Action<Hittable, Vector3> onHit, float lifetime)
    // {
    //     GameObject new_projectile = Instantiate(projectiles[which], where + direction.normalized * 1.1f, Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
    //     new_projectile.GetComponent<ProjectileController>().movement = MakeMovement(trajectory, speed);
    //     new_projectile.GetComponent<ProjectileController>().OnHit += onHit;
    //     new_projectile.GetComponent<ProjectileController>().SetLifetime(lifetime);
    // }
// Add this overload to your existing ProjectileManager
    public void CreateProjectile(int which, string trajectory, Vector3 spawnPos, Vector3 direction, 
        float speed, Action<Hittable, Vector3> onHit, float lifetime, float size, 
        string damageType = "arcane", string spellName = "", string[] modifiers = null)
    {
        print("Create projectile called with visuals");
        Debug.DrawRay(spawnPos, direction, Color.magenta, 0.2f);
        
        GameObject newProjectile = Instantiate(projectiles[which], spawnPos + direction.normalized * 1.1f, Quaternion.identity);
        
        // Try enhanced controller first
        var enhancedController = newProjectile.GetComponent<EnhancedProjectileController>();
        if (enhancedController != null)
        {
            enhancedController.movement = MakeMovement(trajectory, speed);
            enhancedController.OnHit += onHit;
            enhancedController.SetLifetime(lifetime);
            enhancedController.direction = direction;
            
            bool isModified = modifiers != null && modifiers.Length > 0;
            enhancedController.SetupVisuals(damageType, spellName, isModified, modifiers);
        }
        // else
        // {
        //     // Fallback to existing controller
        //     var controller = newProjectile.GetComponent<ProjectileController>();
        //     controller.movement = MakeMovement(trajectory, speed);
        //     controller.OnHit += onHit;
        //     controller.SetLifetime(lifetime);
        //     controller.direction = direction;
        // }
        
        newProjectile.transform.localScale = new Vector3(size, size, size);
    }

    public ProjectileMovement MakeMovement(string name, float speed)
    {
        if (name == "straight")
        {
            return new StraightProjectileMovement(speed);
        }
        if (name == "homing")
        {
            return new HomingProjectileMovement(speed);
        }
        if (name == "spiraling")
        {
            return new SpiralingProjectileMovement(speed);
        }
        if (name == "wavy")
        {
            return new WavyProjectileMovement(speed);
        }
        if (name == "ricochet")
        {
            return new RicochetProjectileMovement(speed);
        }
        return null;
    }

}
