using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
//-------- Main Stats ----------//

    public string name;
    public string description;
    public string history;

    public int damageBasicAttack;
    public int healthMax;
    public int shieldMax;

    public string nameSkill;
    public string descSkill;
    public int damageSkill;
    public float skillCD;

    public string nameUltimate;
    public string descUltimate;
    public int damageUltimate;
    public float ultimateCD;

    public float fireRate;
    public float recoveryShieldTime;

    public GameStats gameStats;
    //public List<GameStats> gameStats;
}

[System.Serializable]
public class GameStats
{
    //-------- Other Stats ----------//

    public int gamesWin;
    public int gamesLose;
    //Para saber el total de partidas sumamos win y lose

    public int roundsWin;
    public int roundsLose;
    //Para saber el total de rondas sumamos win y lose

    public int biggestCombo;
    public int totalDamage;
    //Si quiero saber la media simplemente divido con el total de partidas
}


[System.Serializable]
public class ListCharacters
{
    public List<CharacterStats> characterStats;
}

public class GameData : MonoBehaviour
{
    string filePath;
    string jsonString;

    public ListCharacters listCharacters;


    private void Awake()
    {
        filePath = Application.dataPath + "/GameData/FileCharactersData.json";



        #region Save to Disc
        //jsonString = JsonUtility.ToJson(listCharacters);
        //File.WriteAllText(filePath, jsonString);
        #endregion

        #region Load out Disc

        //jsonString = File.ReadAllText(filePath);

        //listCharacters = JsonUtility.FromJson<ListCharacters>(jsonString);
        //listCharacters.characterStats.Find(p => p.name == "Santa").history = "New History";
        //listCharacters.characterStats[0].name = "New History";

        //Debug.Log("Info: " + );

        #endregion

    }


}
