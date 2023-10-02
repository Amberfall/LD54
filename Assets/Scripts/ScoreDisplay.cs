using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private ScoreManager _scoreManager;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _combo;
    [SerializeField] private RectTransform _comboBar;

    // Start is called before the first frame update
    void Start()
    {
        _scoreManager = ScoreManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        _score.text = "Score: " + _scoreManager.score.ToString();
        if (_scoreManager.scoreMultiplier > 1)
        {
            _combo.text = "X " + _scoreManager.scoreMultiplier.ToString("#.#");
        }
        else
        {
            _combo.text = "";
        }
        _comboBar.localScale = new Vector3(_scoreManager.ratio, 1, 1);
    }
}
