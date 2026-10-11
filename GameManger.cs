using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    private AudioSource MyAudio;
    public AudioClip Jump;

    private GameObject Player;
    private GameObject End;
    public GameObject Music;

    public GameObject[] Health;
    public GameObject Win;
    public GameObject Lose;

    void Start()
    {
        MyAudio = GetComponent<AudioSource>();

        Player = GameObject.FindGameObjectWithTag("Player");
        End = GameObject.FindGameObjectWithTag("End");

    }

    void Update()
    {
        if(Player == null)
        {
            Lose.SetActive(true);
            Music.SetActive(false);
        }
        else
        {
            for (int i = 0; i < Health.Length; i++)
            {
                if (Player.GetComponent<player>().Health > i)
                {
                    Health[i].SetActive(true);
                }
                else
                {
                    Health[i].SetActive(false);
                }
            }
        }
        if(End == null)
        {
            Win.SetActive(true);
            Music.SetActive(false);
        }
    }
    public void PlaySound(AudioClip sound)
    {
        MyAudio.PlayOneShot(sound);
    }
    public void LoadNextSceneAfterDelay()
    {
        StartCoroutine(LoadNextScene());
    }
    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(5f);

        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }
    public void ReloadeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
