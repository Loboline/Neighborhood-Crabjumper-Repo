using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Behöver finnas med för att JSON ska funka
using TMPro;


public class MainManager : MonoBehaviour
{
    //Koden nedan gör att jag kan accessa MainManager-skriptet från alla andra skript.
    public static MainManager Instance; //static class members kan bli accessade från vilket skript som helst utan att referera till ett speciellt object. Alla instanser av i detta fallet MainManager delar samma värden som är sparade i en av dem.
    private JumpDisplay jumpDisplay; //koppling tll annat script
    private TextMeshProUGUI nameNow;

    public string playerName; // variabel som i detta fallet kommer sparas genom scenes eftersom värdet sparas i MainManager
    private string highNameCandidate; // variabel som testas mot tidigare highscores
    private int highScoreCandidate; // variabel som testas mot tidigare highscores

    public TextMeshProUGUI nameText1; //koppling till rutan
    public TextMeshProUGUI nameText2; //koppling till rutan
    public TextMeshProUGUI nameText3; //koppling till rutan
    public TextMeshProUGUI scoreText1; //koppling till rutan
    public TextMeshProUGUI scoreText2; //koppling till rutan
    public TextMeshProUGUI scoreText3; //koppling till rutan
    public int highScore1; // variabel som i detta fallet kommer sparas genom scenes eftersom värdet sparas i MainManager
    public int highScore2; // variabel som i detta fallet kommer sparas genom scenes eftersom värdet sparas i MainManager
    public int highScore3; // variabel som i detta fallet kommer sparas genom scenes eftersom värdet sparas i MainManager
    public string highName1; // variabel som i detta fallet kommer sparas genom scenes eftersom värdet sparas i MainManager
    public string highName2; // variabel som i detta fallet kommer sparas genom scenes eftersom värdet sparas i MainManager
    public string highName3; // variabel som i detta fallet kommer sparas genom scenes eftersom värdet sparas i MainManager


    private void Update()
    {

    }

    private void Awake() //körs när skriptet blir laddat (så fort objektet blir skapat)
    {
        UpdateHighscores();
        if (Instance != null) //Kollar om instancen inte är fylld (vilken den första gången blir nedanför denna kod) denna del av skriptet behövs så det inte skapas mängder av MainManagers varje gång vi byter scen.
        {
            Destroy(gameObject); //Om det redan finns en instans av MainManager, förstör det extra gameobjektet.
            return; //fortsätt med skriptet
        }
        Instance = this; //sparar ”this” i class member Instance (dvs den nuvarande instansen av MainManager). Vi kan nu kalla MainManager.Instance varifrån som helst.
        DontDestroyOnLoad(gameObject); //förstör inte detta gameObject när en ny scen laddas.

        LoadName(); //laddar in information från save-fil (om det finns en)
    }

    [System.Serializable] //JSON-mekanik, behövs för att JsonUtility ska funka. Funkar inte på primitive types, arrays, lists eller dictionaries. Taggar nedan som något som ska serielizeas 
    class SaveData
    {
        public string playerName; //det som ska läggas in i en JSON (en slags text save-fil)
        public int highScore1; // variabel för highscores
        public int highScore2; // variabel för highscores
        public int highScore3; // variabel för highscores
        public string highName1; // variabel för highscores
        public string highName2; // variabel för highscores
        public string highName3; // variabel för highscores
    }

    public void Save() //en save-method som tar data, konverterar den till JSON-text som först blir till en string och därefter sparar den på en plats på hårddisken
    {
        SaveData data = new SaveData(); //Gör ”data” till en ny instans baserad på SaveData()
        data.playerName = playerName; //lägger in variabeln playerName värde i ”data”
        data.highScore1 = highScore1;
        data.highScore2 = highScore2;
        data.highScore3 = highScore3;   
        data.highName1 = highName1;
        data.highName2 = highName2;
        data.highName3 = highName3;

        string json = JsonUtility.ToJson(data); //transformerar ”data” till JSON-text med hjälp av JsonUtility.ToJson och gör en string av det.

        File.WriteAllText(Application.persistentDataPath + "/ savefile.json", json);
    } //Vi använder en speciell method File.WriteAllText för att skriva en string till en fil. Den första parametern i parentesen är en väg till filen genom att använda ytterligare en Unity-method som heter Application.persistentDataPath (den ger oss en folder som överlever en reinstall eller update) och ger den filen namnet savefile.json. Den andra parametern är texten som vi vill skriva till den filen (dvs stringen json)


    public void LoadName() //en load-method som hämtar informationen från en fil på en speciell plats på hårddisken (om den finns) och fyller SaveData data med informationen.
    {
        Debug.Log("Loading name");
        string path = Application.persistentDataPath + "/ savefile.json"; //gör en ny string-variabel och ger den vägen till savefilen
        if (File.Exists(path)) //Om det finns en savefil på platsen som var path pekar på...
        {
            string json = File.ReadAllText(path); //Läser in texten från filen på platsen och gör den till en string som fyller json-variabeln
            SaveData data = JsonUtility.FromJson<SaveData>(json); //Konverterar stringen från JSON-text till SaveData instance...
            playerName = data.playerName; //Slutligen sätter den playerName till datan som hämtades
        }
    }

    public void LoadHighscore() //en load-method som hämtar informationen från en fil på en speciell plats på hårddisken (om den finns) och fyller SaveData data med informationen.
    {
        string path = Application.persistentDataPath + "/ savefile.json"; //gör en ny string-variabel och ger den vägen till savefilen
        if (File.Exists(path)) //Om det finns en savefil på platsen som var path pekar på...
        {
            string json = File.ReadAllText(path); //Läser in texten från filen på platsen och gör den till en string som fyller json-variabeln
            SaveData data = JsonUtility.FromJson<SaveData>(json); //Konverterar stringen från JSON-text till SaveData instance...
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
        //Om värdet är en highscore (högre än nuvarande 3:e-platsen) spara numret i en variabel
        //Om det är högre kör SortHighscore()
    }

    public void UpdateHighscores()
    {
        Debug.Log("Updating score");

        LoadHighscore();
        nameText1.text = highName1;
        nameText2.text = highName2;
        nameText3.text = highName3;
        scoreText1.text = highScore1.ToString(); //Gör om int till text som kan läggas in i highscorelistan
        scoreText2.text = highScore2.ToString();
        scoreText3.text = highScore3.ToString();
    }
}
