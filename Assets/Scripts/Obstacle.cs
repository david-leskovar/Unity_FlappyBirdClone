using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;



    [SerializeField]
    private Player player;

    private enum ObstacleState
    {
        Moving,
        Stopped
    }

    [SerializeField]
    
    private float moveSpeed = 5.0f;
    private ObstacleState currentState = ObstacleState.Moving;

    void Start()
    {
        player.OnPlayerDead += HandlePlayerDead;
    }

    private void HandlePlayerDead(object sender, EventArgs e)
    {
        this.currentState = ObstacleState.Stopped;
    }

    void Update()
    {
        if (currentState == ObstacleState.Moving)
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            Debug.Log(moveSpeed * Time.deltaTime);  
            if (transform.position.x < -20f)
            {

                SpawnObstacle();
                Destroy(gameObject);
            }
        }
    }

    private void SpawnObstacle()
    {
       
        GameObject obstacle = Instantiate(prefab, new Vector3(16f, UnityEngine.Random.Range(-2f, 2f), 0f), Quaternion.identity);
        GameObject scorerGO = obstacle.transform.Find("Scorer")?.gameObject;

        scorerGO.SetActive(true);




    }
}
