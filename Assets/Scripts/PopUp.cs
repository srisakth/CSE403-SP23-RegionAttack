using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public float _displayDuration = 1;

    public TMP_Text _text;

    public void StartDisplay(string message)
    {
        _text.text = message;
        gameObject.SetActive(true);
        StartCoroutine(Display());
    }

    IEnumerator Display()
    {
        for (float t = 0; t < _displayDuration; t += Time.deltaTime)
        {
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
