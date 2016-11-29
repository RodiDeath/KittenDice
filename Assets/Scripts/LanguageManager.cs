using UnityEngine;
using System.Xml;

public class LanguageManager : MonoBehaviour
{
    [SerializeField]
    string language;
    private TextAsset textAsset;
    private XmlDocument xmlDoc;
    
    void Awake()
    {
        LoadLanguageDocument(language);
    }

    public void LoadLanguageDocument(string lang)
    {
        textAsset = (TextAsset)Resources.Load("Languages/" + lang);


        if (textAsset == null)
        {
            Debug.Log("Language doesn't exists");
            lang = "English";
            textAsset = (TextAsset)Resources.Load("Languages/" + lang);
        }

        language = lang;

        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        Debug.Log("Loaded " + lang);
    }

    public string GetString(string id)
    {
        XmlNodeList list = xmlDoc.GetElementsByTagName("string");

        foreach (XmlNode node in list)
        {
            if (node.Attributes["name"].Value.Equals(id))
            {
                return node.InnerText;
            }
        }
        return "";
    }
}
