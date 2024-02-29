using UnityEngine;

public enum PlayerMode
{
    Human,
    AI
}

public class GameModeController : MonoBehaviour
{
    [SerializeField] private Palette player1Palette;
    [SerializeField] private Palette player2Palette;
    [SerializeField] private PlayerMode player1Mode = PlayerMode.Human;
    [SerializeField] private PlayerMode player2Mode = PlayerMode.AI;


    void Start()
    {
        player1Palette.Initialize(player1Mode, 1);
        player2Palette.Initialize(player2Mode, 2);
    }

    //public void SetPlayer1Mode(PlayerMode mode)
    //{
    //    player1Mode = mode;
    //    player1Palette.SetPlayerMode(player1Mode);
    //}

    //public void SetPlayer2Mode(PlayerMode mode)
    //{
    //    player2Mode = mode;
    //    player2Palette.SetPlayerMode(player2Mode);
    //}
}

