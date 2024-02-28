using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerMode
{
    Human,
    AI
}

public class Palette : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private AIController _aiController;
    [SerializeField] private InputController _inputController;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] private PlayerMode _playerMode;
    [SerializeField] private float _AIThreshold = .5f;

    private float _moveUpValue = 0f;
    private float _moveDownValue = 0f;

    private void Start()
    {
        _inputController = GetComponent<InputController>();
        _inputController.Initialize(this);
    }

    private void OnDestroy()
    {
        _inputController = GetComponent<InputController>();
        _inputController.Disable();
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
        if (_playerMode == PlayerMode.AI)
        {
            _aiController.AIControll(_ball, _AIThreshold, movementSpeed, minY, maxY, ref _moveUpValue, ref _moveDownValue);
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
            return;

        float yOffset = moveInput * movementSpeed * Time.deltaTime;
        float newYPosition = transform.position.y + yOffset;

        newYPosition = Mathf.Clamp(newYPosition, minY, maxY);
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }
}