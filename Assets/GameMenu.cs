using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameModeController _gameModeController;
    [SerializeField] Canvas _gameMenuCanvas;
    [SerializeField] GameMode _currentGameMode;

    private void Start()
    {
        _currentGameMode = _gameModeController._currentGameMode;
    }
    public void StartPong()
    {
        _gameModeController.StartGame();
        _gameMenuCanvas.enabled = false;
    }

    public void ChangeSide()
    {
        _gameModeController.ChangePlayerSide(_currentGameMode);
    }

    public void SetMultiplayerMode()
    {
        _gameModeController.SetGameMode(GameMode.MultiPlayer);
    }
}