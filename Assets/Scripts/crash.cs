using UnityEngine;
using UnityEngine.Diagnostics;

public class crash : MonoBehaviour
{
    [SerializeField] GameObject trolling;
    void Start()
    {
        Invoke("Crash", 2);
    }

    void Update() {
        Instantiate(trolling, transform);
    }

    void Crash() {
            
            Utils.ForceCrash(ForcedCrashCategory.AccessViolation); // we do a little trolling
    }
}
