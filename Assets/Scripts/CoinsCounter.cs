using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Shop shop;

    public List<Text> Prices = new List<Text>();
    public List<int> PricesNumber = new List<int>();


    public int coinsCount;
    public int Money;
    public Text counterCurrent;
    public Text bestCounter;

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Delete saves")]
    private static void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }
#endif

    private void Start()
    {
        Money = PlayerPrefs.GetInt("Save", 0);
        bestCounter.text = Money.ToString();

        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        for (int i = 0; i < Mathf.Min(gameManager.playerPrefabs.Count, PricesNumber.Count, Prices.Count); i++)
        {
            if (PlayerPrefs.GetInt($"Is purchased {i}", 0) == 1)
                Prices[i].text = "Sold";
            else
                Prices[i].text = PricesNumber[i].ToString();
        }

        bestCounter.text = Money.ToString();

        if (gameManager.start == false)
        {
            bestCounter.text = Money.ToString();
        }


    }
    private void OnDestroy()
    {
        Money += coinsCount;
        PlayerPrefs.SetInt("Save", Money);
        bestCounter.text = Money.ToString();
    }

    private int GetPrice(int index, int basePrice)
    {
        var isPurchased = PlayerPrefs.GetInt($"Is purchased {index}", 0) == 1;

        return isPurchased ? 0 : basePrice;
    }

    public void FirstBird()
    {
        var price = GetPrice(0, 5);
        shop.buy(price, 0);
    }

    public void SecondBird()
    {
        var price = GetPrice(1, 10);
        shop.buy(price, 1);
    }

    public void ThirdBird()
    {
        var price = GetPrice(2, 30);
        shop.buy(price, 2);
    }
    public void FourthBird()
    {
        var price = GetPrice(3, 40);
        shop.buy(price, 3);
    }
    public void FifthBird()
    {
        var price = GetPrice(4, 50);
        shop.buy(price, 4);
    }
    public void SixthBird()
    {
        var price = GetPrice(5, 60);
        shop.buy(price, 5);
    }
    public void SeventhBird()
    {
        var price = GetPrice(6, 70);
        shop.buy(price, 6);
    }
    public void EighthBird()
    {
        var price = GetPrice(7, 80);
        shop.buy(price, 7);
    }
    public void NinthBird()
    {
        var price = GetPrice(8, 90);
        shop.buy(price, 8);
    }
}
