using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	[SerializeField] private float waitTime;
	[SerializeField] private GameObject[] obstaclePrefabs;
	private float tempTime;
    public float direction = 0.0f;


    void Start(){
		tempTime = waitTime - Time.deltaTime;
	}

	void LateUpdate () {
		if(GameManager.Instance.GameState())
        {
			tempTime += Time.deltaTime;
			if(tempTime > waitTime)
            {
				// Wait for some time, create an obstacle, then set wait time to 0 and start again
				tempTime = 0;
				GameObject pipeClone = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], transform.position * direction, transform.rotation);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
    {
		if(col.GetComponent<CoinsMovement>() != null)
        {
			Destroy(col.gameObject);
            Debug.Log("BOOM", col.gameObject);
		}
	}

}
