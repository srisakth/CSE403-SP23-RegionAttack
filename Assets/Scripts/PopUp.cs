using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public TMP_Text _text;

    private WaitForSeconds _wait = new WaitForSeconds(1);

    public void StartDisplay(bool toP1, string message, float duration)
    {
        _text.text = message;
        gameObject.SetActive(true);
        StartCoroutine(Display(duration));
    }

    public void StartDisplay(bool toP1, string message)
    {
        _text.text = message;
        gameObject.SetActive(true);
        StartCoroutine(Display());
    }

    public void StartDisplay(string message) { StartDisplay(true, message); }

    IEnumerator Display()
    {
        yield return _wait;
        gameObject.SetActive(false);
    }

    IEnumerator Display(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
