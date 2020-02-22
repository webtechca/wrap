using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    private int coins = 0;
    private int keys = 0;
    public Text coinsText;
    public Text keysText;
    public Text centerText;

    public static GameUiController instance;

    public GameObject pauseUi;

    // Start is called before the first frame update
    void Start()
    {
        // Allow pause and play mechanic
        Time.timeScale = 1;

        // restart level when player comeback
        coins = 0;
        keys = 0;

        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        string textPrefix = "";

        if (coins < 10)
        {
            textPrefix = "0";
        }
        coinsText.text = textPrefix + coins.ToString();

        textPrefix = "";
        if (keys < 10)
        {
            textPrefix = "0";
        }
        keysText.text = textPrefix + keys.ToString();
    }

    public void incrementCoins()
    {
        coins++;
    }
    public void incrementKeys()
    {
        keys++;
    }
    // public void setCoins(int coinsValue)
    // {
    //     coins = coinsValue;
    // }
    // public int GetCoins()
    // {
    //     return coins;
    // }

    public void setCenterText(string text)
    {
        centerText.text = text;
    }

    public void setKeys(int keysValue)
    {
        keys = keysValue;
    }
    public int GetKeys()
    {
        return keys;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseUi.SetActive(true);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        pauseUi.SetActive(false);
    }

    public IEnumerator FlashMessage(string msg, int seconds = 3)
    {
        centerText.text = msg;
        yield return new WaitForSeconds(seconds);
        centerText.text = "";
    }
}
