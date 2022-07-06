using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill 
{

    public Character character;
    public int skillLevel; // ??
    public int upgradeCost; // ??
    public int useCost;

   // public abstract int UseCost {get:  };
    public abstract void UseSkill();
    public Skill (int cost)
    {
        useCost = cost;
    }

    public virtual void Initialize(Character player)
    {
        character = player;
    }

    public virtual void FinishSkill()
    {

    }



    public int UseCost
    {
        get
        {
            return useCost;
        }
        set
        {
            useCost = value;
        }
    }


}
