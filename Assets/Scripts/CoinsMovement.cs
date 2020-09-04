using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinsMovement : MonoBehaviour
{
    public float Speed;
    [SerializeField] private CoinsCounter coinsCounter;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        coinsCounter = FindObjectOfType<CoinsCounter>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Awake()
    {
        Speed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState())
        {
            if (gameManager.direction == -1)
            {
                transform.position = new Vector2(transform.position.x + Time.deltaTime * Speed, transform.position.y);
            }
            if (gameManager.direction == 1)
            {
                transform.position = new Vector2(transform.position.x - Time.deltaTime * Speed, transform.position.y);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        transform.DOKill(true);
        if (collider.GetComponent<PlayerController>() == null)
            return;

        coinsCounter.Money += 1;
        transform.DOLocalMoveY(5, 2, false);
        Destroy(gameObject, 0.1f);

         
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
