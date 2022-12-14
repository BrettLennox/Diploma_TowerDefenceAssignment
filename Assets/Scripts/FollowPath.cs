using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [Header("Waypoint Info")]
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    private int _moveIndex = 0;
    private float offset = 0.3f;
    [Header("Stats")]
    public float _moveSpeed;
    [SerializeField] private int _maxHP = 20;
    public int currentHP;

    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = _maxHP;

        NewWaypoints();
    }

    private void NewWaypoints()
    {
        _waypoints.Clear();
        AStar pathFinder = GameObject.Find("GameManager").GetComponent<AStar>();
        foreach(Node node in pathFinder.FindShortestPath(pathFinder.start, pathFinder.end))
        {
            _waypoints.Add(node.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _waypoints[_moveIndex].transform.position) > offset)
        {
            dir = _waypoints[_moveIndex].position - transform.position;
            dir.Normalize();
            transform.Translate((dir * _moveSpeed) * Time.deltaTime);
        }
        

        if (Vector3.Distance(transform.position, _waypoints[_moveIndex].transform.position) <= offset && _moveIndex < _waypoints.Count - 1)
        {
            _moveIndex++;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        if(currentHP <= 0)
        {
            Debug.Log(this.gameObject.name + "is now dead");
            _moveSpeed = 0;
            GameManager.instance.Enemies.Remove(this.gameObject);
            Destroy(this.gameObject, 1f);
        }
    }
}
