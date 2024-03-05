using System;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private int _rightPlayerScore = 0;
    private int _leftPlayerScore = 0;
    [SerializeField] private TextMeshProUGUI _rightScoreText;
    [SerializeField] private TextMeshProUGUI _leftScoreText;

    [SerializeField] private Canvas _scoreCanvas;
    private Ball _ball;

    [Tooltip("Description of the action to take when a player wins.")]
    [SerializeField] private string onWinActionDescription;
    public event Action OnWin;
    private int _winScoreCondition;

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

    public void SubscribeToBall(Ball ball, int condition)
    {
        if (condition <= 0)
        {
            Debug.LogError("Invalid win score condition: " + _winScoreCondition + ". Check Game Mode Controller inspector.");
            return;
        }

        _ball = ball;
        if (_ball != null)
        {
            _ball.OnScore += UpdateScore;
            UpdateScoreDisplay();
        }
        _winScoreCondition = condition;
    }

    private void UpdateScoreDisplay()
    {
        _rightScoreText.text = _rightPlayerScore.ToString();
        _leftScoreText.text = _leftPlayerScore.ToString();
    }

    private void UpdateScore(string side)
    {
        if (_ball == null)
        {
            Debug.LogError("Ball is not assigned.");
            return;
        }

        if (side == _ball.RightScoreTag)
        {
            _rightPlayerScore++;
        }
        else if (side == _ball.LeftScoreTag)
        {
            _leftPlayerScore++;
        }
        if (Mathf.Abs(_leftPlayerScore - _rightPlayerScore) >= _winScoreCondition)
        {
            OnWin?.Invoke();
            ResetScore();
        }

        UpdateScoreDisplay();
    }

    private void ResetScore()
    {
        _rightPlayerScore = 0;
        _leftPlayerScore = 0;
    }
}