﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelector : MonoBehaviour {

    public Animator animSelector;
    public Animator animLogo;
    public EventTrigger eventTrigger;
    //public SpriteRenderer sp;
    public Image image;
    private Button button;

    public GameObject logoPlayer1, logoPlayer2;

    public GameObject info, characters;

    private BattleChoose battleChoose;
    private bool buttonSelected;

    public int numButton;
    bool fadeScale;

    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();
        button = GetComponent<Button>();
        battleChoose = FindObjectOfType<BattleChoose>();

    }

    private void Update()
    {
        if (fadeScale)
            transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(380, 380), 350 * Time.deltaTime);
        else
            transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(325, 325), 350 * Time.deltaTime);
    }

    private void ResetAnim()
    {
        animSelector.ResetTrigger("SelectedBlue");
    }

    private void SelectedBlue()
    {
        animSelector.SetTrigger("SelectedBlue");
    }

    private void SelectedRed()
    {
        animSelector.SetTrigger("SelectedRed");
    }

    private void SelectedBlueRed()
    {
        animSelector.SetTrigger("SelectedBlueRed");
    }

    private void SelectedNone()
    {
        animSelector.SetTrigger("None");


    }

    //-------------------------------------------//

    public void SelectAnim()
    {
        //Debug.Log("Select");
        AudioManager.instance.Play("ButonClick");

        if (battleChoose.numSelected < 2)
        {
            // Se accede al script BattleChoose ya que este script no tiene instanciado GameManager
            battleChoose.gameManager.playerChoise[battleChoose.numSelected] = numButton;

            battleChoose.Play();
            buttonSelected = true;
        }
    }

    public void PointEnterLibrary()
    {
        fadeScale = true;
        animLogo.SetBool("Active", true);
        SelectedBlue();
        AudioManager.instance.Play("ButonTrigger");
    }

    public void PointExitLibrary()
    {
        fadeScale = false;
        animLogo.SetBool("Active", false);
        SelectedNone();
    }

    public void PointerEnterAnim()
    {
        Debug.Log("Point Enter");
        ResetAnim();
        animLogo.SetBool("Active", true);
        AudioManager.instance.Play("ButonTrigger");

        //StartCoroutine(ControllerManager.ControllerVibration(battleChoose.numSelected, 0.4f, 0.4f, 0.15f));

        fadeScale = true;
        if (battleChoose.numSelected < 2)
        {
            if (!buttonSelected)
            {
                if (battleChoose.numSelected == 0)
                {
                    logoPlayer1.SetActive(true);
                    SelectedBlue();
                    // Working: Que se cambie el color del fondo
                    // Working: Añadir una animacion que el boton se haga un poco mas grande
                }
                else if (battleChoose.numSelected == 1)
                {
                    logoPlayer2.SetActive(true);
                    SelectedRed();
                }
            }
            else
            {
                logoPlayer2.SetActive(true);
                SelectedBlueRed();
            }
        }
    }

    public void PointerExitAnim()
    {
        //Debug.Log("Point Exit");
        fadeScale = false;
        animLogo.SetBool("Active", false);

        if (battleChoose.numSelected < 2)
        {
            if (!buttonSelected)
            {
                logoPlayer1.SetActive(false);
                logoPlayer2.SetActive(false);
                SelectedNone();
            }
            else
            {
                logoPlayer2.SetActive(false);
                SelectedBlue();
            }       
        }
    }

    public void ResetButton()
    {
        //numButton = 0;
        buttonSelected = false; //hacer ResetTrigger
        animSelector.SetTrigger("None");
    }

    public void SetInterectable(bool value)
    {
        button.interactable = value;
    }

    public void ChangeToInfo()
    {
        transform.localScale = new Vector2(325, 325);
        fadeScale = false;

        info.SetActive(true);
        characters.SetActive(false);
    }
}
