using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject stopObject;
    public AudioSource music;
    public AudioSource efx;
    public AudioClip psfx;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            StopGame();
        }
    }

    public void StopGame()
    {
        isPaused = true;

        Time.timeScale = 0f;
        stopObject.SetActive(true);

        music.Pause();
        efx.PlayOneShot(psfx);
    }

    public void ResumeGame()
    {
        isPaused = false;

        Time.timeScale = 1f;
        stopObject.SetActive(false);

        music.UnPause();
        efx.PlayOneShot(psfx);
    }
}