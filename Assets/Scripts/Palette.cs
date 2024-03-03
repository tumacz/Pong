using UnityEngine;

public class Palette : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _minYRange = -5f;
    [SerializeField] private float _maxYRange = 5f;
    [SerializeField] private float _aiActivationThreshold = 0.5f;

    [SerializeField] private AIController _aiController;
    [SerializeField] private InputController _inputController;

    private Ball _ball;
    private PlayerState _playerMode;
    private float _moveUpValue = 0f;
    private float _moveDownValue = 0f;
    private int _playerNumber;

    public void Initialize(PlayerState mode, int playerNumber, Ball ball)
    {
        _ball = ball;
        _playerMode = mode;
        _playerNumber = playerNumber;
        _inputController.Initialize(this, _playerNumber);
    }

    public void OnMatchFinished()
    {
        if (_inputController != null)
        {
            _inputController.Disable();
        }
    }

    public void SetPlayerMode(PlayerState mode)
    {
        _playerMode = mode;
    }

    public void SetPlayerNumber(int playerNumber)
    {
        _playerNumber = playerNumber;
    }

    public void OnMoveUpStarted()
    {
        if (_moveDownValue > 0f)
        {
            _moveDownValue = 1f;
            _moveUpValue = 2f;
        }
        else
        {
            _moveUpValue = 1f;
        }
    }

    public void OnMoveDownStarted()
    {
        if (_moveUpValue > 0f)
        {
            _moveUpValue = 1f;
            _moveDownValue = 2f;
        }
        else
        {
            _moveDownValue = 1f;
        }
    }

    public void ResetMoveUpValue()
    {
        if (_moveDownValue > 0f)
        {
            _moveDownValue = 1f;
        }
        _moveUpValue = 0f;
    }

    public void ResetMoveDownValue()
    {
        if (_moveUpValue > 0f)
        {
            _moveUpValue = 1f;
        }
        _moveDownValue = 0f;
    }

    private void Update()
    {
        if (_playerMode == PlayerState.Computer)
        {
            _aiController.AIControll(_ball, _aiActivationThreshold, _movementSpeed, _minYRange, _maxYRange, ref _moveUpValue, ref _moveDownValue);
        }
        else
        {
            float moveInput = _moveUpValue - _moveDownValue;
            Move(moveInput);
        }
    }

    private void Move(float moveInput)
    {
        if (Mathf.Approximately(moveInput, 0f))
        {
            return;
        }

        float yOffset = moveInput * _movementSpeed * Time.deltaTime;
        float newYPosition = transform.position.y + yOffset;

        newYPosition = Mathf.Clamp(newYPosition, _minYRange, _maxYRange);
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }
}