using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR //körs endast om det är i Unity editorn som vi är, denna raden kod filtreras annars bort som ingenting (kallas conditional instructions)
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField NamePicker; //drag och droppa Input-fält till denna i Inspectorn
    public string currentName; //En variabel för att hålla namnet i textfältet

    void Start()
    {
        MainManager.Instance.LoadName();
        NamePicker.text = MainManager.Instance.playerName;
    }
    public void StartNew() //När vi trycker på start-knappen händer detta:
    {
        GetName();//Mthoden tar namnet som finns i InputField (Namepicker)
        MainManager.Instance.playerName = currentName; //Fyller MainManagers var playerName med innehållet från currentName
        SceneManager.LoadScene(1); //Byter scen till scen 1
    }

    public void Remember()
    {
        MainManager.Instance.UpdateHighscores();
    }

    public void Exit()
    {
        MainManager.Instance.Save(); //Sparar den sist valda namnet i MainManager.
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode(); //Stänger spelet om det körs i Unity editor
#else
        Application.Quit(); //Stänger spelet om det körs i en build
#endif
    }

    public void GetName() //Hämtar namnet från Gameobjektet som länkats till var NamePicker
    {
            currentName = NamePicker.text; //Fyller var currentName med innehållet från textrutan
    }
}
