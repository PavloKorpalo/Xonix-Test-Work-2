using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private Vector2 _lastPosition;
    [SerializeField] private Transform _wall;
    [SerializeField] private Transform _texture;
    [SerializeField] private Vector2Int _gridPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _someNumber;
    [SerializeField] private bool _isMoving;
    private bool _isWallDestroyed;
    private float _timeSpawn;

    
    

    private void Awake()
    {
        _texture.localScale = new Vector2(1, 1);
        _isMoving = false;
        _isWallDestroyed = false;
        _startPosition = new Vector2(_player.position.x, _player.position.y);
       

    }

  

    void Update()
    {
        if (ShouldSpawn())
        {
            DrawnLine();
        }
        Movement();
        
    }

    private void Movement()
    {
        if (Input.GetKey("a"))
        {
            _gridPosition.x -= 1;
        }
        else if (Input.GetKey("w"))
        {
            _gridPosition.y += 1;
        }
        else if (Input.GetKey("s"))
        {
            _gridPosition.y -= 1;
        }
        else if (Input.GetKey("d"))
        {
            _gridPosition.x += 1;
        }

        
        _player.position = new Vector2(_gridPosition.x, _gridPosition.y) * _speed;
    }

    private void DrawnLine()
    {
        _timeSpawn = Time.time + _spawnDelay;
       
        Vector2 wallPosition = new Vector2(_gridPosition.x, _gridPosition.y);


       if (_isMoving == true && transform.hasChanged)
        {
            Instantiate(_wall, wallPosition, _wall.rotation);
           
            
            
            _player.transform.hasChanged = false;
            
        }
       
    }

    private bool ShouldSpawn()
    {
        return Time.time >= _timeSpawn;
    }

   

    private void DestroyAll(string tag)
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < walls.Length; i++)
        {
            Destroy(walls[i]);
        }
    }

    private void FindCornerWall(string tag)
    {
        Vector2 cornerWall = new Vector2(0,0);
       
        Vector2 wallDeltaX = new Vector2(0,0);
        Vector2 wallDeltaY = new Vector2(0,0);
        var Magnitude = (_lastPosition + _startPosition)/2;
        Debug.Log("Magnitude:" + Magnitude);
        
        GameObject[] walls = GameObject.FindGameObjectsWithTag(tag);
         for (int i = 0; i < walls.Length; i++)
         {

            if (i > 0 && i < walls.Length - 1)
            {
                if ((walls[i].transform.position.y == walls[i - 1].transform.position.y && walls[i].transform.position.x == walls[i + 1].transform.position.x) ||
                    (walls[i].transform.position.x == walls[i - 1].transform.position.x && walls[i].transform.position.y == walls[i + 1].transform.position.y))
                {
                    Debug.Log(i);
                    cornerWall = new Vector2(walls[i].transform.position.x, walls[i].transform.position.y);


                    Debug.Log("Corner Wall:" + cornerWall);
                    wallDeltaX.x = (cornerWall.x + _lastPosition.x)/5;
                    wallDeltaY.y = (cornerWall.y + _startPosition.y)/5;
                    _texture.localScale = new Vector2(wallDeltaX.x, wallDeltaY.y);
                    Debug.Log("Scale: " + _texture.localScale);


                }
              
               

            }         
         }

        
        Instantiate(_texture, Magnitude, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boundaries")
        {
            _isMoving = false;
            _isWallDestroyed = true;
            _lastPosition = _player.transform.position;
            Debug.Log("Last position:" + _lastPosition);
            FindCornerWall("Wall");
            DestroyAll("Wall");
        }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Boundaries")
        {
            _isMoving = true;
            _isWallDestroyed = false;
            _startPosition = _player.transform.position;
            Debug.Log("Start Position:" + _startPosition);
            
        }
    }

    private void FillScene()
    {
        if(_isWallDestroyed == true)
        {
            
        }
    }
}
