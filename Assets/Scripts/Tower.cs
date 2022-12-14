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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node")
        {
            Node node = other.GetComponent<Node>();
            node.towerWeight = _weight;
            //Debug.LogWarning(other.name);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            Node node = other.GetComponent<Node>();
            node.towerWeight = 0;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Boat")
        {
            if (timer >= _attackRate && GameManager.instance.IsAlive == true)
            {
                timer = 0;
                other.GetComponentInParent<FollowPath>().TakeDamage(_attackDamage);
            }
        }
    }
}
