using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PongControls _pongControls;
    private Palette _palette;
    public InputActionMap _player1;
    public InputActionMap _player2;
    public InputActionMap _currentPlayer;

    public void Initialize(Palette palette, int playerNumber)
    {
        _palette = palette;
        _pongControls = new PongControls();
        _player1 = _pongControls.PongMap;
        _player2 = _pongControls.PongMap2;


        if (playerNumber == 1)
        {
            _currentPlayer = _player1;
        }
        else if (playerNumber == 2)
        {
            _currentPlayer = _player2;
        }
        else
        {
            Debug.LogError("Nieprawid³owy numer paletki!");
            return;
        }


        _currentPlayer["moveUp"].started += ctx =>
        {
            _palette.OnMoveUpStarted();
        };
        _currentPlayer["moveDown"].started += ctx =>
        {
            _palette.OnMoveDownStarted();
        };

        _currentPlayer["moveUp"].canceled += ctx => _palette.ResetMoveUpValue();
        _currentPlayer["moveDown"].canceled += ctx => _palette.ResetMoveDownValue();

        _pongControls.Enable();
    }

    public void Disable()
    {
        _pongControls.Disable();
    }
}
