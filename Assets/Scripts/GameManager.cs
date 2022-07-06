using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float crystalCount;
    public TextMeshProUGUI tmesh;
    public Animator animator;
    public Character enemyPrefab;
    public Character allyPrefab;
    public Mineral min1;
    public static GameManager instance;
    public Character player;
    public Transform cam;
    public Image mineralImg;
    public Image healthBarImg;
    public GameObject panel;
    public GameObject UIPos;
    public AudioSource mineralAudio;
    public AudioSource bulletAudio;
    public delegate void OnMineralCollectDelegate();
    public OnMineralCollectDelegate OnMineralCollect;
    private void Start()
    {
        SkillMenu.instance.InitializeSkillMenu();

        crystalCount = 0;
        tmesh.SetText(crystalCount + "");
        for (int i =0; i<100; i++)
        {
            SpawnManager.instance.SpawnMineral();
        }
        SpawnManager.instance.InvokeRepeating("SpawnEnemy" , 2f , 3f);
        SpawnManager.instance.InvokeRepeating("SpawnMineral", 2f, 3f);
        
    }
    private void Update()
    {
       
    }
    private void Awake()
    {
        if (instance == null || instance == this)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public void ChangeValueOfCrystal(float amount)
    {
        crystalCount += amount;
        tmesh.SetText("" + GameManager.instance.crystalCount);
        OnMineralCollect?.Invoke();
    }


}
