using System.Collections;
using System.Threading;
using UnityEngine;

public class fps : MonoBehaviour
{
    [Header("freame setting")]
    int MaxRate = 9999;
    public float TargetFrameRate = 60.0f;
    float CurrntFrameTime;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = MaxRate;
        CurrntFrameTime = Time.realtimeSinceStartup;
        StartCoroutine("WaitForNextFrame");
    } 
    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            CurrntFrameTime += 1.0f / TargetFrameRate;
            var t = Time.realtimeSinceStartup;
            var sleepTime = CurrntFrameTime - t - 0.01f;
            if (sleepTime > 0)
                Thread.Sleep((int)(sleepTime * 1000));
            while (t < CurrntFrameTime)
                t = Time.realtimeSinceStartup;
        }
    }
}
