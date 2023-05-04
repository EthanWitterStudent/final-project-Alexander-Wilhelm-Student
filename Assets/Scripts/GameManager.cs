using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //no im not making a separate script for stage ui lol

public class GameManager : MonoBehaviour
{
    [Header("Stages")]
    [SerializeField] List<StageSO> stageList;
    [SerializeField] float stageStartDelay;
    [SerializeField] float stageEndDelay;
    [System.NonSerialized] public bool stagePlaying;

    //[SerializeField] TextMeshProUGUI stageStartText;
    //[SerializeField] TextMeshProUGUI stageEndText;

    EnemySpawner enemySpawner;

    [Header("Music")]
    GameObject music;
    AudioSource musicSrc;
    float defaultMusicVol;
    [SerializeField] float musicFadeFactor;


    [Header("Cash")]
    int cash;

    [SerializeField] int startingCash;
    [SerializeField] int passiveCashAmount;
    [SerializeField] float passiveCashDelay;
    [SerializeField] AudioClip passiveCashSound;
    [SerializeField] float passiveCashVolume;
    float cashtimer;



    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        music = FindObjectOfType<MusicLoop>().gameObject;
        musicSrc = music.GetComponent<AudioSource>();
        defaultMusicVol = musicSrc.volume;
        cash = startingCash;
        StartCoroutine(HandleStages());
        StartCoroutine(PassiveCash());
    }


    public IEnumerator HandleStages()
    {
        foreach (StageSO stage in stageList)
        {
            //stageStartText.text = stage.GetStageName();
            yield return new WaitForSeconds(stageStartDelay);
            NextStage(stage);
            stagePlaying = true;
            do { yield return null; }
            while (enemySpawner.spawningEnemies || GameObject.FindGameObjectsWithTag("Enemy").Length > 0);
            stagePlaying = false;
            StartCoroutine(FadeMusic());
            yield return new WaitForSeconds(stageEndDelay);

        }
    }

    void NextStage(StageSO stage)
    {
        enemySpawner.waveList = stage.GetWaves();
        StartCoroutine(enemySpawner.SpawnWaves());
        musicSrc.clip = stage.GetIntroMusic();
        music.GetComponent<MusicLoop>().LoopMusic = stage.GetLoopMusic();
        musicSrc.loop = false;
        musicSrc.volume = defaultMusicVol;
        musicSrc.Play();

    }

    public IEnumerator FadeMusic()
    {
        while (musicSrc.volume >= Mathf.Epsilon)
        {
            musicSrc.volume -= (Time.deltaTime * musicFadeFactor);
            yield return null;
        }
        musicSrc.Stop();
    }

    public int GetCash()
    {
        return cash;
    }

    public void AddCash(int x)
    {
        cash += x;
    }

    public float GetCashTimer() {
        return cashtimer;
    }

    public float GetCashDelay() {
        return passiveCashDelay;
    }

    IEnumerator PassiveCash() //using an enumerator because i don't wanna make a whole-ass update loop for one function
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            cashtimer += Time.fixedDeltaTime;
            if (cashtimer >= passiveCashDelay) {
                cashtimer = 0;                      //using a timer instead of yield return to give the ui something to play with
                AddCash(passiveCashAmount);
                FindObjectOfType<AudioPlayer>().PlayClip(passiveCashSound, passiveCashVolume);
                FindObjectOfType<UIScript>().UpdateMoneyText();
            }
            
        }
    }


    /* from space defender, could be of use later
        put into ui script, let's not leave this here!!!!


        public IEnumerator FadeText(TextMeshProUGUI text, float fadeFactor) {

            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            yield return new WaitForSeconds(fadeHold);
            Debug.Log($"{text.color.r} {text.color.g} {text.color.b} {text.color.a}");
            while (text.color.a >= Mathf.Epsilon)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, (text.color.a - (fadeFactor*Time.deltaTime))); //INSANE!!!! WHY AM I DOING THIS
                yield return null;
            }

        }

        */

    public void StopGame()
    {
        StopCoroutine(HandleStages());
    }
}
