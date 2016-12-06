using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class LanguageManager : MonoBehaviour
{
    [SerializeField]
    static string language;
    static private TextAsset textAsset;
    static private XmlDocument xmlDoc;
    
    void Awake()
    {
        DontDestroyOnLoad(this);
        //if (FindObjectsOfType(GetType()).Length > 1)
        //{
        //    Destroy(gameObject);
        //}

        language = Storage.GetLanguage();
        LoadLanguageDocument(language);
    }

    public static void LoadLanguageDocument(string lang)
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

    public static List<string> GetAllLanguages()
    {
        List<string> languagesList = new List<string>();

        Object[] allResources = Resources.LoadAll("Languages/", typeof(TextAsset));

        foreach (Object obj in allResources)
        {
            if (!obj.name.Contains(".meta"))
            {
                languagesList.Add(obj.name.Split('.')[0]);
            }
        }
        return languagesList;
    }
}
