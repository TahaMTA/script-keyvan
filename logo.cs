using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StudioLogo : MonoBehaviour
{
    public Image fadeImage;

    public float logoTime = 5f;
    public float fadeTime = 1.5f;

    private void Start()
    {
        StartCoroutine(PlayLogo());
    }

    IEnumerator PlayLogo()
    {
        // نمایش لوگو
        yield return new WaitForSeconds(logoTime);

        // محو شدن
        float time = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, time / fadeTime);

            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        // رفتن به سکانس بعدی بر اساس Build Index
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}