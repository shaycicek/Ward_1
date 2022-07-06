using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    Skill mySkill;

    public void SetSkill(Skill skill , int cost )
    {
        mySkill = skill;
        text.SetText("" + cost);

        GetComponent<Button>().onClick.AddListener(() => {
            mySkill.UseSkill();
            GetComponent<CooldownSkill>().StartCooldDown();
            
        });
    }

    public void UseSkill()
    {

        mySkill.UseSkill();
    }

    public void CheckUsable()
    {
        if(GameManager.instance.crystalCount >= mySkill.useCost && !GetComponent<CooldownSkill>().isCoolDown)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
