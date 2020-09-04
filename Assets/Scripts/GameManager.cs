using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	[SerializeField] private Transform playerPos;
    [SerializeField] private Transform playerPosOpposite;
	[SerializeField] private Sprite[] backgroundImage;
	[SerializeField] private SpriteRenderer background;
	[SerializeField] private Animator getReadyAnim;
	[SerializeField] private Text gameScoreText, endScore, endHighScore;
	[SerializeField] private GameObject[] endButtons;
	[SerializeField] private Animator endAnimations, fadeAnim;
	[SerializeField] private Image newImage;
	[SerializeField] private Sprite[] medals;
	[SerializeField] private Image medalImage;
	[SerializeField] private GameObject medalSparkle;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject RestartReverseButton;
    [SerializeField] private GameObject MainMebuButton;
    [SerializeField] private CoinsCounter coins;
    [SerializeField] private GameObject Shop;
    [SerializeField] private PlayerController player;
    [SerializeField] private bool PermitForDelete;
    [SerializeField] Vector3 up = new Vector3(0, 0, 0);


    public GameObject getReadyIcon;
    public GameObject getReadybutton;
    public List<GameObject> playerPrefabs = new List<GameObject>();

    public GameObject flappy;
    public int SavedCoins;
	public bool ready, start, end, newBool;
	private int gameScore;
    public float direction = 1.0f;
    public Quaternion rotate = new Quaternion(0f, 180.0f, 0f, 0f);

	void Awake ()
    {
		// Create an Instance of the GameManager to be used by other scripts
		Instance = this;
	}

    void Start()
    {
        ready = true;
        coins = FindObjectOfType<CoinsCounter>();
        PlayerPrefs.GetInt("Index", 1);

        flappy = Instantiate(playerPrefabs[PlayerPrefs.GetInt("Index")], playerPos.position * direction, transform.rotation);

        // Create one amongst the 3 players
        if (direction == 1)
        {
            flappy.transform.parent = playerPos;
        }
        else if (direction == -1)
        {
            var scale = flappy.transform.localScale;
            scale.x *= -1;
            flappy.transform.localScale = scale;

            flappy.transform.parent = playerPosOpposite; 
        }
        // Use one amongst the 2 Backgrounds
        background.sprite = backgroundImage[Random.Range (0, backgroundImage.Length)];
        
    }

    private void Update()
    {

    }

    public void StartGamePlaying ()
    {
        if (ready && !start)
        {
            ready = false;
            getReadyAnim.StartPlayback();
            getReadyIcon.SetActive(true);
            getReadybutton.SetActive(true);
            //start = true;
            
        }
        Menu.gameObject.SetActive(false);
    }

    public void GetReadyStart()
    {
        start = true;

        getReadyIcon.SetActive(false);
        getReadybutton.SetActive(false);

        var comp = FindObjectOfType<IdleAnimForBird>();
        var ojj = comp.GetComponent<IdleAnimForBird>();
        ojj.enabled = !ojj.enabled;

        var flap = flappy.GetComponent<Rigidbody2D>();
        flap.gravityScale = 1;
        flap.AddForce(Vector2.up * 300f);
        if(direction == 1)
            up = new Vector3(0, 0, 45f);
        
        else if( direction == -1)
            up = new Vector3(0, 0, -45f);

        flappy.transform.DOLocalRotate(up, 5f, RotateMode.Fast);
    }

    public void GetReady ()
    {
		// Remove the Tutorial Image
		getReadyAnim.SetTrigger ("Start");

		flappy.GetComponentInChildren<Rigidbody2D> ().velocity = Vector2.zero;
		flappy.GetComponentInChildren<Rigidbody2D> ().gravityScale = 1f;
	}

	public void UpdateScore ()
    {
		gameScore++;
		gameScoreText.text = gameScore + "";
		SoundManager.Instance.PlayTheAudio("Point");
	}

	public bool GameState ()
    {
		// Return whether the game is running or has ended
		return start;
	}

	public void EndGame () {
		start = false;
        var scroll = FindObjectOfType<Scrolling>();
        scroll.isMainMenu = false;
		if(!end)
        {
			// Call this code only once, either after hitting the pipe, or after hitting the ground. As the player
			// falls and hits the ground if it hits the pipe
			end = true;

			if(gameScore > PlayerPrefs.GetInt("Score"))
            {
				// Update the Highscore
				PlayerPrefs.SetInt("Score", gameScore); 
				newBool = true;
			}
            GameOverCounter();
			endHighScore.text = PlayerPrefs.GetInt("Score") + "";
            // Start the gameover animations

            coins.Money += coins.coinsCount;
            PlayerPrefs.SetInt("Save", coins.Money);
            coins.bestCounter.text = coins.Money.ToString();

            RestartButton.SetActive(true);
            RestartReverseButton.SetActive(true);
            MainMebuButton.SetActive(true);
            Shop.SetActive(true);

            GameManager.Instance.StartCoroutine("GameOver");
			SoundManager.Instance.PlayTheAudio("Hit");

        }
	}
    void GameOverCounter()
    {
        for (int i = 0; i <= gameScore; i++)
        {
            endScore.text = i.ToString();
        }
    }

	IEnumerator GameOver ()
    {
        StopAllCoroutines();
		endAnimations.SetTrigger("End");
		yield return new WaitForSeconds (0.5f);

		SoundManager.Instance.PlayTheAudio("Swoosh");
		gameScoreText.enabled = false;
		yield return new WaitForSeconds (1f);

		SoundManager.Instance.PlayTheAudio("Swoosh");
		yield return new WaitForSeconds (0.5f);

		// Roll the current score from 0

		
		if(newBool){
			// Display New if current score exceeds the Highscore
			newImage.enabled = true;
		}
		
		// Display Medals according to the score
		if(gameScore >= 40)
        {
			medalImage.sprite = medals[3];
		}

        else if(gameScore >= 30)
        {
			medalImage.sprite = medals[2];
		}

        else if(gameScore >= 20)
        {
			medalImage.sprite = medals[1];
		}

        else if(gameScore >= 10)
        {
			medalImage.sprite = medals[0];
		}

		// Activate the sparkles  for the Medal
		if(gameScore >= 10){
			medalSparkle.SetActive(true);
		}

		// Show "Play again" and "Leaderboards" button
		foreach(GameObject endButton in endButtons)
        {
			endButton.SetActive(true);
		}
	
	}

	public void Replay(){
		fadeAnim.SetTrigger("Start");
		StartCoroutine("StartGame");
		//SoundManager.Instance.PlayTheAudio("Swoosh");
	}

	IEnumerator StartGame(){
		yield return new WaitForSeconds(0.8f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Leaderboard(){

	}

    public void RestartGame()
    {
        if (start == false)
        {
            SceneManager.LoadScene("Game");
            ready = false;
            start = true;
            RestartButton.SetActive(false);
        }
    }
    public void RestartOppositeGame()
    {
        SceneManager.LoadScene("ReverseGame");
        ready = true;
        end = false;
        RestartButton.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main");
    }

    void DeleteResults()
    {
        if (PermitForDelete == false)
            return;
        PlayerPrefs.DeleteAll();
    }
}