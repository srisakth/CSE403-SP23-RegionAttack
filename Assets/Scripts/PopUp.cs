using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public TMP_Text _text;
    public GameObject _container;
    public bool _default = false;
    public float _duration = 1;
    private WaitForSeconds _wait = new WaitForSeconds(1);


    private void Awake()
    {
        // If we don't have a container assigned, assume it is this one
        if (_container == null)
        {
            _container = gameObject;
        }
    }

    public void StartDisplay(bool toP1, string message)
    {
        _text.text = message;
        _container.SetActive(!_default);
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
        _container.SetActive(_default);
    }
}
