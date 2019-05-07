using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelector : MonoBehaviour {

    public Animator animSelector;
    public EventTrigger eventTrigger;
    public SpriteRenderer sp;

    private BattleChoose battleChoose;
    private bool buttonSelected;

    public int numButton;

    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();
        battleChoose = FindObjectOfType<BattleChoose>();
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
        Debug.Log("Select");

        if (battleChoose.numSelected < 2)
        {
            // Se accede al script BattleChoose ya que este script no tiene instanciado GameManager
            battleChoose.gameManager.playerChoise[battleChoose.numSelected] = numButton;

            battleChoose.Play();
            buttonSelected = true;
        }
    }

    public void PointerEnterAnim()
    {
        Debug.Log("Point Enter");
        ResetAnim();

        if (battleChoose.numSelected < 2)
        {
            if (!buttonSelected)
            {
                if (battleChoose.numSelected == 0)
                {
                    SelectedBlue();
                }
                else if (battleChoose.numSelected == 1)
                {
                    SelectedRed();
                }
            }
            else
            {
                SelectedBlueRed();
            }
        }
    }

    public void PointerExitAnim()
    {
        Debug.Log("Point Exit");

        if (battleChoose.numSelected < 2)
        {
            if (!buttonSelected)
            {
                SelectedNone();
            }
            else
            {
                SelectedBlue();
            }       
        }
    }
}
