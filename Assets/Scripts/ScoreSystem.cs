using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public const string HIGH_SCORE_KEY = "HighScore";

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier = 5;

    private float score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, Mathf.FloorToInt(score));
        }
    }
}
