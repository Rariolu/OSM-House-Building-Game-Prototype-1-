using UnityEngine;
using UnityEditor;

public class ChooseXMLFile : EditorWindow
{
    bool aborted = false;
    public bool Aborted
    {
        get
        {
            return aborted;
        }
    }
    string enteredText;
    public string InputText
    {
        get
        {
            return enteredText;
        }
    }
    const string xmlFile = "Assets\\Contracts\\Semi-Detached House_SEMI_DETACHED_HOUSE_0.xml";
    int projectNumber = 1;


    //[MenuItem("Tools/Misc/ShowPopup Example")]
    public static bool ChooseXML(out string text)
    {
        ChooseXMLFile window = CreateInstance<ChooseXMLFile>();//new ChooseXMLFile();
        window.minSize = new Vector2(50, 20);
        window.ShowModalUtility();
        //window.ShowUtility();
        text = window.InputText;
        return !window.Aborted;
    }

    void OnGUI()
    {
        string inputText = EditorGUILayout.TextField("Choose XML File", xmlFile);
        
        if (GUILayout.Button("Use XML File"))
        {
            enteredText = inputText;
            Close();
        }
        if (GUILayout.Button("Cancel"))
        {
            aborted = true;
            Close();
        }
    }
}