using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Transform _enemy;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _gridPosition;
    [SerializeField] private Vector2 _velocity;

    [SerializeField] private bool _hitRight, _hitLeft, _hitUp, _hitDown;
    private void Awake()
    {
        _hitDown = false;
        _hitLeft = false;
        _hitRight = false;
        _hitUp = false;
        _gridPosition = new Vector2(_enemy.position.x, _enemy.position.y);
        InitVelocity();
    }

    void Update()
    {
        ChangeDirection();
        Movement();
       
    }

    private void Movement()
    {
        _enemy.position += new Vector3(_velocity.x, _velocity.y, 0) * _speed* Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyPosition = _enemy.position;
        var zeroVector = new Vector3(0, 0, 0);
        if(collision.CompareTag("Boundaries"))
        {
            
            if(enemyPosition.x < zeroVector.x && enemyPosition.y > zeroVector.y)
            {

                  _hitLeft = true;
                  _hitRight = false;
                  _hitUp = true;
                  _hitDown = false;
                //_velocity = new Vector2(1, -1);
                Debug.Log("HitLeft");
            }
            if(enemyPosition.x > zeroVector.x && enemyPosition.y < zeroVector.y)
            {
                _hitRight = true;
                _hitLeft = false;
                _hitDown = true;
                _hitUp = false;
                //_velocity = new Vector2(-1, 1);
                Debug.Log("HitRight");
            }
            if(enemyPosition.y > zeroVector.y && enemyPosition.x > zeroVector.x)
            {
                _hitUp = true;
                _hitDown = false;
                _hitLeft = false;
                _hitRight = true;
                //_velocity = new Vector2(-1, -1);
                Debug.Log("HitUP");
            }
            if(enemyPosition.y < zeroVector.y && enemyPosition.y > zeroVector.y)
            {
                _hitDown = true;
                _hitUp = false;
                _hitRight = false;
                _hitLeft = true;
                //_velocity = new Vector2(1, 1);
                Debug.Log("HitDown");
            }
        }
    }

    private void InitVelocity()
    {
        if(_gridPosition.x < 0 && _gridPosition.y > 0)
        {
            _velocity = new Vector2(-1, 1);
        }
        else if (_gridPosition.x < 0 && _gridPosition.y < 0)
        {
            _velocity = new Vector2(1, 1);
        }
        else if (_gridPosition.x > 0 && _gridPosition.y > 0)
        {
            _velocity = new Vector2(-1, -1);
        }
        else if (_gridPosition.x > 0 && _gridPosition.y < 0)
        {
            _velocity = new Vector2(1, -1);
        }
    }

    private void ChangeDirection()
    {
        if (_hitDown == true)
        {
            _velocity.y = Mathf.Abs(_velocity.y);
            _velocity.x = Mathf.Abs(_velocity.x);
        }
           
        else if (_hitUp == true)
        {
            _velocity.y = -Mathf.Abs(_velocity.y);
            _velocity.x = -Mathf.Abs(_velocity.x);
        }
           
        else if (_hitRight == true)
        {
            _velocity.x = -Mathf.Abs(_velocity.x);
            _velocity.y = -Mathf.Abs(_velocity.y);
        }
            
        else if (_hitLeft == true)
        {
            _velocity.x = Mathf.Abs(_velocity.x);
            _velocity.y = Mathf.Abs(_velocity.y);
        }
            
    }
}
