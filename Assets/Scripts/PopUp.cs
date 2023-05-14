using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public TMP_Text _text;
    public bool _default = false;
    public float _duration = 1;
    private WaitForSeconds _wait = new WaitForSeconds(1);

    public void StartDisplay(bool toP1, string message)
    {
        _text.text = message;
        gameObject.SetActive(!_default);
        StartCoroutine(Display());
    }

    public void SetDuration(float duration)
    {
        _duration = duration;
        _wait = new WaitForSeconds(duration);
    }

    public void SetMessage(string message)
    {
        _text.text = message;
    }

    public void StartDisplay(string message) { StartDisplay(true, message); }

    IEnumerator Display()
    {
        yield return _wait;
        gameObject.SetActive(_default);
    }
}
