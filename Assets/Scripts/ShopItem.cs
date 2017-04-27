using UnityEngine;
using System.Collections;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private int cost;

    [SerializeField]
    private string name;

    [SerializeField]
    private int typeOfDivise; // 0 -> Real Money
                              // 1 -> Coins
                              // 2 -> Crystals

    [SerializeField]
    private int typeOfItem; // 0 -> Coins
                            // 1 -> Crystals
                            // 2 -> Spell 0
                            // 3 -> Spell 1
                            // 4 -> Spell 2
                            // 5 -> Spell 3
                            // 6 -> Spell 4
                            // 7 -> Spell 5
                            // (...)


    [SerializeField]
    private int amountBought; // Amount of the typeOfItem bought

    [SerializeField]
    private CurrencyManager currencyManager;

    public void ItemClicked()
    {
        switch (typeOfDivise)
        {
            case 0: // Real Money
                switch (typeOfItem)
                {
                    case 0: // Coins
                        break;
                    case 1: // Crystals
                        break;
                    case 2: // Spell 0
                        break;
                    case 3: // Spell 1
                        break;
                    case 4: // Spell 2
                        break;
                    case 5: // Spell 3
                        break;
                    case 6: // Spell 4
                        break;
                    case 7: // Spell 5
                        break;
                }
                break;


            case 1: // Coins
                switch (typeOfItem)
                {
                    case 0: // Coins
                        break;
                    case 1: // Crystals
                        break;
                    case 2: // Spell 0
                        if (currencyManager.HasEnoughCoins(cost))
                        {
                            currencyManager.QuitCoins(cost);
                            MagicSpell.AddSpell(0, amountBought);
                            Debug.Log("Cobrados " + cost + " monedas.");
                        }
                        break;
                    case 3: // Spell 1
                        if (currencyManager.HasEnoughCoins(cost))
                        {
                            currencyManager.QuitCoins(cost);
                            MagicSpell.AddSpell(1, amountBought);
                            Debug.Log("Cobrados " + cost + " monedas.");
                        }
                        break;
                    case 4: // Spell 2
                        if (currencyManager.HasEnoughCoins(cost))
                        {
                            currencyManager.QuitCoins(cost);
                            MagicSpell.AddSpell(2, amountBought);
                            Debug.Log("Cobrados " + cost + " monedas.");
                        }
                        break;
                    case 5: // Spell 3
                        if (currencyManager.HasEnoughCoins(cost))
                        {
                            currencyManager.QuitCoins(cost);
                            MagicSpell.AddSpell(3, amountBought);
                            Debug.Log("Cobrados " + cost + " monedas.");
                        }
                        break;
                    case 6: // Spell 4
                        if (currencyManager.HasEnoughCoins(cost))
                        {
                            currencyManager.QuitCoins(cost);
                            MagicSpell.AddSpell(4, amountBought);
                            Debug.Log("Cobrados " + cost + " monedas.");
                        }
                        break;
                    case 7: // Spell 5
                        if (currencyManager.HasEnoughCoins(cost))
                        {
                            currencyManager.QuitCoins(cost);
                            MagicSpell.AddSpell(5, amountBought);
                            Debug.Log("Cobrados " + cost + " monedas.");
                        }
                        break;
                }
                break;

            case 2: // Crystals
                switch (typeOfItem)
                {
                    case 0: // Coins
                        if (currencyManager.HasEnoughCrystals(cost))
                        {
                            currencyManager.QuitCrystals(cost);
                            currencyManager.AddCoins(amountBought);
                        }
                        break;
                    case 1: // Crystals
                        break;
                    case 2: // Spell 0
                        if (currencyManager.HasEnoughCrystals(cost))
                        {
                            currencyManager.QuitCrystals(cost);
                            MagicSpell.AddSpell(0, amountBought);
                            Debug.Log("Cobrados " + cost + " cristales.");
                        }
                        break;
                    case 3: // Spell 1
                        if (currencyManager.HasEnoughCrystals(cost))
                        {
                            currencyManager.QuitCrystals(cost);
                            MagicSpell.AddSpell(1, amountBought);
                            Debug.Log("Cobrados " + cost + " cristales.");
                        }
                        break;
                    case 4: // Spell 2
                        if (currencyManager.HasEnoughCrystals(cost))
                        {
                            currencyManager.QuitCrystals(cost);
                            MagicSpell.AddSpell(2, amountBought);
                            Debug.Log("Cobrados " + cost + " cristales.");
                        }
                        break;
                    case 5: // Spell 3
                        if (currencyManager.HasEnoughCrystals(cost))
                        {
                            currencyManager.QuitCrystals(cost);
                            MagicSpell.AddSpell(3, amountBought);
                            Debug.Log("Cobrados " + cost + " cristales.");
                        }
                        break;
                    case 6: // Spell 4
                        if (currencyManager.HasEnoughCrystals(cost))
                        {
                            currencyManager.QuitCrystals(cost);
                            MagicSpell.AddSpell(4, amountBought);
                            Debug.Log("Cobrados " + cost + " cristales.");
                        }
                        break;
                    case 7: // Spell 5
                        if (currencyManager.HasEnoughCrystals(cost))
                        {
                            currencyManager.QuitCrystals(cost);
                            MagicSpell.AddSpell(5, amountBought);
                            Debug.Log("Cobrados " + cost + " cristales.");
                        }
                        break;
                }
                break;

        }
    }
    
    public int GetCost() { return cost; }
    public string GetName() { return name; }
    public int GetTypeOfDivise() { return typeOfDivise; }
}
