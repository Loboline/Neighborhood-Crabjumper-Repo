using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Beh�ver finnas med f�r att JSON ska funka

public class MainManager : MonoBehaviour
{
    //Koden nedan g�r att jag kan accessa MainManager-skriptet fr�n alla andra skript.
    public static MainManager Instance; //static class members kan bli accessade fr�n vilket skript som helst utan att referera till ett speciellt object. Alla instanser av i detta fallet MainManager delar samma v�rden som �r sparade i en av dem.

    public string playerName; // variabel som i detta fallet kommer sparas genom scenes eftersom v�rdet sparas i MainManager


    private void Awake() //k�rs n�r skriptet blir laddat (s� fort objektet blir skapat)
    {
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
    }

    public void SaveName() //en save-method som tar data, konverterar den till JSON-text som f�rst blir till en string och d�refter sparar den p� en plats p� h�rddisken
    {
        SaveData data = new SaveData(); //G�r �data� till en ny instans baserad p� SaveData()
        data.playerName = playerName; //l�gger in variabeln playerName v�rde i �data�

        string json = JsonUtility.ToJson(data); //transformerar �data� till JSON-text med hj�lp av JsonUtility.ToJson och g�r en string av det.

        File.WriteAllText(Application.persistentDataPath + "/ savefile.json", json);
    } //Vi anv�nder en speciell method File.WriteAllText f�r att skriva en string till en fil. Den f�rsta parametern i parentesen �r en v�g till filen genom att anv�nda ytterligare en Unity-method som heter Application.persistentDataPath (den ger oss en folder som �verlever en reinstall eller update) och ger den filen namnet savefile.json. Den andra parametern �r texten som vi vill skriva till den filen (dvs stringen json)


    public void LoadName() //en load-method som h�mtar informationen fr�n en fil p� en speciell plats p� h�rddisken (om den finns) och fyller SaveData data med informationen.
    {
        string path = Application.persistentDataPath + "/ savefile.json"; //g�r en ny string-variabel och ger den v�gen till savefilen
        if (File.Exists(path)) //Om det finns en savefil p� platsen som var path pekar p�...
        {
            string json = File.ReadAllText(path); //L�ser in texten fr�n filen p� platsen och g�r den till en string som fyller json-variabeln
            SaveData data = JsonUtility.FromJson<SaveData>(json); //Konverterar stringen fr�n JSON-text till SaveData instance...
            playerName = data.playerName; //Slutligen s�tter den TeamColor till datan som h�mtades
        }
    }
}
