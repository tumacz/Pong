using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public int aPlayerScore { get; private set; } = 0;
    public int bPlayerScore { get; private set; } = 0;

    [SerializeField] private string _aScoreTag;
    [SerializeField] private string _bScoreTag;

    [SerializeField] private TextMeshProUGUI _aScoreText;
    [SerializeField] private TextMeshProUGUI _bScoreText;

    private void Start()
    {
        FindObjectOfType<Ball>().OnScore += UpdateScore;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        _aScoreText.text = aPlayerScore.ToString();
        _bScoreText.text = bPlayerScore.ToString();
    }

    private void UpdateScore(string side)
    {
        if (side == _aScoreTag)
        {
            aPlayerScore++;
        }
        else if (side == _bScoreTag)
        {
            bPlayerScore++;
        }
        UpdateScoreDisplay();
    }
}
