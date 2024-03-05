using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputController : MonoBehaviour
{
    [SerializeField] private ButtonIndicator _buttonIndicator;

    private MenuControls _menuControls;
    private InputAction _indicatorUpAction;
    private InputAction _indicatorDownAction;
    private InputAction _indicatorConfirmAction;

    private void Start()
    {
        _menuControls = new MenuControls();

        if (_indicatorUpAction == null)
        {
            _indicatorUpAction = _menuControls.MenuMap.IndicatorUp;
        }
        if (_indicatorDownAction == null)
        {
            _indicatorDownAction = _menuControls.MenuMap.IndicatorDown;
        }
        if (_indicatorConfirmAction == null)
        {
            _indicatorConfirmAction = _menuControls.MenuMap.ConfirmIndicated;
        }

        _indicatorUpAction.performed += ctx => _buttonIndicator.SelectPreviousButton();
        _indicatorDownAction.performed += ctx => _buttonIndicator.SelectNextButton();
        _indicatorConfirmAction.performed += ctx => _buttonIndicator.PressSelectedButton();

        EnableMenuControls();
    }

    public void EnableMenuControls()
    {
        _menuControls.Enable();
    }

    public void DisableMenuControls()
    {
        _menuControls.Disable();
    }

    private void OnDestroy()
    {
        _indicatorUpAction.performed -= ctx => _buttonIndicator.SelectPreviousButton();
        _indicatorDownAction.performed -= ctx => _buttonIndicator.SelectNextButton();
        _indicatorConfirmAction.performed -= ctx => _buttonIndicator.PressSelectedButton();

        DisableMenuControls();
    }
}