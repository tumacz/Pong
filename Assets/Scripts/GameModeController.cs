using System;
using UnityEngine;

public enum PlayerState
{
    Human,
    Computer
}

public enum PlayerInputMapNumber
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
    [SerializeField] public ScoreBoard _scoreBoard;
    [SerializeField] private GameSettings _gameSettings;

    private Palette _player1Palette;
    private Palette _player2Palette;
    private Ball _ball;

    [HideInInspector] public GameMode _currentGameMode;
    [SerializeField] private Transform _ballStartPosition;
    [SerializeField] private Transform _player1PaletteSpawnPosition;
    [SerializeField] private Transform _player2PaletteSpawnPosition;

    private GameObject _player1PalettePrefab;
    private GameObject _player2PalettePrefab;
    private PlayerState _player1State;
    private PlayerState _player2State;
    private GameObject _ballPrefab;

    private int _player1MapNumber;
    private int _player2MapNumber;

    private void OnEnable()
    {
        _scoreBoard.OnWin += EndMatch;
    }

    private void OnDisable()
    {
        _scoreBoard.OnWin -= EndMatch;
    }

    private void Start()
    {
        if (_gameSettings != null)
        {
            _player1PalettePrefab = _gameSettings.Player1PalettePrefab;
            _player1State = _gameSettings.Player1Mode;
            _player2PalettePrefab = _gameSettings.Player2PalettePrefab;
            _player2State = _gameSettings.Player2Mode;
            _ballPrefab = _gameSettings.BallPrefab;

            SetPlayerMapNumbers();

            _currentGameMode = _gameSettings.DefaultGameMode;
            InstantiateComponents();
        }
        else
        {
            Debug.Log("GameSettings is not assigned!");
        }
    }

    public void StartMatch()
    {
        InitializePalette(_player1Palette, _player1State, _player1MapNumber);
        InitializePalette(_player2Palette, _player2State, _player2MapNumber);
        InitializeGame();
    }

    public void EndMatch()
    {
        _scoreBoard.DisableScoreCanvas();
        if (_player1Palette != null)
        {
            _player1Palette.OnMatchFinished();
            Destroy(_player1Palette.gameObject);
        }
        if (_player2Palette != null)
        {
            _player2Palette.OnMatchFinished();
            Destroy(_player2Palette.gameObject);
        }
        if (_ball != null)
        {
            Destroy(_ball.gameObject);
        }
        _player1Palette = null;
        _player2Palette = null;
        _ball = null;
        InstantiateComponents();
    }

    private void SetPlayerMapNumbers()
    {
        _player1MapNumber = _gameSettings.Player1PaletteNum;
        _player2MapNumber = _gameSettings.Player2PaletteNum;
    }

    private void InstantiateComponents()
    {
        _ball = Utility.InstantiateBall(_ballPrefab, _ballStartPosition, transform);
        _player1Palette = Utility.InstantiatePalette(_player1PalettePrefab, _player1PaletteSpawnPosition, transform);
        _player2Palette = Utility.InstantiatePalette(_player2PalettePrefab, _player2PaletteSpawnPosition, transform);
    }

    private void InitializePalette(Palette palette, PlayerState state, int mapNumber)
    {
        if (palette != null)
        {
            palette.Initialize(state, mapNumber, _ball);
        }
    }

    public void InitializeGame()
    {
        _scoreBoard.EnableScoreCanvas();
        _scoreBoard.SubscribeToBall(_ball);
        _ball.StartBall();
    }

    public void SetGameMode(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.SinglePlayer:
                _player1State = PlayerState.Human;
                _player2State = PlayerState.Computer;
                _player1MapNumber = 1;
                _player2MapNumber = 2;
                _currentGameMode = GameMode.SinglePlayer;
                break;
            case GameMode.MultiPlayer:
                _player1State = PlayerState.Human;
                _player2State = PlayerState.Human;
                _player1MapNumber = 1;
                _player2MapNumber = 2;
                _currentGameMode = GameMode.MultiPlayer;
                break;
            default:
                Debug.LogError("Invalid game mode.");
                break;
        }
    }

    public void ChangePlayerSide(GameMode currentGameMode)
    {
        switch (currentGameMode)
        {
            case GameMode.SinglePlayer:
                SwapPlayersState();
                SwapPlayerMapNumbers();
                break;
            case GameMode.MultiPlayer:
                SwapPlayerMapNumbers();
                break;
            default:
                Debug.LogError("Invalid game mode.");
                break;
        }

        UpdatePalettesWithNewMapValues();
    }

    private void SwapPlayersState()
    {
        PlayerState tempState = _player1State;
        _player1State = _player2State;
        _player2State = tempState;
    }

    private void SwapPlayerMapNumbers()
    {
        int tempMap = _player1MapNumber;
        _player1MapNumber = _player2MapNumber;
        _player2MapNumber = tempMap;
    }

    private void UpdatePalettesWithNewMapValues()
    {
        SetPaletteMapNumber(_player1Palette, _player1MapNumber);
        SetPaletteMapNumber(_player2Palette, _player2MapNumber);
    }

    private void SetPaletteMapNumber(Palette palette, int mapNumber)
    {
        if (palette != null)
        {
            palette.SetPlayerNumber(mapNumber);
        }
    }
}