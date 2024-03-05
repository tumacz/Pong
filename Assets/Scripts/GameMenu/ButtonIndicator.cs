using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndicator : MonoBehaviour
{
    [SerializeField] private GameMenuUIEffectManager _gameMenuUIEffectManager;

    private int _selectedIndex = 0;

    public void SelectNextButton()
    {
        _selectedIndex = (_selectedIndex + 1) % _gameMenuUIEffectManager.Buttons.Length;
        _gameMenuUIEffectManager.HighlightButton(_selectedIndex);
    }

    public void SelectPreviousButton()
    {
        _selectedIndex = (_selectedIndex - 1 + _gameMenuUIEffectManager.Buttons.Length) % _gameMenuUIEffectManager.Buttons.Length;
        _gameMenuUIEffectManager.HighlightButton(_selectedIndex);
    }

    public void PressSelectedButton()
    {
        _gameMenuUIEffectManager.Buttons[_selectedIndex].onClick.Invoke();
    }
}