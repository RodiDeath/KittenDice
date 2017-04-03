using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static int Coins { get; set; }
    public static int Crystals { get; set; }

    [SerializeField]
    Text textCrystals;

    [SerializeField]
    Text textCoins;

    void Start()
    {
        InitCurrency();
        GetCurrencyValues();
        UpdateCurrencyText();
    }

    private void InitCurrency()
    {
        if (!PlayerPrefs.HasKey("coins"))
        {
            PlayerPrefs.SetInt("coins", 0);
            Debug.Log("Key coins created");
        }

        if (!PlayerPrefs.HasKey("crystals"))
        {
            PlayerPrefs.SetInt("crystals", 0);
            Debug.Log("Key crystals created");
        }
    }

    void OnDestroy()
    {
        StoreCurrencyValues();
    }

    public void StoreCurrencyValues()
    {
        PlayerPrefs.SetInt("coins", Coins);
        PlayerPrefs.SetInt("crystals" , Crystals);
    }

    public void GetCurrencyValues()
    {
        Coins = PlayerPrefs.GetInt("coins");
        Crystals = PlayerPrefs.GetInt("crystals");
        Debug.Log("CurrencyLoaded");
    }

    private void UpdateCurrencyText()
    {
        if (textCoins != null && textCrystals != null)
        {
            textCoins.text = Coins.ToString();
            textCrystals.text = Crystals.ToString();
        }
    }



    public bool HasEnoughCoins (int c)
    {
        if (c <= Coins) return true;
        else return false;
    }

    public bool HasEnoughCrystals(int c)
    {
        if (c <= Crystals) return true;
        else return false;
    }

    public void AddCoins(int c)
    {
        Coins += c;
        StoreCurrencyValues();

        UpdateCurrencyText();
    }

    public void QuitCoins(int c)
    {
        Coins -= c;
        StoreCurrencyValues();
        UpdateCurrencyText();
    }

    public void AddCrystals(int c)
    {
        Crystals += c;
        StoreCurrencyValues();
        UpdateCurrencyText();
    }

    public void QuitCrystals(int c)
    {
        Crystals -= c;
        StoreCurrencyValues();
        UpdateCurrencyText();
    }
}
