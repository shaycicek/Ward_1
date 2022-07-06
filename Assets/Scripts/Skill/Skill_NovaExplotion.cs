using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_NovaExplotion : Skill
{
    float radius;
    float damage;
    public Skill_NovaExplotion(float damage ,float radius , int cost) : base(cost)
    {
        this.radius = radius;
        this.damage = damage;
    }


    /* public float Radius
    {
        get
        {
            return radius;
        }
        set
        {
            radius = value;
        }
    }  */

    public override void UseSkill()
    {
        if (GameManager.instance.crystalCount >= UseCost)
        {
            Debug.Log("Nova Skill Used");
            GameManager.instance.ChangeValueOfCrystal(-useCost);
            
            Collider[] hitColliders = Physics.OverlapSphere(character.transform.position , radius, character.enemy);
            foreach (var hitCollider in hitColliders)
            {
                if (!hitCollider.gameObject.GetComponent<Character>().isDead)
                {                    
                    hitCollider.gameObject.GetComponent<Health>().GetDamage(damage);
                }
            }

        }
    }
}
