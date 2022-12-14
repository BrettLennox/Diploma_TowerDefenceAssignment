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
        //clears the waypoints list
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
        //if Distance from transform position to the waypoints at moveIindex transform position is greater than offset
        if (Vector3.Distance(transform.position, _waypoints[_moveIndex].transform.position) > offset)
        {
            dir = _waypoints[_moveIndex].position - transform.position; //dir is set to waypoints at moveIndex position - transform position
            dir.Normalize(); //normalizes Dir
            transform.Translate((dir * _moveSpeed) * Time.deltaTime); //transform position is updated by dir * moveSpeed * deltaTime
        }
        
        //if Distance from transform position to the waypoints at moveIndex transform position is less than or equal to offset && moveIndex is less than waypoints list count -1
        if (Vector3.Distance(transform.position, _waypoints[_moveIndex].transform.position) <= offset && _moveIndex < _waypoints.Count - 1)
        {
            _moveIndex++; //increments moveIndex
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount; //decreases currentHP by amount passed in

        if(currentHP <= 0) //if currentHP is less than or equal to 0
        {
            _moveSpeed = 0; //sets moveSpeed to 0
            GameManager.instance.Enemies.Remove(this.gameObject); //removes this gameobject from Enemies list on GameManager
            Destroy(this.gameObject, 1f); //Destroys this gameobject after 1 second
        }
    }
}
