using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour {
	
	[SerializeField] private float moveSpeed;
    [SerializeField] private ObstacleSpawner obstacleSpawner;

    private void Start()
    {
        obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
    }


    void Update ()
    {
        if (GameManager.Instance.GameState())
        {
            if (obstacleSpawner.direction == 1) 
            transform.position = new Vector2(transform.position.x - Time.deltaTime * moveSpeed, transform.position.y);
            
            else if (obstacleSpawner.direction == -1)
            transform.position = new Vector2(transform.position.x + Time.deltaTime *moveSpeed, transform.position.y);

            Destroy(gameObject, 10f);
		}
	}
}
