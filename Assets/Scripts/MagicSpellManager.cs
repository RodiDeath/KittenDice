using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MagicSpell))]  

public class MagicSpellManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private CurrencyManager currencyManager;

    private MagicSpell magicSpell;

    void Start()
    {
        magicSpell = GetComponent<MagicSpell>();
    }

    







    public void RandomiceDiceBehind()
    {
        if (CanBuy())
        {
            player.RandomiceDiceBehind();
            ChargeCost();
        }
    }

    public void ShowPanelChangeDiceValue()
    {
        if (CanBuy())
        {
            player.ShowPanelChangeDiceValue();
            ChargeCost();
        }
    }

    public void ClosePanelChangeDiceValue()
    {
        player.ClosePanelChangeDiceValue();
    }

    public void TransformDiceBehind(int value)
    {
        player.TransformDiceBehind(value);
    }

    public void SlowTime()
    {
        if (CanBuy())
        {
            if (levelManager.SlowTime())
                ChargeCost();
        }
    }

    public void FreezeMoves()
    {
        if (CanBuy())
        {
            if(levelManager.FreezeMoves())
                ChargeCost();
        }
    }

    private bool CanBuy()
    {
        if (magicSpell.GetAmount() > 0)
            return true;
        else
            return false;
    }

    private void ChargeCost()
    {
        magicSpell.ChargeSpell();
    }
}
