using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PongControls _pongControls;
    private Palette _palette;
    private InputAction _moveUpAction;
    private InputAction _moveDownAction;
    private int _playerNumber;

    public void Initialize(Palette palette, int playerNumber)
    {
        _palette = palette;
        _pongControls = new PongControls();
        _playerNumber = playerNumber;

        if (_moveUpAction == null && _moveDownAction == null)
        {
            if (_playerNumber == 1)
            {
                _moveUpAction = _pongControls.PongMap.moveUp;
                _moveDownAction = _pongControls.PongMap.moveDown;
            }
            else if (_playerNumber == 2)
            {
                _moveUpAction = _pongControls.PongMap2.moveUp;
                _moveDownAction = _pongControls.PongMap2.moveDown;
            }
            else
            {
                Debug.LogError("Incorrect palette number!");
                return;
            }

            _moveUpAction.started += ctx => _palette.OnMoveUpStarted();
            _moveDownAction.started += ctx => _palette.OnMoveDownStarted();
            _moveUpAction.canceled += ctx => _palette.ResetMoveUpValue();
            _moveDownAction.canceled += ctx => _palette.ResetMoveDownValue();

            _pongControls.Enable();
        }
        else
        {
            Debug.LogWarning("Actions are already assigned.");
        }
    }

    public void Disable()
    {
        if (_pongControls != null)
        {
            _moveUpAction.started -= ctx => _palette.OnMoveUpStarted();
            _moveDownAction.started -= ctx => _palette.OnMoveDownStarted();
            _moveUpAction.canceled -= ctx => _palette.ResetMoveUpValue();
            _moveDownAction.canceled -= ctx => _palette.ResetMoveDownValue();

            _pongControls.Disable();
        }
    }
}