using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Library : MonoBehaviour {


    public ListCharacters lc;

    public Text nameC;
    public Text descriptionC;
    public Text statsC;
    public Text skillC;
    public Text ultimateC;

    public int index = 0;

    //private void Awake()
    //{

    //}
    // Use this for initialization
    void Start () {
        lc = GameManager.instance.LoadFileToString();
        nameC.text = "Name: " + lc.characterStats[index].name;
        descriptionC.text = "Description: " + lc.characterStats[index].description;
        statsC.text = "Life: " + lc.characterStats[index].healthMax + "\nShield: " + lc.characterStats[index].shieldMax + "\nBasic damage: " +
            lc.characterStats[index].damageBasicAttack + "\nFire Rate: "+ lc.characterStats[index].fireRate + "\nSkill Damage: " +
            lc.characterStats[index].damageSkill + "\nSkill Cooldown: " + lc.characterStats[index].skillCD + "\nUltimate Damage: " +
            lc.characterStats[index].damageUltimate + "\nUltimate Cooldown: " + lc.characterStats[index].ultimateCD;

        //skillC.text = lc.characterStats[index].
        //TODO: Add skills and ultimate name to file

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
