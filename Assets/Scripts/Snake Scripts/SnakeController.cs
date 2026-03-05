using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private GameObject snakeBodyPrefab;
    [SerializeField] private GameObject foodPrefab;

    private List<Transform> _childList;
    private List<SnakeBody> _bodyComponents;
    private List<Vector3> _positionHistory;
    private Transform _appleInGame;
    private Vector3 _direction;
    private Vector3 _savedDirection;
    private Vector3 _target;

    public const float Vel = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _childList = new List<Transform>();
        _bodyComponents = new List<SnakeBody>();
        _positionHistory = new List<Vector3>();
        _target = transform.position;
        _direction = Vector3.right;
        _savedDirection = Vector3.right;
        _appleInGame = SpawnApple();
    }

    private void OnMove(InputValue movement)
    {
        _direction = movement.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, Vel * Time.deltaTime);

        if (_direction.x != 0)
        {
            _savedDirection = Vector3.right * _direction.x;
        }
        if (_direction.y != 0)
        {
            _savedDirection = Vector3.up * _direction.y;
        }

        if (transform.position != _target) return;

        _positionHistory.Insert(0, _target);

        _target += _savedDirection;

        // ChecksOutOfBounds();
        SetChildrenTargets();
    }

    private void SetChildrenTargets()
    {
        // if (_childList == null) return;
        // if (_childList.Count > 0)
        // {
        //     _childList[0].GetComponent<SnakeBody>().SetTargetPosition(transform.position);

        //     for (int index = _childList.Count - 1; index > 0; index--)
        //     {
        //         _childList[index].GetComponent<SnakeBody>().SetTargetPosition(_childList[index - 1].position);
        //     }
        // }
        for (int i = 0; i < _bodyComponents.Count; i++)
        {
            if (i < _positionHistory.Count)
            {
                _bodyComponents[i].SetTargetPosition(_positionHistory[i]);
            }
        }
    }

    // private void ChecksOutOfBounds()
    // {
    //     if (_target.x is >= 10f or <= -10.25f || _target.y is >= 5.2f or <= -5.2f)
    //     {
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            EatsFood();
        }
        else if (other.CompareTag("Body"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (other.CompareTag("Wall"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void EatsFood()
    {
        Destroy(_appleInGame.gameObject);

        // Spawn at the tail's position, or at the head if no body exists yet
        int historyIndex = _childList.Count;
        Vector3 spawnPosition = historyIndex < _positionHistory.Count
            ? _positionHistory[historyIndex]
            : (_childList.Count > 0 ? _childList[_childList.Count - 1].position : transform.position);


        var obj = Instantiate(snakeBodyPrefab, spawnPosition, Quaternion.identity);
        var snakeBody = obj.GetComponent<SnakeBody>();
        snakeBody.Initialize(spawnPosition);
        // snakeBody.WaitHeadUpdateCycles(_childList.Count);

        obj.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(ActivateBodyCollider(obj));

        _childList.Add(obj.transform);
        _bodyComponents.Add(snakeBody);

        _appleInGame = SpawnApple();
    }

    private IEnumerator ActivateBodyCollider(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);
        obj.GetComponent<BoxCollider2D>().enabled = true;
    }

    private Transform SpawnApple()
    {
        return Instantiate(foodPrefab, new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0), Quaternion.identity).transform;
    }

}
