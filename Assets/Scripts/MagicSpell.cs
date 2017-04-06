using UnityEngine;
using System;
using UnityEngine.UI;

public class MagicSpell : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int cost;
    [SerializeField]
    Text textAmount;

    [SerializeField]
    private int indexOnPrefs;

    private int amount = 0;

    private int totalSpells = 4;

    private static bool init = false;

    void Start()
    {
        //PlayerPrefs.SetString("spells", "10#20#30#40");
        InitSpells();

        
    }

    public void InitSpells()
    {
        if (!PlayerPrefs.HasKey("spells"))
        {
            PlayerPrefs.SetString("spells", "0#0#0#0#");
        }
        //amount = GetSpellAmount();
        GetSpellAmount();
        textAmount.text = amount.ToString();

        
    }

    private void GetSpellAmount()
    {
        //MagicSpell[] magicSpells = GetComponentsInParent<MagicSpell>();
        MagicSpell[] magicSpells = FindObjectsOfType<MagicSpell>();
        
        if (magicSpells.Length == totalSpells)
        {
            init = true;
            Debug.Log("Spells Init");
            string rawSpellInfo = PlayerPrefs.GetString("spells"); // 5#50#20#10

            string[] spellsAmount = rawSpellInfo.Split('#');

            Debug.Log("SpellString: " + rawSpellInfo);

            
            foreach (MagicSpell spell in magicSpells)
            {
                spell.SetAmount(Int32.Parse(spellsAmount[spell.GetIndexInPrefs()]));
            }
        }

        //return Int32.Parse( spellsAmount[indexOnPrefs]);

    }
    


    public void ChargeSpell()
    {
        amount--;
        string[] spellsString = PlayerPrefs.GetString("spells").Split('#');

        spellsString[indexOnPrefs] = amount.ToString();

        string newSpellString = "";

        for(int i = 0; i < spellsString.Length; i++)
        {
            newSpellString += spellsString[i];

            if (i <= totalSpells-2) newSpellString += "#";
        }

        PlayerPrefs.SetString("spells", newSpellString);
        textAmount.text = amount.ToString();
    }




    public void SetCost(int c) { cost = c; }
    public int GetCost() { return cost; }
    public void SetName(string n) { name = n; }
    public string GetName() { return name; }

    public void SetAmount(int a) { amount = a; }
    public int GetAmount() { return amount; }

    public void SetIndexInPrefs(int i) { indexOnPrefs = i; }
    public int GetIndexInPrefs() { return indexOnPrefs; }
}
