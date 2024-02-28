using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] private bool _isAI;
    [SerializeField] private float _AIThreshold = .5f;

    private PongControls _pongControls;

    private InputAction _moveUp;
    private InputAction _moveDown;

    private float _moveUpValue = 0f;
    private float _moveDownValue = 0f;

    private void Start()
    {
        _pongControls = new PongControls();

        _moveUp = _pongControls.PongMap.moveUp;
        _moveDown = _pongControls.PongMap.moveDown;

        _moveUp.started += ctx =>
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
        };
        _moveUp.canceled += ctx => ResetMoveUpValue();

        _moveDown.started += ctx =>
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
        };
        _moveDown.canceled += ctx => ResetMoveDownValue();

        _pongControls.Enable();
    }

    private void OnDestroy()
    {
        _pongControls.Disable();
    }

    private void Update()
    {
        if (_isAI)
        {
            AIControll();
        }
        else
        {
            float moveInput = _moveUpValue - _moveDownValue;
            if (Mathf.Approximately(moveInput, 0f))
                return;

            float yOffset = moveInput * movementSpeed * Time.deltaTime;
            float newYPosition = transform.position.y + yOffset;

            newYPosition = Mathf.Clamp(newYPosition, minY, maxY);
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        }
    }

    private void ResetMoveUpValue()
    {
        if (_moveDownValue > 0f)
        {
            _moveDownValue = 1f;
        }
        _moveUpValue = 0f;
    }

    private void ResetMoveDownValue()
    {
        if (_moveUpValue > 0f)
        {
            _moveUpValue = 1f;
        }
        _moveDownValue = 0f;
    }

    private void AIControll()
    {
        if (_ball != null)
        {
            float yDifference = Mathf.Abs(_ball.transform.position.y - transform.position.y);
            //movemend dependency on Threshold
            if (yDifference > _AIThreshold)
            {
                if (_ball.transform.position.y < transform.position.y)
                {
                    _moveUpValue = 0;
                    _moveDownValue = 1;
                }
                else if (_ball.transform.position.y > transform.position.y)
                {
                    _moveDownValue = 0;
                    _moveUpValue = 1;
                }
                else
                {
                    _moveDownValue = 0;
                    _moveUpValue = 0;
                }
            }
            //no movement values
            else
            {
                _moveDownValue = 0;
                _moveUpValue = 0;
            }
            //execute
            float yOffset = (_moveUpValue - _moveDownValue) * movementSpeed * Time.deltaTime;
            float newYPosition = transform.position.y + yOffset;
            newYPosition = Mathf.Clamp(newYPosition, minY, maxY);

            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        }
    }

}