using UnityEngine;
using System.Collections;
using System.Xml;

public class GameSetting : MonoBehaviour {


    private XmlDocument xmlDoc;
    private string xmlAddress;

    public static string language;
    public static string resolution;
    public static int backgroundVolume;
    public static int effectSound;
    public static bool isWindowMode;

    void Awake()
    {

    }
    void Start ()
    {
        xmlDoc = new XmlDocument();
        xmlAddress = Application.dataPath + "/Resources/Configuration/Setting.xml";
        xmlDoc = LoadXmlFile(xmlAddress);
        ReadXml();
    }
	

	void Update ()
    {
	
	}

    private XmlDocument LoadXmlFile(string url)
    {
        xmlDoc.Load(url);
        return xmlDoc;
    }

    private void ReadXml()
    {
        language = xmlDoc.SelectSingleNode("SeetingRoot/Language").Attributes["value"].Value;
        backgroundVolume = XmlConvert.ToInt16(xmlDoc.SelectSingleNode("SeetingRoot/BackgroundVolume").Attributes["value"].Value);
        effectSound = XmlConvert.ToInt16(xmlDoc.SelectSingleNode("SeetingRoot/EffectSound").Attributes["value"].Value);
        resolution = xmlDoc.SelectSingleNode("SeetingRoot/Resolution").Attributes["value"].Value;
        string str = xmlDoc.SelectSingleNode("SeetingRoot/WindowMode").Attributes["value"].Value;
        if (str == "Yes"){
            isWindowMode = true;
        }
        else isWindowMode = false;
        Debug.Log(isWindowMode);
      
    }
}
