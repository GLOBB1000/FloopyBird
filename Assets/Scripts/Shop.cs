using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{

    public List<GameObject> Prefabs = new List<GameObject>();
    [SerializeField] private CoinsCounter coinsCounter;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        coinsCounter = FindObjectOfType<CoinsCounter>();
        gameManager = FindObjectOfType<GameManager>();
    }
    public void buy(int cost, int index)
    {
        if (cost > coinsCounter.Money)
            return;
        
        coinsCounter.Money -= cost;
        PlayerPrefs.SetInt("Index", index);
        PlayerPrefs.SetInt($"Is purchased {index}", 1);
        PlayerPrefs.SetString("Sold", "Sold");
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void loadLastScene()
    {
        var lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SceneIndex", lastSceneIndex);
    }

    public void backOffScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SceneIndex"));
    }
}
