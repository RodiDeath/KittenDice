using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopPanelSelector : MonoBehaviour
{
    [SerializeField]
    GameObject panelSpells;
    [SerializeField]
    GameObject panelResources;
    [SerializeField]
    GameObject panelSkins;

    [SerializeField]
    GameObject buttonSpells;
    [SerializeField]
    GameObject buttonResources;
    [SerializeField]
    GameObject ButtonSkins;

    Sprite spellsNormal;
    Sprite resourcesNormal;
    Sprite skinsNormal;

    Sprite spellsSelected;
    Sprite resourcesSelected;
    Sprite skinsSelected;

    void Start()
    {
        spellsNormal = Resources.Load<Sprite>("Sprites/ShopSelector/SpellsShopIcon");
        spellsSelected = Resources.Load<Sprite>("Sprites/ShopSelector/SpellsShopIconSelected");

        resourcesNormal = Resources.Load<Sprite>("Sprites/ShopSelector/ResourcesShopIcon");
        resourcesSelected = Resources.Load<Sprite>("Sprites/ShopSelector/ResourcesShopIconSelected");

        skinsNormal = Resources.Load<Sprite>("Sprites/ShopSelector/SkinsShopIcon");
        skinsSelected = Resources.Load<Sprite>("Sprites/ShopSelector/SkinsShopIconSelected");

        ClickedSpells();
    }

    public void ClickedSpells()
    {
        panelResources.SetActive(false);
        panelSkins.SetActive(false);
        panelSpells.SetActive(true);

        buttonSpells.GetComponent<Image>().sprite = spellsSelected;
        buttonResources.GetComponent<Image>().sprite = resourcesNormal;
        ButtonSkins.GetComponent<Image>().sprite = skinsNormal;
    }

    public void ClickedResources()
    {
        panelResources.SetActive(true);
        panelSkins.SetActive(false);
        panelSpells.SetActive(false);

        buttonSpells.GetComponent<Image>().sprite = spellsNormal;
        buttonResources.GetComponent<Image>().sprite = resourcesSelected;
        ButtonSkins.GetComponent<Image>().sprite = skinsNormal;
    }

    public void ClickedSkins()
    {
        panelResources.SetActive(false);
        panelSkins.SetActive(true);
        panelSpells.SetActive(false);

        buttonSpells.GetComponent<Image>().sprite = spellsNormal;
        buttonResources.GetComponent<Image>().sprite = resourcesNormal;
        ButtonSkins.GetComponent<Image>().sprite = skinsSelected;
    }


}
