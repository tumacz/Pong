using UnityEngine;

public enum PlayerMode
{
    Human,
    AI
}

public enum PlayerMap
{
    map1 = 1,
    map2 = 2
}

public enum GameMode
{
    SinglePlayer,
    MultiPlayer
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

    private void Start()
    {
        InstantiateComponents();

        _scoreBoard.SubscribeToBall(_ball);
        _scoreBoard.SetScoreText(_startScoreText);
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
            return;
        }

        // Instantiate the player 1 palette
        _player1Palette = InstantiatePalette(player1PalettePrefab, player1Mode, _player1PaletteSpawnPostion);
        if (_player1Palette == null)
        {
            return;
        }

        // Instantiate the player 2 palette
        _player2Palette = InstantiatePalette(player2PalettePrefab, player2Mode, _player2PaletteSpawnPostion);
        if (_player2Palette == null)
        {
            return;
        }
    }

    private Palette InstantiatePalette(GameObject palettePrefab, PlayerMode mode, Transform spawnPosition)
    {
        if (palettePrefab != null)
        {
            GameObject paletteObj = Instantiate(palettePrefab, spawnPosition.position, Quaternion.identity, transform);
            Palette palette = paletteObj.GetComponent<Palette>();
            palette.Initialize(mode, GetMapValue(PlayerMap.map1), _ball);
            return palette;
        }
        else
        {
            Debug.LogError("Palette prefab is not assigned.");
            return null;
        }
    }

    private int GetMapValue(PlayerMap map)
    {
        return (int)map;
    }

    public void SetGameMode(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.SinglePlayer:
                player1Mode = PlayerMode.Human;
                player2Mode = PlayerMode.AI;
                break;
            case GameMode.MultiPlayer:
                player1Mode = PlayerMode.Human;
                player2Mode = PlayerMode.Human;
                break;
            default:
                Debug.LogError("Invalid game mode.");
                break;
        }
    }

    public void ChangePlayerSide()
    {
        // Swap player sides
        PlayerMode tempMode = player1Mode;
        player1Mode = player2Mode;
        player2Mode = tempMode;

        // Swap player maps
        int tempMap = GetMapValue(PlayerMap.map1);
        SetMapValue(PlayerMap.map1, GetMapValue(PlayerMap.map2));
        SetMapValue(PlayerMap.map2, tempMap);
    }

    private void SetMapValue(PlayerMap map, int value)
    {
        switch (map)
        {
            case PlayerMap.map1:
                _player1Palette.SetPlayerNumber(value);
                break;
            case PlayerMap.map2:
                _player2Palette.SetPlayerNumber(value);
                break;
            default:
                Debug.LogError("Invalid player map.");
                break;
        }
    }
}
