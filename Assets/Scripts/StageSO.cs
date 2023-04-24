using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage Config", fileName = "New Stage Config")] //lole
public class StageSO : ScriptableObject
{
    [SerializeField] string stageName;

    [SerializeField] List<WaveSO> waves;

    [SerializeField] AudioClip introMusic;
    [SerializeField] AudioClip loopMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetStageName() {
        return stageName;
    }

    public List<WaveSO> GetWaves() {
        return waves;
    }

    public AudioClip GetIntroMusic() {
        return introMusic;
    }

    public AudioClip GetLoopMusic() {
        return loopMusic;
    }

}
