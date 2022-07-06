using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DamageMultiplier : Skill
{
    int multiplier;
    float skillTimer;

    public Skill_DamageMultiplier(int multiplier , float skillTimer , int cost) : base(cost)
    {
        this.multiplier = multiplier;
        this.skillTimer = skillTimer;
    }

    public override void FinishSkill()
    {
        character.damageFactor /= multiplier;
    }

    public override void UseSkill()
    {
        if(GameManager.instance.crystalCount >=UseCost)
        {
            GameManager.instance.ChangeValueOfCrystal(-useCost);            
            character.GetComponent<SkillManager>().StartSkillCountdown(this, skillTimer);
            character.damageFactor *= multiplier;
        }

        //Süre dolunca resetlenecek ???
    }

}
