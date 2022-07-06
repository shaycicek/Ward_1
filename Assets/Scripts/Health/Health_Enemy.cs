using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Enemy : Health
{
    Sequence minAnimation;
    public GameObject UIPos;
    public GameObject minUI;
    public GameObject panel;
    public AudioSource audio;
    private void Start()
    {
        panel = GameManager.instance.panel;
        UIPos = GameManager.instance.UIPos;
        audio = GameManager.instance.mineralAudio;
    }
    public override void OnDeath()
    {
        base.OnDeath();
        GameManager.instance.ChangeValueOfCrystal(5);
        GameManager.instance.tmesh.SetText("" + GameManager.instance.crystalCount);
        GameObject mineralUI;
        minAnimation = DOTween.Sequence();
        audio.Play();
        mineralUI = Instantiate(minUI, Camera.main.WorldToScreenPoint(transform.position), panel.transform.rotation, panel.transform);
        minAnimation.Append(mineralUI.transform.DOMove(Camera.main.WorldToScreenPoint(UIPos.transform.position), 0.5f).SetEase(Ease.OutSine)).OnComplete(() => Destroy(mineralUI));
        Destroy(gameObject,1f);
    }
}
