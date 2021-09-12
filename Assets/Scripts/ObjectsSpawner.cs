using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    private float UpdateTimer;
    private float powerUpUpdateTimer;
    bool update = true;
    bool powerUpUpdate;
    int ObstacleChooser;
    int ObstacleChooser1;
    int ObstacleChooser2;
    int powerUpsChooser;
    int positionChooser;
   
    [Header ("GameObjects")]
    public GameObject[] Obstacles;
    public GameObject[] powerups;
    
    Vector3 position = new Vector3(0.65f, -1f, 8f);
    Vector3 position1 = new Vector3(0.3f, -1f, 8f);
    Vector3 position2 = new Vector3(-0.1f, -1f, 8f);
    Quaternion degree = new Quaternion(0f, 180f, 0f, 0f);
    Vector3[] positionArray;

    private void Update()
    {
        InstantiateObstacles();
        InstantiatePowerUps();
        
    }
    void Start()
    {
        positionArray = new Vector3[] { new Vector3(position.x, position.y, gameObject.transform.position.z), new Vector3(position1.x, position1.y, gameObject.transform.position.z), new Vector3(position2.x, position2.y, gameObject.transform.position.z) };
    }
    void InstantiateObstacles()
    {
        if (update == true)
        {

            positionChooser = Random.Range(0, 3);
            powerUpsChooser = Random.Range(0, 3);
            ObstacleChooser = Random.Range(0, 6);
            ObstacleChooser1 = Random.Range(0, 6);
            ObstacleChooser2 = Random.Range(0, 6);
            Instantiate(Obstacles[ObstacleChooser], position, degree);
            Instantiate(Obstacles[ObstacleChooser1], position1, degree);
            Instantiate(Obstacles[ObstacleChooser2], position2, degree);


            update = false;
            Destroy(GameObject.Find("Car1 1(Clone)"), 20f);
            Destroy(GameObject.Find("Car2 1(Clone)"), 20f);
            Destroy(GameObject.Find("Car3 1(Clone)"), 20f);
            Destroy(GameObject.Find("Obstacle 2 2(Clone)"), 20f);
            Destroy(GameObject.Find("Obstacle 1 2(Clone)"), 20f);
            Destroy(GameObject.Find("Obstacle 3(Clone)"), 20f);
        }
        UpdateTimer += Time.deltaTime;
        if (UpdateTimer > 3f)
        {
            UpdateTimer = 0f;
            update = true;
            position.z = transform.position.z + Random.Range(-2f, 8f);
            position1.z = transform.position.z + Random.Range(-2f, 8f);
            position2.z = transform.position.z + Random.Range(-2f, 8f);
            positionArray = new Vector3[] { new Vector3(position.x, position.y, gameObject.transform.position.z), new Vector3(position1.x, position1.y, gameObject.transform.position.z), new Vector3(position2.x, position2.y, gameObject.transform.position.z) };
        }
    }
    void InstantiatePowerUps()
    {
        powerUpUpdateTimer += Time.deltaTime;
        if (powerUpUpdate)
        {
            positionArray = new Vector3[] { new Vector3(position.x, position.y, gameObject.transform.position.z), new Vector3(position1.x, position1.y, gameObject.transform.position.z), new Vector3(position2.x, position2.y, gameObject.transform.position.z) };
            Instantiate(powerups[powerUpsChooser], positionArray[positionChooser], degree);
            powerUpUpdate = false;
        }
        if (powerUpUpdateTimer > 10f)
        {
            powerUpUpdateTimer = 0f;
            powerUpUpdate = true;
        }
    }
}
