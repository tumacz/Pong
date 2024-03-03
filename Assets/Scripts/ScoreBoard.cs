using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private int _aPlayerScore = 0;
    private int _bPlayerScore = 0;
    [SerializeField] private string _aScoreTag;
    [SerializeField] private string _bScoreTag;
    [SerializeField] private TextMeshProUGUI _aScoreText;
    [SerializeField] private TextMeshProUGUI _bScoreText;

    [SerializeField] private Canvas _scoreCanvas;
    private Ball _ball;

    public void EnableScoreCanvas()
    {
        if (_scoreCanvas != null)
        {
            _scoreCanvas.enabled = true;
        }
        else
        {
            Debug.LogError("Score Canvas is not assigned.");
        }
    }

    public void DisableScoreCanvas()
    {
        if (_scoreCanvas != null)
        {
            _scoreCanvas.enabled = false;
        }
        else
        {
            Debug.LogError("Score Canvas is not assigned.");
        }
    }

    public void SubscribeToBall(Ball ball)
    {
        _ball = ball;
        _ball.OnScore += UpdateScore;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        _aScoreText.text = _aPlayerScore.ToString();
        _bScoreText.text = _bPlayerScore.ToString();
    }

    private void UpdateScore(string side)
    {
        if (side == _aScoreTag)
        {
            _aPlayerScore++;
        }
        else if (side == _bScoreTag)
        {
            _bPlayerScore++;
        }
        UpdateScoreDisplay();
    }
}