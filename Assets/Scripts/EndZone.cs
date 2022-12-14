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
        if(other.tag == "Boat") //if the trigger that enters is tagged Boat
        {
            Destroy(other.gameObject, 0.5f); //Destroy boat gameObject after 0.5 seconds
            _gm.Health -= 1; //decreases Health on GameManager by 1
            if(_gm.Health <= 0) //if Health is less than or equal to 0
            {
                _gm.IsAlive = false; //sets IsAlive on GameManager to false
            }
        }
    }
}
