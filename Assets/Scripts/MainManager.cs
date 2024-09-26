using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Beh�ver finnas med f�r att JSON ska funka
using TMPro;


public class MainManager : MonoBehaviour
{
    //Koden nedan g�r att jag kan accessa MainManager-skriptet fr�n alla andra skript.
    public static MainManager Instance; //static class members kan bli accessade fr�n vilket skript som helst utan att referera till ett speciellt object. Alla instanser av i detta fallet MainManager delar samma v�rden som �r sparade i en av dem.
    private JumpDisplay jumpDisplay; //koppling tll annat script
    private TextMeshProUGUI nameNow;

    public string playerName; // variabel som i detta fallet kommer sparas genom scenes eftersom v�rdet sparas i MainManager
    private string highNameCandidate; // variabel som testas mot tidigare highscores
    private int highScoreCandidate; // variabel som testas mot tidigare highscores

    public TextMeshProUGUI nameText1; //koppling till rutan
    public TextMeshProUGUI nameText2; //koppling till rutan
    public TextMeshProUGUI nameText3; //koppling till rutan
    public TextMeshProUGUI scoreText1; //koppling till rutan
    public TextMeshProUGUI scoreText2; //koppling till rutan
    public TextMeshProUGUI scoreText3; //koppling till rutan
    public int highScore1; // variabel som i detta fallet kommer sparas genom scenes eftersom v�rdet sparas i MainManager
    public int highScore2; // variabel som i detta fallet kommer sparas genom scenes eftersom v�rdet sparas i MainManager
    public int highScore3; // variabel som i detta fallet kommer sparas genom scenes eftersom v�rdet sparas i MainManager
    public string highName1; // variabel som i detta fallet kommer sparas genom scenes eftersom v�rdet sparas i MainManager
    public string highName2; // variabel som i detta fallet kommer sparas genom scenes eftersom v�rdet sparas i MainManager
    public string highName3; // variabel som i detta fallet kommer sparas genom scenes eftersom v�rdet sparas i MainManager


    private void Update()
    {

    }

    private void Awake() //k�rs n�r skriptet blir laddat (s� fort objektet blir skapat)
    {
        UpdateHighscores();
        if (Instance != null) //Kollar om instancen inte �r fylld (vilken den f�rsta g�ngen blir nedanf�r denna kod) denna del av skriptet beh�vs s� det inte skapas m�ngder av MainManagers varje g�ng vi byter scen.
        {
            Destroy(gameObject); //Om det redan finns en instans av MainManager, f�rst�r det extra gameobjektet.
            return; //forts�tt med skriptet
        }
        Instance = this; //sparar �this� i class member Instance (dvs den nuvarande instansen av MainManager). Vi kan nu kalla MainManager.Instance varifr�n som helst.
        DontDestroyOnLoad(gameObject); //f�rst�r inte detta gameObject n�r en ny scen laddas.

        LoadName(); //laddar in information fr�n save-fil (om det finns en)
    }

    [System.Serializable] //JSON-mekanik, beh�vs f�r att JsonUtility ska funka. Funkar inte p� primitive types, arrays, lists eller dictionaries. Taggar nedan som n�got som ska serielizeas 
    class SaveData
    {
        public string playerName; //det som ska l�ggas in i en JSON (en slags text save-fil)
        public int highScore1; // variabel f�r highscores
        public int highScore2; // variabel f�r highscores
        public int highScore3; // variabel f�r highscores
        public string highName1; // variabel f�r highscores
        public string highName2; // variabel f�r highscores
        public string highName3; // variabel f�r highscores
    }

    public void Save() //en save-method som tar data, konverterar den till JSON-text som f�rst blir till en string och d�refter sparar den p� en plats p� h�rddisken
    {
        SaveData data = new SaveData(); //G�r �data� till en ny instans baserad p� SaveData()
        data.playerName = playerName; //l�gger in variabeln playerName v�rde i �data�
        data.highScore1 = highScore1;
        data.highScore2 = highScore2;
        data.highScore3 = highScore3;   
        data.highName1 = highName1;
        data.highName2 = highName2;
        data.highName3 = highName3;

        string json = JsonUtility.ToJson(data); //transformerar �data� till JSON-text med hj�lp av JsonUtility.ToJson och g�r en string av det.

        File.WriteAllText(Application.persistentDataPath + "/ savefile.json", json);
    } //Vi anv�nder en speciell method File.WriteAllText f�r att skriva en string till en fil. Den f�rsta parametern i parentesen �r en v�g till filen genom att anv�nda ytterligare en Unity-method som heter Application.persistentDataPath (den ger oss en folder som �verlever en reinstall eller update) och ger den filen namnet savefile.json. Den andra parametern �r texten som vi vill skriva till den filen (dvs stringen json)


    public void LoadName() //en load-method som h�mtar informationen fr�n en fil p� en speciell plats p� h�rddisken (om den finns) och fyller SaveData data med informationen.
    {
        Debug.Log("Loading name");
        string path = Application.persistentDataPath + "/ savefile.json"; //g�r en ny string-variabel och ger den v�gen till savefilen
        if (File.Exists(path)) //Om det finns en savefil p� platsen som var path pekar p�...
        {
            string json = File.ReadAllText(path); //L�ser in texten fr�n filen p� platsen och g�r den till en string som fyller json-variabeln
            SaveData data = JsonUtility.FromJson<SaveData>(json); //Konverterar stringen fr�n JSON-text till SaveData instance...
            playerName = data.playerName; //Slutligen s�tter den playerName till datan som h�mtades
        }
    }

    public void LoadHighscore() //en load-method som h�mtar informationen fr�n en fil p� en speciell plats p� h�rddisken (om den finns) och fyller SaveData data med informationen.
    {
        string path = Application.persistentDataPath + "/ savefile.json"; //g�r en ny string-variabel och ger den v�gen till savefilen
        if (File.Exists(path)) //Om det finns en savefil p� platsen som var path pekar p�...
        {
            string json = File.ReadAllText(path); //L�ser in texten fr�n filen p� platsen och g�r den till en string som fyller json-variabeln
            SaveData data = JsonUtility.FromJson<SaveData>(json); //Konverterar stringen fr�n JSON-text till SaveData instance...
            highScore1 = data.highScore1;
            highScore2 = data.highScore2; 
            highScore3 = data.highScore3;
            highName1 = data.highName1;
            highName2 = data.highName2;
            highName3 = data.highName3;
        }
    }


    public void CheckHighscore() 
    {
        jumpDisplay = GameObject.Find("JumpText").GetComponent<JumpDisplay>();
        nameNow = GameObject.Find("NameNowText").GetComponent<TextMeshProUGUI>();
        highScoreCandidate = jumpDisplay.jump;
        highNameCandidate = nameNow.text;
        Debug.Log(highScore1);
        Debug.Log(highName1);
        Debug.Log(highScoreCandidate);
        Debug.Log(highNameCandidate);

        if (highScoreCandidate > highScore1)
        {   highScore3 = highScore2;
            highName3 = highName2;
            highScore2 = highScore1;
            highName2 = highName1;
            highScore1 = highScoreCandidate;
            highName1 = highNameCandidate;
            Debug.Log("Success");
            Save();
        } else if(highScoreCandidate > highScore2)
        {
            highScore3 = highScore2;
            highName3 = highName2;
            highScore2 = highScoreCandidate;
            highName2 = highNameCandidate;
            Save();
        } else if (highScoreCandidate > highScore3)
        {
            highScore3 = highScoreCandidate;
            highName3 = highNameCandidate;
            Save();
        }
        Save();
        //Om v�rdet �r en highscore (h�gre �n nuvarande 3:e-platsen) spara numret i en variabel
        //Om det �r h�gre k�r SortHighscore()
    }

    public void UpdateHighscores()
    {
        Debug.Log("Updating score");

        LoadHighscore();
        nameText1.text = highName1;
        nameText2.text = highName2;
        nameText3.text = highName3;
        scoreText1.text = highScore1.ToString(); //G�r om int till text som kan l�ggas in i highscorelistan
        scoreText2.text = highScore2.ToString();
        scoreText3.text = highScore3.ToString();
    }
}
