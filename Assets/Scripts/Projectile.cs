using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject model;
    float cameraShakeIntensity = .75f;
    float cameraShakeTime=.05f;
    public float speed;
    public int damage;
    float damageFactor;
    private Weapon weapon;
    public LayerMask enemy;
    private Health hitCharacter;
    public float lifetime;
    public ParticleSystem destroyingParticle;
    public ParticleSystem onFireParticle;
    public AudioSource bulletAudio;
    
    private void Start()
    {
        CinemachineShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTime);
        GetComponent<Rigidbody>().AddForce(-weapon.transform.forward * speed, ForceMode.Impulse);
        bulletAudio = GameManager.instance.bulletAudio;
        bulletAudio.gameObject.SetActive(true);
        if(!bulletAudio.isPlaying) 
        {
            bulletAudio.Play();
        }
        
        onFireParticle.gameObject.SetActive(true);
        Destroy(gameObject, lifetime);
    }

    public void OnTriggerEnter(Collider other)
    {
     
        //Debug.Log("Collider layer = " + other.gameObject.layer + "Enemy layer = " + enemy);
        if (enemy == (enemy | (1 << other.gameObject.layer)))
        {

            hitCharacter = other.GetComponent<Health>();
            OnHit();
        }   
    }

    public void OnHit()
    {
        onFireParticle.gameObject.SetActive(false);
        hitCharacter.GetDamage(damage * damageFactor);
        //destroyingParticle.gameObject.SetActive(true);
        model.SetActive(false);
        Destroy(gameObject,1f);
    } 

    public void Initialize(LayerMask enemy, float damageFactor, Weapon weapon )
    {
        this.weapon = weapon;
        this.enemy = enemy;
        this.damageFactor = damageFactor;

    }

    
    //timer yaz !

}
