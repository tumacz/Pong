using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _initialSpeed = 10f;
    [SerializeField] private float _maxSpeed = 14f;
    [SerializeField] private float _speedIncrease = 0.25f;
    [SerializeField] private string _paletteTag;
    [SerializeField] private string _aScoreTag;
    [SerializeField] private string _bScoreTag;

    private List<string> _tags;
    private Rigidbody2D _rb;
    public int hitCounter = 0;
    public event Action<string> OnScore;

    private void Start()
    {
        GetReferences(_paletteTag, _aScoreTag, _bScoreTag);
        StartBall();
    }

    private void GetReferences(params string[] tags)
    {
        _rb = GetComponent<Rigidbody2D>();
        _tags = new List<string>(tags);
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.sqrMagnitude > _maxSpeed * _maxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * _maxSpeed;
        }
    }


    private void StartBall()
    {
        float initialXSpeed = Mathf.Sign(Random.Range(-1f, 1f)) * _initialSpeed;
        float initialYSpeed = Random.Range(-1f, 1f) * _initialSpeed;
        _rb.velocity = new Vector2(initialXSpeed, initialYSpeed);
    }

    public void ResetBall()
    {
        _rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        StartBall();
        hitCounter = 0;
    }

    private void BounceBall(Transform player)
    {
        hitCounter++;
        Vector2 ballPosition = transform.position;
        Vector2 playerPosition = player.position;

        float xDirection = Mathf.Sign(transform.position.x - player.position.x);

        float yDirection = Mathf.Clamp((transform.position.y - player.position.y) / player.GetComponent<Collider2D>().bounds.size.y, -1f, 1f);
        yDirection += Random.Range(-0.25f, 0.25f);

        _rb.velocity = new Vector2(xDirection, yDirection) * (_initialSpeed + _speedIncrease * hitCounter);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;

        if (_tags.Contains(collisionTag))
        {
            if (collisionTag == _paletteTag)
            {
                BounceBall(collision.transform);
            }
            else if (collisionTag == _aScoreTag)
            {
                OnScore?.Invoke(_aScoreTag);
                ResetBall();
            }
            else if (collisionTag == _bScoreTag)
            {
                OnScore?.Invoke(_bScoreTag);
                ResetBall();
            }
        }
    }
}
