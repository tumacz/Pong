using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUIEffectManager : MonoBehaviour
{
    [SerializeField] private Button _startPongButton;
    [SerializeField] private Button _changeGameModeButton;
    [SerializeField] private Button _changeSideButton;
    [SerializeField] private Button _quitAppButton;
    [SerializeField] private ButtonIndicator _buttonIndicator;

    public void UpdateChangeGameModeButtonText(GameMode newMode)
    {
        string buttonText = newMode == GameMode.SinglePlayer ? "Multiplayer" : "Singleplayer";
        _changeGameModeButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
    }
    //temporary
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            _buttonIndicator.SelectPreviousButton();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            _buttonIndicator.SelectNextButton();
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            _buttonIndicator.PressSelectedButton();
        }
    }
}
