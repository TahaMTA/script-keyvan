using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public float timeLimit = 600f; // 10 دقیقه
    public TMP_Text timerText;

    private float currentTime;
    private bool timerFinished = false;

    void Start()
    {
        currentTime = 0f;
    }

    void Update()
    {
        if (timerFinished)
            return;

        currentTime += Time.deltaTime;

        if (currentTime >= timeLimit)
        {
            currentTime = timeLimit;
            timerFinished = true;

            timerText.text = "10:00";

            TimeUp();
            return;
        }

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimeUp()
    {
        player player = FindFirstObjectByType<player>();

        if (player != null)
        {
                player.Health = 0;
        }
        
    }
}