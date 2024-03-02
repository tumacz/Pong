using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public static GameSettings Instance { get; private set; }

    [Header("Left Side Player")]
    [Tooltip("Prefab representing the left side player palette.")]
    public GameObject player1PalettePrefab;
    public PlayerMode player1Mode;

    [Header("Right Side Player")]
    [Tooltip("Prefab representing the right side player palette.")]
    public GameObject player2PalettePrefab;
    public PlayerMode player2Mode;

    [Header("Ball")]
    [Tooltip("Prefab representing the ball.")]
    public GameObject ballPrefab;

    public string startScoreText;
    public GameMode defaultGameMode;

    public int _player1PaletteNum;
    public int _player2PaletteNum;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of GameSettings already exists.");
        }
    }
}