using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;
    [SerializeField] private string _aScoreTag;
    [SerializeField] private string _bScoreTag;

    private void Start()
    {
        FindObjectOfType<Ball>().OnScore += UpdateScore;
    }

    private void UpdateScore(string side)
    {
        if (side == _aScoreTag)
        {
            scorePlayer1++;
        }
        else if (side == _bScoreTag)
        {
            scorePlayer2++;
        }
    }
}
