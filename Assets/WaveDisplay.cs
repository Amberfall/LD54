using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveDisplay : MonoBehaviour
{
    public static WaveDisplay instance;
    private void Awake()
    {
        instance = this;
    }

    public void SetWaveNumber(int n, int n_max)
    {
        GetComponent<TextMeshProUGUI>().text = "wave: " + n.ToString() + "/" + n_max.ToString();
    }
}
