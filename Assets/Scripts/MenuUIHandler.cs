using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR //k�rs endast om det �r i Unity editorn som vi �r, denna raden kod filtreras annars bort som ingenting (kallas conditional instructions)
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField NamePicker; //drag och droppa Input-f�lt till denna i Inspectorn
    public string currentName; //En variabel f�r att h�lla namnet i textf�ltet

    void Start()
    {
        MainManager.Instance.LoadName();
        NamePicker.text = MainManager.Instance.playerName;
    }
    public void StartNew() //N�r vi trycker p� start-knappen h�nder detta:
    {
        GetName();//Mthoden tar namnet som finns i InputField (Namepicker)
        MainManager.Instance.playerName = currentName; //Fyller MainManagers var playerName med inneh�llet fr�n currentName
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
        EditorApplication.ExitPlaymode(); //St�nger spelet om det k�rs i Unity editor
#else
        Application.Quit(); //St�nger spelet om det k�rs i en build
#endif
    }

    public void GetName() //H�mtar namnet fr�n Gameobjektet som l�nkats till var NamePicker
    {
            currentName = NamePicker.text; //Fyller var currentName med inneh�llet fr�n textrutan
    }
}
