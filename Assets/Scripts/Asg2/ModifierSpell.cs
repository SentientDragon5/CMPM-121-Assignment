
using System.Collections;
using UnityEngine;

public class ModifierSpell : Spell {
    public Spell child;

    public ModifierSpell(Spell child, SpellCaster owner) : base(owner)
    {
        this.child = child;
    }
}