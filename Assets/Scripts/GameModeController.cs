using UnityEngine;

public enum PlayerMode
{
    Human,
    AI
}

public class GameModeController : MonoBehaviour
{
    [SerializeField] private ScoreBoard _scoreBoard;

    [Header("Left Side Player")]
    [Tooltip("Prefab representing the left side player palette.")]
    [SerializeField] private GameObject player1PalettePrefab;
    [SerializeField] private PlayerMode player1Mode = PlayerMode.Human;
    private Palette _player1Palette;
    [SerializeField] private Transform _player1PaletteSpawnPostion;

    [Header("Right Side Player")]
    [Tooltip("Prefab representing the right side player palette.")]
    [SerializeField] private GameObject player2PalettePrefab;
    [SerializeField] private PlayerMode player2Mode = PlayerMode.AI;
    private Palette _player2Palette;
    [SerializeField] private Transform _player2PaletteSpawnPostion;

    [Header("Ball")]
    [Tooltip("Prefab representing the ball.")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform _ballStartPosition;
    private Ball _ball;
    private string _startScoreText = "0";

    void Start()
    {
        InstantiateComponents();
        InitializeComponents();
    }

    private void InstantiateComponents()
    {
        // Instantiate the ball
        if (ballPrefab != null)
        {
            _ball = Instantiate(ballPrefab, _ballStartPosition.position, Quaternion.identity, transform).GetComponent<Ball>();
        }
        else
        {
            Debug.LogError("Ball prefab is not assigned in the GameModeController.");
        }

        // Instantiate the player 1 palette
        if (player1PalettePrefab != null)
        {
            _player1Palette = Instantiate(player1PalettePrefab, _player1PaletteSpawnPostion.position, Quaternion.identity, transform).GetComponent<Palette>();
        }
        else
        {
            Debug.LogError("Player 1 palette prefab is not assigned in the GameModeController.");
        }

        // Instantiate the player 2 palette
        if (player2PalettePrefab != null)
        {
            _player2Palette = Instantiate(player2PalettePrefab, _player2PaletteSpawnPostion.position, Quaternion.identity, transform).GetComponent<Palette>();
        }
        else
        {
            Debug.LogError("Player 2 palette prefab is not assigned in the GameModeController.");
        }
    }

    private void InitializeComponents()
    {
        _scoreBoard.SubscribeToBall(_ball);
        // Initialize player palettes with their respective modes and numbers
        _player1Palette.Initialize(player1Mode, 1, _ball);
        _player2Palette.Initialize(player2Mode, 2, _ball);
        _scoreBoard.SetScoreText(_startScoreText);
    }

    //public void SetPlayer1Mode(PlayerMode mode)
    //{
    //    player1Mode = mode;
    //    _player1Palette.SetPlayerMode(player1Mode);
    //}

    //public void SetPlayer2Mode(PlayerMode mode)
    //{
    //    player2Mode = mode;
    //    _player2Palette.SetPlayerMode(player2Mode);
    //}
}