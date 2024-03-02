using System;
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
    public GameSettings _gameSettings;

    private Palette _player1Palette;
    private Palette _player2Palette;
    private Ball _ball;
    [HideInInspector] public GameMode _currentGameMode;
    [SerializeField] private Transform _ballStartPosition;
    [SerializeField] private Transform _player1PaletteSpawnPosition;
    [SerializeField] private Transform _player2PaletteSpawnPosition;
    private GameObject _player1PalettePrefab;
    private PlayerMode _player1Mode;
    private GameObject _player2PalettePrefab;
    private PlayerMode _player2Mode;
    private GameObject _ballPrefab;
    private string _startScoreText;

    private int _player1PaletteNum;
    private int _player2PaletteNum;

    private void Start()
    {
        _player1PalettePrefab = _gameSettings.player1PalettePrefab;
        _player1Mode = _gameSettings.player1Mode;
        _player2PalettePrefab = _gameSettings.player2PalettePrefab;
        _player2Mode = _gameSettings.player2Mode;
        _ballPrefab = _gameSettings.ballPrefab;
        _startScoreText = _gameSettings.startScoreText;

        _player1PaletteNum = _gameSettings._player1PaletteNum;
        _player2PaletteNum = _gameSettings._player2PaletteNum;
        _currentGameMode = _gameSettings.defaultGameMode;
        InstantiateComponents();
        GetPlayerNumber();
    }


    private void GetPlayerNumber()
    {
        _gameSettings._player1PaletteNum = GetMapValue(PlayerMap.map1);
        _gameSettings._player2PaletteNum = GetMapValue(PlayerMap.map2);
    }

    private void InstantiateComponents()
    {
        _ball = Utility.InstantiateBall(_ballPrefab, _ballStartPosition, transform);
        _player1Palette = Utility.InstantiatePalette(_player1PalettePrefab, _player1PaletteSpawnPosition, transform);
        _player2Palette = Utility.InstantiatePalette(_player2PalettePrefab, _player2PaletteSpawnPosition, transform);
    }

    public void InitializeGame()
    {
        InitializePalettes();
        StartGame();
    }

    private void InitializePalettes()
    {
        // Initialize the player 1 palette
        if (_player1Palette != null)
        {
            _player1Palette.Initialize(_player1Mode, _player1PaletteNum, _ball);
        }

        // Initialize the player 2 palette
        if (_player2Palette != null)
        {
            _player2Palette.Initialize(_player2Mode, _player2PaletteNum, _ball);
        }
    }

    private int GetMapValue(PlayerMap map)
    {
        return (int)map;
    }

    public void StartGame()
    {
        _scoreBoard.EnableScoreCanvas();
        _scoreBoard.SubscribeToBall(_ball);
        _scoreBoard.SetScoreText(_startScoreText);
        _ball.StartBall();
    }

    public void SetGameMode(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.SinglePlayer:
                _player1Mode = PlayerMode.Human;
                _player2Mode = PlayerMode.AI;
                _player1PaletteNum = 1;
                _player2PaletteNum = 2;
                _currentGameMode = GameMode.SinglePlayer;
                break;
            case GameMode.MultiPlayer:
                _player1Mode = PlayerMode.Human;
                _player2Mode = PlayerMode.Human;
                _player1PaletteNum = 1;
                _player2PaletteNum = 2;
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
                // Swap player sides
                PlayerMode tempMode = _player1Mode;
                _player1Mode = _player2Mode;
                _player2Mode = tempMode;

                // Swap player maps
                int tempMap = _player1PaletteNum;
                _player1PaletteNum = _player2PaletteNum;
                _player2PaletteNum = tempMap;
                break;
            case GameMode.MultiPlayer:
                int tempMapMulti = _player1PaletteNum;
                _player1PaletteNum = _player2PaletteNum;
                _player2PaletteNum = tempMapMulti;
                break;
            default:
                Debug.LogError("Invalid game mode.");
                break;
        }

        // Update palettes with new map values
        SetMapValue(PlayerMap.map1, _player1PaletteNum);
        SetMapValue(PlayerMap.map2, _player2PaletteNum);
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