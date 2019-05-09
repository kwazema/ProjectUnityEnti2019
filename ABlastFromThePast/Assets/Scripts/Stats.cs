using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //public MinotauroData minotauroData = new MinotauroData();
    public MinotauroData minotauroData;

    private void Start()
    {
        // Convert objecto to json (string)
            // string json = JsonUtility.ToJson(minotauroData);
            // Debug.Log(json);

        // Save json into disc
            //File.WriteAllText(Application.dataPath + "/saveFile.json", json);

        //Load json out disc
            string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
            MinotauroData loadedMinotauroData = JsonUtility.FromJson<MinotauroData>(json);
            Debug.Log("Health: " + loadedMinotauroData.healthMax);
            Debug.Log("Damage: " + loadedMinotauroData.damage);

        // Load info out json
            //MinotauroData loadedMinotauroData = JsonUtility.FromJson<MinotauroData>(json);
            //Debug.Log("Health: " + loadedMinotauroData.healthMax);
            //Debug.Log("Damage: " + loadedMinotauroData.damage);

    }

    [System.Serializable]
    public class MinotauroData
    {
        public int healthMax;
        public int damage;
    }
}



public class SantaData
{


}

public class CiclopeData
{

}