using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TMP_Text _p1, _p2;

    public void SetScore(int p1, int p2)
    {
        _p1.text = p1.ToString();
        _p2.text = p2.ToString();
    }

}
