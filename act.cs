using UnityEngine;
using TMPro;
using System.Collections;

public class ActTextController : MonoBehaviour
{
    public TextMeshProUGUI actText;

    void Start()
    {
        //actText.text = "ACT 1";
        StartCoroutine(HideText());
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(3f);

        actText.gameObject.SetActive(false);
    }
}