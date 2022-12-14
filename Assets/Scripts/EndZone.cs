using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    [SerializeField] private GameManager _gm;

    private void Awake()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boat")
        {
            Debug.Log(other.name + " made it to the end.");
            Destroy(other.gameObject, 0.5f);
            _gm.Health -= 1;
            if(_gm.Health <= 0)
            {
                _gm.IsAlive = false;
            }
        }
    }
}
