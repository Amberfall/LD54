using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public float time;
    [SerializeField] private float _timeBeforeLosingCombo;
    public int score;
    public float scoreMultiplier;
    public float ratio;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 0;
            scoreMultiplier = 1;
        }
        ratio = time / _timeBeforeLosingCombo;
    }

    public void IncreaseScoreMultiplier()
    {
        scoreMultiplier += 0.1f;
        time = _timeBeforeLosingCombo;
    }

    public void ResetScoreMultiplier()
    {
        time = 0;
        scoreMultiplier = 0;
    }

    public void AddPoints(int points)
    {
        score += (int)(points * scoreMultiplier);
    }
}
