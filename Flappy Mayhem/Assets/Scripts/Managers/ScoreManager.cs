using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instace;

    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private int score;

    public int GetScore()
    {
        return score;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (instace == null)
        {
            instace = this;
        }
    }

    private void Start()
    {
        currentScoreText.text = score.ToString();

        bestScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateBestScore();
    }

    private void UpdateBestScore()
    {
        if(score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            bestScoreText.text = score.ToString();
        }
    }

    public void UpdateScore()
    {
        score++;
        currentScoreText.text = score.ToString();
        UpdateBestScore();
    }
}
