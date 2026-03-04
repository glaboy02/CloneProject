using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SnakeController : MonoBehaviour
{
    // [SerializeField] private GameObject snakeBodyPrefab;
    // [SerializeField] private GameObject foodPrefab;

    // private List<Transform> _childList;
    // private Transform _appleInGame;
    // private Vector3 _direction;
    // private Vector3 _savedDirection;
    // private Vector3 _target;

    // public const float Vel = 5f;

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     _target = transform.position;
    //     _direction = Vector3.right;
    //     _appleInGame = SpawnApple();
    // }

    // private void OnMove(InputValue movement)
    // {
    //     _direction = movement.Get<Vector2>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     transform.position = Vector3.MoveTowards(transform.position, _target, Vel * Time.deltaTime);

    //     if (_direction.x != 0)
    //     {
    //         _savedDirection = Vector3.right * _direction.x;
    //     }
    //     if (_direction.y != 0)
    //     {
    //         _savedDirection = Vector3.up * _direction.y;
    //     }

    //     if (transform.position != _target) return;


    // }


}
