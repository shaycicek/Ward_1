using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillMenu : MonoBehaviour
{
    public TextMeshProUGUI tmesh;
    public static bool SkillUsed = false;
    public ParticleSystem novaExplotionParticle;
    public ParticleSystem speedParticle;

    Character player;
    Skill spawnSkill;
    Skill damageMultipSkill;
    Skill novaSkill;
    Skill speedSkill;

    public SkillButton spawnAllyButton;
    public SkillButton damageMultipButton;
    public SkillButton novaExplotion;
    public SkillButton speedButton;
    public List<SkillButton> skillButtons = new List<SkillButton>();

    public static SkillMenu instance;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            speedSkill.UseSkill();
        }
    
    }

    private void Awake()
    {
        if(instance == null || instance == this)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Update is called once per frame
    private void Start()
    {
        List<Skill> skills = new List<Skill>();
        
        spawnSkill = new Skill_SpawnAlly(10);
        spawnSkill.Initialize(GameManager.instance.player);
        damageMultipSkill = new Skill_DamageMultiplier(5, 10, 20);
        damageMultipSkill.Initialize(GameManager.instance.player);
        novaSkill = new Skill_NovaExplotion(10, 5, 30);
        novaSkill.Initialize(GameManager.instance.player);
        speedSkill = new Skill_Speed(2f, 5f, 15);
        speedSkill.Initialize(GameManager.instance.player);

        skills.Add(spawnSkill);
        skills.Add(speedSkill);
        skills.Add(damageMultipSkill);
        skills.Add(novaSkill);
        

        for (int i = 0; i < skillButtons.Count; i++)
        {
            Skill s = skills[i];
            skillButtons[i].SetSkill(s, s.useCost);
        }

        player = GameManager.instance.player;

    }
    
    public void InitializeSkillMenu()
    {
        GameManager.instance.OnMineralCollect += CheckUsable;
    }

    private void OnDisable()
    {
        
        GameManager.instance.OnMineralCollect -= CheckUsable;
    }
    public void SpawnAllies()
    {
    }

    public void DamageMultiplier()
    {
    }

    public void NovaExplotion()
    {
        ParticleSystem particle;
        CheckUsable();
        particle = Instantiate(novaExplotionParticle, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity);
        particle.transform.Rotate(-90, 0, 0);
        var sh = particle.shape;
        sh.radius = 10;
    }

    public void Speed()
    {
        ParticleSystem particle;
        //speedSkill.UseSkill();
        CheckUsable();
        particle = Instantiate(speedParticle, new Vector3(player.transform.position.x, 
            player.transform.position.y + 1, 
            player.transform.position.z), 
            Quaternion.identity);
        particle.transform.parent = player.transform;
        Destroy(particle, 5f); // ??
    }

    public void CheckUsable()
    {
        foreach(SkillButton sb in skillButtons)
        {
            sb.CheckUsable();
        }
    }
}
