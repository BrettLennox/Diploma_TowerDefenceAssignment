using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int _attackDamage = 1;
    [SerializeField] private float _attackRate = 2;
    [SerializeField] private int _weight = 1000;

    [Header("Attack Range")]
    [SerializeField] private GameObject _attackRadius;

    private float timer = Mathf.Infinity;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; //increments timer by deltaTime
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node") //if the trigger that enters is tagged Node
        {
            Node node = other.GetComponent<Node>(); //gets the Node component
            node.towerWeight = _weight; //sets node towerWeight to weight
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node") //if the trigger that exits is tagged Node
        {
            Node node = other.GetComponent<Node>(); //gets the Node component
            node.towerWeight = 0; //sets node towerWeight to 0
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Boat") //if the trigger that stays is tagged Boat
        {
            if (timer >= _attackRate && GameManager.instance.IsAlive == true) //if timer is greater than or equal to attackRate && GameManagers IsAlive bool is true
            {
                timer = 0; //resets timer to 0
                other.GetComponentInParent<FollowPath>().TakeDamage(_attackDamage); //runs TakeDamage function from the parent gameobject of the trigger
            }
        }
    }
}
