using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaceFX : MonoBehaviour
{
    [SerializeField] GameObject[] FX;
    [SerializeField] float FXInterval;
    [SerializeField] float intervalVariance;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeFX());
    }

    // Update is called once per frame
    IEnumerator MakeFX()
    {
        while (true) {
            yield return new WaitForSeconds(FXInterval + Random.Range(0, intervalVariance));
        Vector2 pos = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0,1), Random.Range(0,1)));
        GameObject fxobj = FX[Random.Range(0, FX.Length)];
        Instantiate(fxobj, pos, Quaternion.identity);
        }
    }
}
