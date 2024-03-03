using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameModeController _gameModeController;
    [SerializeField] Canvas _gameMenuCanvas;
    [SerializeField] GameMode _currentGameMode;

    private void OnEnable()
    {
        _gameModeController._scoreBoard.OnWin += EnableGameMenu;
    }

    private void OnDestroy()
    {
        _gameModeController._scoreBoard.OnWin -= EnableGameMenu;
    }

    private void Start()
    {
        _currentGameMode = _gameModeController._currentGameMode;
    }

    private void EnableGameMenu()
    {
        _gameMenuCanvas.enabled = true;
    }
    public void StartPong()
    {
        _gameMenuCanvas.enabled = false;
        _gameModeController.StartMatch();
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