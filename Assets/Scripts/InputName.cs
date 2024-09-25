using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InputName : MonoBehaviour
{
    private string inputName;
    public TextMeshProUGUI PlayerNameText;

    // Start is called before the first frame update
    void Start()
    {
        inputName = MainManager.Instance.playerName; //Hämtar namnet från MainManager
        PlayerNameText.text = inputName;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
