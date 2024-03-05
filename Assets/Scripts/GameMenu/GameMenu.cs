using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameModeController _gameModeController;
    [SerializeField] private Canvas _gameMenuCanvas;
    [SerializeField] private GameMenuUIEffectManager _gameMenuUIEffectManager;
    [SerializeField] private MenuInputController _menuInputController;

    private GameMode _currentGameMode => _gameModeController.CurrentGameMode;

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
        _gameMenuUIEffectManager.UpdateChangeGameModeButtonText(_currentGameMode);
    }

    private void EnableGameMenu()
    {
        _gameMenuCanvas.enabled = true;
        _menuInputController.EnableMenuControls();

    }

    public void StartPong()
    {
        _gameMenuCanvas.enabled = false;
        _menuInputController.DisableMenuControls();
        _gameModeController.StartMatch();
    }

    public void ChangeSide()
    {
        _gameModeController.ChangePlayerSide(_currentGameMode);
    }

    public void ChangeGameMode()
    {
        GameMode newMode = _currentGameMode == GameMode.SinglePlayer ? GameMode.MultiPlayer : GameMode.SinglePlayer;
        _gameModeController.SetGameMode(newMode);
        _gameMenuUIEffectManager.UpdateChangeGameModeButtonText(_currentGameMode);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}