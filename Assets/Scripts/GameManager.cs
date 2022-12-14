using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int _health = 10;
    [SerializeField] private bool _isAlive = true;

    [Header("Enemy Data")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemySpawnLocation;
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private GameObject _enemyParent;
    [SerializeField] private List<GameObject> _enemies;

    [Header("Tower Data")]
    [SerializeField] private GameObject _towerPrefab;
    GameObject tower;

    [Header("UI")]
    [SerializeField] private GameObject _gameOverPanel;

    float timer;

    public static GameManager instance;

    public int Health { get => _health; set => _health = value; }
    public bool IsAlive { get => _isAlive; set => _isAlive = value; }
    public List<GameObject> Enemies { get => _enemies; set => _enemies = value; }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive) //if isAlive is true
        {
            timer += Time.deltaTime; //increments timer by Time.deltaTime

            if (timer >= _spawnDelay) //if timer is greater than or equal to spawnDelay
            {
                timer = 0; //resets timer to 0
                SpawnEnemy(); //runs SpawnEnemy function
            }

            if (Input.GetMouseButton(0)) //if user inputs MouseButton 0
            {
                PlaceTower(); //runs PlaceTower function
            }
        }
        else //if isAlive is not true
        {
            //enables the gameOverPanel
            _gameOverPanel.SetActive(true);
            foreach(GameObject boat in _enemies)
            {
                boat.GetComponent<FollowPath>()._moveSpeed = 0;
            }
        }
    }

    private void PlaceTower()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) //casts a ray and outputs hitinfo to hit
        {
            if (hit.transform.tag == "Land") //if hit gameobjects tag is land
            {
                if (tower == null) //if tower is currently null
                {
                    tower = Instantiate(_towerPrefab, hit.transform); //instantiates towerPrefab and sets tower variable to it
                    //tower.transform.position = new Vector3(hit.transform.position.x, 1, hit.transform.position.z); 
                }
                tower.transform.position = new Vector3(hit.transform.position.x, 1, hit.transform.position.z); //sets towers position to the hit Land gameobjects position with Y offset
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject boat = Instantiate(_enemyPrefab, _enemySpawnLocation.transform.position, Quaternion.identity, _enemyParent.transform); //Instantiates enemyPrefab at enemySpawnLocation position
        _enemies.Add(boat); //adds spawned boat into enemies list
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(1); //loads scene with index 1
    }
    public void QuitGameButton()
    {
        Application.Quit(); //quits the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
