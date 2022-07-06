using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public HealthBar healthBar;
    public float charHealth;
    public float currentHealth;
    public Animator animator;
    public ParticleSystem deathParticle;
    Sequence knockBackAnimation;

    private void Awake()
    {
        currentHealth = charHealth;
        healthBar.SetMaxHealth(charHealth);

    }
    public virtual void OnDeath()
    {
        animator.SetTrigger("isDead");
        StartCoroutine(InstantiateParticle());
    }

    IEnumerator InstantiateParticle()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(deathParticle, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity).gameObject.SetActive(true);
    }

    public virtual void GetDamage(float damage)
    {

        if(GetComponent<Character>().isDead==false)
        {
            knockBackAnimation = DOTween.Sequence();
            //knockBackAnimation.Append(transform.DOMove(), 0.5f).SetEase(Ease.OutSine)))
            ParticleSystem blood;
            blood = Instantiate(ParticleContainer.instance.kanDamlasý, new Vector3(transform.position.x,
                transform.position.y + 1, transform.position.z), Quaternion.identity);
            Destroy(blood.gameObject, .2f);
            
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                GetComponent<Character>().isDead = true;
                OnDeath();
            }
        }

    }

}
