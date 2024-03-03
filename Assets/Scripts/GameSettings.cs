using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public static GameSettings Instance { get; private set; }

    [Header("Left Side Player")]
    [Tooltip("Prefab representing the left side player palette.")]
    public GameObject Player1PalettePrefab;
    public PlayerState Player1Mode;

    [Header("Right Side Player")]
    [Tooltip("Prefab representing the right side player palette.")]
    public GameObject Player2PalettePrefab;
    public PlayerState Player2Mode;

    [Header("Ball")]
    [Tooltip("Prefab representing the ball.")]
    public GameObject BallPrefab;
    public GameMode DefaultGameMode;

    public int Player1PaletteNum;
    public int Player2PaletteNum;

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