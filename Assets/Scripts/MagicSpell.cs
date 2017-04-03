using UnityEngine;
using System.Collections;

public class MagicSpell : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int cost;

    private int amount = 0;

    public void SetCost(int c) { cost = c; }
    public int GetCost() { return cost; }
    public void SetName(string n) { name = n; }
    public string GetName() { return name; }

    public void SetAmount(int a) { amount = a; }
    public int GetAmount() { return amount; }
}
