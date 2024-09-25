using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Behöver finnas med för att JSON ska funka

public class MainManager : MonoBehaviour
{
    //Koden nedan gör att jag kan accessa MainManager-skriptet från alla andra skript.
    public static MainManager Instance; //static class members kan bli accessade från vilket skript som helst utan att referera till ett speciellt object. Alla instanser av i detta fallet MainManager delar samma värden som är sparade i en av dem.

    public string playerName; // variabel som i detta fallet kommer sparas genom scenes eftersom värdet sparas i MainManager


    private void Awake() //körs när skriptet blir laddat (så fort objektet blir skapat)
    {
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
    }

    public void SaveName() //en save-method som tar data, konverterar den till JSON-text som först blir till en string och därefter sparar den på en plats på hårddisken
    {
        SaveData data = new SaveData(); //Gör ”data” till en ny instans baserad på SaveData()
        data.playerName = playerName; //lägger in variabeln playerName värde i ”data”

        string json = JsonUtility.ToJson(data); //transformerar ”data” till JSON-text med hjälp av JsonUtility.ToJson och gör en string av det.

        File.WriteAllText(Application.persistentDataPath + "/ savefile.json", json);
    } //Vi använder en speciell method File.WriteAllText för att skriva en string till en fil. Den första parametern i parentesen är en väg till filen genom att använda ytterligare en Unity-method som heter Application.persistentDataPath (den ger oss en folder som överlever en reinstall eller update) och ger den filen namnet savefile.json. Den andra parametern är texten som vi vill skriva till den filen (dvs stringen json)


    public void LoadName() //en load-method som hämtar informationen från en fil på en speciell plats på hårddisken (om den finns) och fyller SaveData data med informationen.
    {
        string path = Application.persistentDataPath + "/ savefile.json"; //gör en ny string-variabel och ger den vägen till savefilen
        if (File.Exists(path)) //Om det finns en savefil på platsen som var path pekar på...
        {
            string json = File.ReadAllText(path); //Läser in texten från filen på platsen och gör den till en string som fyller json-variabeln
            SaveData data = JsonUtility.FromJson<SaveData>(json); //Konverterar stringen från JSON-text till SaveData instance...
            playerName = data.playerName; //Slutligen sätter den TeamColor till datan som hämtades
        }
    }
}
