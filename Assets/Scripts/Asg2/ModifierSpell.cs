
using System.Collections;
using UnityEngine;


[System.Serializable]
public class ModifierSpell : Spell {
    public Spell child;

    public ModifierSpell(Spell child, SpellCaster owner) : base(owner)
    {
        this.child = child;
    }
    
    public override string GetName() => child.GetName();
    public override int GetManaCost() => child.GetManaCost();
    public override int GetDamage() => child.GetDamage();
    public override float GetCooldown() => child.GetCooldown();
    public override int GetIcon() => child.GetIcon();
    public override float GetSpeed() => child.GetSpeed();
    public override bool IsReady() => child.IsReady();
    public override string GetTrajectory() => child.GetTrajectory();

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    => child.Cast(where,target,team);

}