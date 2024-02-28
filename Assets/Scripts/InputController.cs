using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PongControls _pongControls;
    private Palette _palette;

    public void Initialize(Palette palette)
    {
        _palette = palette;
        _pongControls = new PongControls();

        _pongControls.PongMap.moveUp.started += ctx =>
        {
            _palette.OnMoveUpStarted();
        };
        _pongControls.PongMap.moveDown.started += ctx =>
        {
            _palette.OnMoveDownStarted();
        };

        _pongControls.PongMap.moveUp.canceled += ctx => _palette.ResetMoveUpValue();
        _pongControls.PongMap.moveDown.canceled += ctx => _palette.ResetMoveDownValue();

        _pongControls.Enable();
    }

    public void Disable()
    {
        _pongControls.Disable();
    }
}

