using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public TMP_Text _text;

    private WaitForSeconds _wait = new WaitForSeconds(1);

    public void StartDisplay(bool hide, string message)
    {
        _text.text = message;
        gameObject.SetActive(true);
        if (hide)
            StartCoroutine(Display());
    }

    public void StartDisplay(string message) { StartDisplay(true, message); }

    public void ChangeDuration(float duration)
    {
        _wait = new WaitForSeconds(duration);
    }

    IEnumerator Display()
    {
        yield return _wait;
        gameObject.SetActive(false);
    }
}
