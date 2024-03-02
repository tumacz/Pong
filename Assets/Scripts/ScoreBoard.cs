using System;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public int aPlayerScore { get; private set; } = 0;
    public int bPlayerScore { get; private set; } = 0;

    [SerializeField] private string _aScoreTag;
    [SerializeField] private string _bScoreTag;

    [SerializeField] private Canvas _scoreCanvas;
    [SerializeField] private TextMeshProUGUI _aScoreText;
    [SerializeField] private TextMeshProUGUI _bScoreText;
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
        //UpdateScoreDisplay();
    }

    public void SetScoreText(string startScoreText)// redo
    {
        _aScoreText.text = startScoreText;
        _bScoreText.text = startScoreText;
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