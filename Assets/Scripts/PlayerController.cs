using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float thrust, minTiltSmooth, maxTiltSmooth, hoverDistance, hoverSpeed;
    [SerializeField] private GameManager gameManager;
	public bool startGame = false;
	private float timer, tiltSmooth, y;
	public Rigidbody2D playerRigid;
    public Vector3 vector = new Vector3(0, -1);
    private bool rotation;
    [SerializeField] Vector3 up = new Vector3(0, 0, 0);
    [SerializeField] Vector3 down = new Vector3(0, 0, 0);

    void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
		tiltSmooth = maxTiltSmooth;
		playerRigid = GetComponent<Rigidbody2D> ();
        if (gameManager.direction == 1)
        {
            up = new Vector3(0, 0, 45f);
            down = new Vector3(0, 0, -45f);
        }
        else if (gameManager.direction == -1)
        {
            up = new Vector3(0, 0, -45f);
            down = new Vector3(0, 0, 45f);
        }
    }

	void Update ()
    {
        if (gameManager.start == true)
        {
            var Y = gameManager.flappy.transform.localPosition.y;
            if (GameManager.Instance.GameState())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    transform.DOKill(true);

                    transform.DOLocalRotate(up, 5f, RotateMode.Fast);

                    playerRigid.gravityScale = 1f;
                    tiltSmooth = minTiltSmooth;

                    playerRigid.velocity = Vector2.zero;
                    // Push the player upwards
                    playerRigid.AddForce(Vector2.up * thrust);
                    SoundManager.Instance.PlayTheAudio("Flap");
                }

                if (Y < 5)
                    transform.DOLocalRotate(down, 2f, RotateMode.Fast);

            }
        }

        if (playerRigid.velocity.y < -1f)
        {
            // Increase gravity so that downward motion is faster than upward motion
            tiltSmooth = maxTiltSmooth;
            playerRigid.gravityScale = 2f;
        }
    }



    void OnTriggerEnter2D (Collider2D col)
    {
        var sign = Mathf.Sign(transform.localScale.x);
        var downRotation = Quaternion.Euler(0, 0, -90 * sign);
        var upRotation = Quaternion.Euler(0, 0, 35 * sign);
        if (col.transform.CompareTag ("Score"))
        {
			Destroy (col.gameObject);
			GameManager.Instance.UpdateScore ();
		}
        else if (col.transform.CompareTag ("Obstacle"))
        {
			// Destroy the Obstacles after they reach a certain area on the screen
			foreach (Transform child in col.transform.parent.transform)
            {
				child.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			}
			KillPlayer ();
		}
	}

	void OnCollisionEnter2D (Collision2D col)
    {
        var sign = Mathf.Sign(transform.localScale.x);
        var downRotation = Quaternion.Euler(0, 0, -90 * sign);
        var upRotation = Quaternion.Euler(0, 0, 35 * sign);
        if (col.transform.CompareTag ("Ground"))
        {
			playerRigid.simulated = false;
			KillPlayer ();
			transform.rotation = downRotation;
		}
	}

	public void KillPlayer ()
    {
		GameManager.Instance.EndGame ();
		playerRigid.velocity = Vector2.zero;
		// Stop the flapping animation
		GetComponent<Animator> ().enabled = false;
	}

}