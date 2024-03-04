using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndicator : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    private int _selectedIndex = 0;

    private void Start()
    {
        HighlightButton(_selectedIndex);
    }

    private void HighlightButton(int index)
    {
        // Wy³¹cz podœwietlenie wszystkich przycisków
        foreach (Button button in _buttons)
        {
            var colors = button.colors;
            colors.normalColor = Color.white;
            button.colors = colors;
        }

        // W³¹cz podœwietlenie wybranego przycisku
        var selectedColors = _buttons[index].colors;
        selectedColors.normalColor = Color.yellow;
        _buttons[index].colors = selectedColors;
    }

    public void SelectNextButton()
    {
        _selectedIndex = (_selectedIndex + 1) % _buttons.Length;
        HighlightButton(_selectedIndex);
    }

    public void SelectPreviousButton()
    {
        _selectedIndex = (_selectedIndex - 1 + _buttons.Length) % _buttons.Length;
        HighlightButton(_selectedIndex);
    }

    public void PressSelectedButton()
    {
        _buttons[_selectedIndex].onClick.Invoke();
    }
}
