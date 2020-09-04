using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
    public GameObject coinPrefab;
    Vector2 spawnPoint;
    public float Direction;

    [SerializeField] private float waitTime;
    [SerializeField] private Transform OppositePosition;
    [SerializeField] private Transform Position;
    [SerializeField] private CoinsMovement coinsMovement;
    

    private float tempTime;

    private void Start()
    {
        coinsMovement = FindObjectOfType<CoinsMovement>();
        InvokeRepeating("SpawnCoin", 2f, 2f);
    }

    private void Update()
    {
        if (GameManager.Instance.GameState())
        {
            tempTime += Time.deltaTime;
            if (tempTime > waitTime)
            {
                tempTime = 0;
                SpawnCoin(true);
            }
        }
        if(GameManager.Instance.start == false)
        {
            SpawnCoin(false);
        }
    }

    public void SpawnCoin(bool turnon)
    {
        if (turnon == false)
            return;

        int randVal = (int)Random.Range(0f, 3f);

        switch (randVal)
        {
            case 0:
                spawnPoint = new Vector2(transform.position.x, 0f);
                break;
            case 1:
                spawnPoint = new Vector2(transform.position.x, 2.5f);
                break;
            case 2:
                spawnPoint = new Vector2(transform.position.x, 2f);
                break;
            case 4:
                spawnPoint = new Vector2(transform.position.x, 1.5f);
                break;
        }
        if (Direction == -1)
        {
            var coin = Instantiate(coinPrefab, spawnPoint, Quaternion.identity);
            coin.transform.parent = OppositePosition;
        }
        if (Direction == 1)
        {
            var coin = Instantiate(coinPrefab, spawnPoint, Quaternion.identity);
            coin.transform.parent = Position;
        }
    }


}
