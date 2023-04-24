using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //no im not making a separate script for stage ui lol

public class GameManager : MonoBehaviour
{

    [SerializeField] List<StageSO> stageList;

    [SerializeField] float musicFadeFactor;

    [SerializeField] float stageStartDelay;
    [SerializeField] float stageEndDelay;
    [SerializeField] float fadeHold;
    [SerializeField] float fadeSpeed;

    [SerializeField] TextMeshProUGUI stageStartText;
    [SerializeField] TextMeshProUGUI stageEndText;

    EnemySpawner enemySpawner;

    GameObject music;
    GameObject player;
    AudioSource musicSrc;
    float defaultMusicVol;

    public int score;

    public bool stagePlaying;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        player = FindObjectOfType<Player>().gameObject;
        music = FindObjectOfType<MusicLoop>().gameObject;
        musicSrc = music.GetComponent<AudioSource>();
        defaultMusicVol = musicSrc.volume;
        StartCoroutine(HandleStages());
    }


    public IEnumerator HandleStages()
    {
        foreach (StageSO stage in stageList)
        {
            stageStartText.text = stage.GetStageName();
            yield return new WaitForSeconds(stageStartDelay);
            StartCoroutine(FadeText(stageStartText, fadeSpeed));
            NextStage(stage);
            stagePlaying = true;
            do { yield return null; }
            while (enemySpawner.spawningEnemies || GameObject.FindGameObjectsWithTag("Enemy").Length > 0);
            stagePlaying = false;
            StartCoroutine(FadeText(stageEndText, fadeSpeed/3));
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

    public void StopGame() {
        StopCoroutine(HandleStages());
    }
}
