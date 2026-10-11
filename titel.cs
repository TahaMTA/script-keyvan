using UnityEngine;
using UnityEngine.SceneManagement;

public class titel : MonoBehaviour
{
    private bool starting = false;

    void Update()
    {
        if (!starting && Input.anyKeyDown)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        starting = true;
        SceneManager.LoadScene("dmvact1");
    }
}