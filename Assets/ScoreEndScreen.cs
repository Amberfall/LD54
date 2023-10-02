using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreEndScreen : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Score: " + ScoreManager.instance.score.ToString();
    }

}
