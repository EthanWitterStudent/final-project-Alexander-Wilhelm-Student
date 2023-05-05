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
    AudioPlayer sfxSrc;


    [Header("Cash")]
    int cash;

    [SerializeField] int startingCash;
    [SerializeField] int passiveCashAmount;
    [SerializeField] float passiveCashDelay;
    [SerializeField] AudioClip passiveCashSound;
    [SerializeField] float passiveCashVolume;
    float cashtimer;

    [Header("Win Stuff")]
    [SerializeField] float winDelay;
    [SerializeField] float fadeDelay;
    [SerializeField] GameObject winEffect;

    [SerializeField] AudioClip winMusic;
    [SerializeField] float menuDelay;

    [SerializeField] Collider2D loseCollider;

    [SerializeField] AudioClip loseSound;

    //misc hidden vars

    bool lostGame; //unused as of yet, i plan to move losing logic here
    UIScript uiscript;
    


    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        music = FindObjectOfType<MusicLoop>().gameObject;
        musicSrc = music.GetComponent<AudioSource>();
        sfxSrc = FindObjectOfType<AudioPlayer>();
        defaultMusicVol = musicSrc.volume;
        uiscript = FindObjectOfType<UIScript>();
        cash = startingCash;
        uiscript.UpdateMoneyText();
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
        Debug.Log("based");
        if (!lostGame) {
            Debug.Log("super based?");
            yield return new WaitForSeconds(winDelay);
            StartCoroutine(WinGame());
        }

    }

    void NextStage(StageSO stage)
    {
        enemySpawner.waveList = stage.GetWaves();
        StartCoroutine(enemySpawner.SpawnWaves());
        if (stage.GetIntroMusic() != null) musicSrc.clip = stage.GetIntroMusic();
        if (stage.GetLoopMusic() != null) music.GetComponent<MusicLoop>().LoopMusic = stage.GetLoopMusic();
        musicSrc.loop = false;
        musicSrc.volume = defaultMusicVol;
        musicSrc.Play();
    }

    IEnumerator WinGame() {
        Debug.Log("wintext");
        StartCoroutine(uiscript.WinText());
        Debug.Log("winmusic");
        musicSrc.clip = winMusic;
        music.GetComponent<MusicLoop>().looping = false;
        musicSrc.loop = false;
        musicSrc.volume = defaultMusicVol;
        musicSrc.Play();
        
        yield return new WaitForSeconds(fadeDelay);
        StartCoroutine(uiscript.FadePanel());
        yield return new WaitForSeconds(menuDelay);
        FindObjectOfType<LevelManager>().LoadMainMenu();
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
                yield return new WaitUntil(() => stagePlaying);
                cashtimer = 0;                      //using a timer instead of yield return to give the ui something to play with
                AddCash(passiveCashAmount);
                FindObjectOfType<AudioPlayer>().PlayClip(passiveCashSound, passiveCashVolume);
                FindObjectOfType<UIScript>().UpdateMoneyText();
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D other) { //lose the game idiot!!
        Debug.Log("gg");
        if (other.gameObject.tag == "Enemy" && !lostGame) {
            Debug.Log("wait");
            StartCoroutine(LoseGame());
        }
    }

    IEnumerator LoseGame() {
        Debug.Log("whar???");
        StopGame();
        lostGame = true;
        sfxSrc.PlayClip(loseSound, 1);
        StartCoroutine(uiscript.FadePanel());
        Debug.Log("WHAR???");
        yield return new WaitForSeconds(menuDelay);
        FindObjectOfType<LevelManager>().LoadGameOver();
    }

    public void StopGame()
    {
        StopCoroutine(HandleStages());
    }
}
