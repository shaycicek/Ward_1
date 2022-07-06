using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Mineral : Interactable
{

    public string mineralname;
    public float amount;
    public float crashTime;
    public bool crashed;
    public ParticleSystem crashEff;
    Sequence minAnimation;
    public GameObject UIPos;
    public float crystalCount;
    public AudioSource audio;

    private void Start()
    {
        StartCoroutine(LateStart());
        crashTime = amount;
        crashed = false;
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        player = GameManager.instance.player;
        panel = GameManager.instance.panel;
        UIPos = GameManager.instance.UIPos;
        audio = GameManager.instance.mineralAudio;

    }
    public override void Interact(GameObject other)
    {
        GetComponent<Collider>().enabled = false;
        GameObject mineralUI;
        Destroy(Instantiate(crashEff, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, panel.transform), .5f);
        UIManager.instance.MineralCollectAnim(transform);
        audio.gameObject.SetActive(true);
        audio.Play();
        model.SetActive(false);
        GameManager.instance.ChangeValueOfCrystal(amount);
        Destroy(gameObject, crashTime/15);
        //Coroutine cagir
    }

    private void OnTriggerEnter(Collider other)
    {        
        other.GetComponent<PlayerController>().AddMineralList(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if(crashTime>=0)
        {
            crashTime -= (Time.deltaTime) * 20; //??
        }
        else
        {
            other.gameObject.GetComponent<PlayerController>().RemoveFromMineralList(this);
            Interact(other.gameObject);
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<PlayerController>().RemoveFromMineralList(this);  
    }

    public void Initialize (string mineralname , int amount)
    {
        this.mineralname = mineralname;
        this.amount = amount;

    } 
}
