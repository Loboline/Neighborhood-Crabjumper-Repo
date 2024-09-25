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
    public TMP_InputField NamePicker; //drag and drop element
    static string PlayerName;

    public void StartNew()
    {
        getName();
        MainManager.Instance.playerName = PlayerName;
        SceneManager.LoadScene(1);
    }

    public void Remember()
    {
        Debug.Log(MainManager.Instance.playerName);
    }

    public void Exit()
    {
        MainManager.Instance.SaveName(); //Sparar den sist valda namnet i MainManager.
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void getName()
    {
        PlayerName = NamePicker.text;
        Debug.Log(PlayerName);
    }
}
