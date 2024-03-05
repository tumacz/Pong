using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUIEffectManager : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    private int _selectedIndex = 0;

    [SerializeField] private Button _startPongButton;
    [SerializeField] private Button _changeGameModeButton;
    [SerializeField] private Button _changeSideButton;
    [SerializeField] private Button _quitAppButton;

    public Button[] Buttons => _buttons;

    private void Start()
    {
        HighlightButton(_selectedIndex);
    }

    public void HighlightButton(int index)
    {
        foreach (Button button in _buttons)
        {
            var colors = button.colors;
            colors.normalColor = Color.white;
            button.colors = colors;
        }

        var selectedColors = _buttons[index].colors;
        selectedColors.normalColor = Color.yellow;
        _buttons[index].colors = selectedColors;
    }

    public void UpdateChangeGameModeButtonText(GameMode newMode)
    {
        string buttonText = newMode == GameMode.SinglePlayer ? "Multiplayer" : "Singleplayer";
        _changeGameModeButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
    }
}