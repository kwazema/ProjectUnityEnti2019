using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleChoose : MonoBehaviour
{
    public Text textPlayer;
    public Text textContinue;
    public Animator AnimContinue;


    public GameManager gameManager;
    public int players;
    private FadeImage fade;
    public RuntimeAnimatorController[] animController;
    public int numSelected = 0;

    public ButtonSelector[] button;
    public GameObject prefabButton;
    //public string[] nameController;

    private void Awake()
    {

        gameManager = FindObjectOfType<GameManager>();
        fade = FindObjectOfType<FadeImage>();
        button = InstantiateButtons(12, 6, new Vector2(-6.5f, 0.8f), 1.5f, 1.85f, 1);
    }

    void Start () {
        gameManager.playerChoise = new int[players];
        textPlayer.text = "Choose for Player " + (numSelected+1);
    }

    bool loadScene = false;
    private void Update()
    {
        if (numSelected >= 2 && !loadScene)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Start0") || Input.GetButton("Start1"))
            {
                AnimContinue.SetTrigger("fadeOut");
                AudioManager.instance.Play("Start");

                Invoke("FadeToImage", 1);
                Invoke("LoadSceneBattleScene", 3);

                loadScene = true;
            }
        }
    }

    private void LateUpdate()
    {
        
    }

    public void Play()
    {
        if (numSelected == 0)
        {
            textPlayer.text = "Choose for Player " + (numSelected + 2);
        }
        else if (numSelected == 1)
        {
            textPlayer.text = "Are you Ready?";
            textContinue.text = "Press Space/Start to Continue";
            AnimContinue.SetTrigger("fadeIn");

            // Desliza de abajo a arriba "Ready"
            // en pequeño "Press Select/Space for Start"
        }

        numSelected++;
    }

    void FadeToImage()
    {
        fade.FadeToBlack();
    }

    void LoadSceneBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    ButtonSelector[] InstantiateButtons(int numCharacters, int buttonInRow, Vector2 offset, float width, float height, float margin)
    {
        ButtonSelector[] button = new ButtonSelector[numCharacters];
        int h = 0;
        int w = 0;

        for (int i = 0; i < numCharacters ; i++) // Horizontal
        {
            //numCharacters / buttonInRow
            button[i] = Instantiate(prefabButton).GetComponent<ButtonSelector>();
            button[i].transform.SetParent(transform);
            button[i].numButton = i;
            button[i].transform.position = offset + new Vector2(w * (width + margin), h * -(height + margin));


            w++;
            if (w == buttonInRow)
            {
                h++;
                w = 0;
                // Se podria calcular si por algun casual no fuera pa, la fila que le falten botones que esten en medio
            }

            if (i >= gameManager.objectPlayer.Length)
                button[i].SetInterectable(false);
            else
                button[i].animLogo.runtimeAnimatorController = animController[i];
        }

        for (int i = 0; i < gameManager.logoPlayer.Length; i++)
        {
            button[i].image.sprite = gameManager.logoPlayer[i];
        }

        return button;
    }

    public void ResetChoised()
    {
        for (int i = 0; i < button.Length; i++)
        {
            button[i].ResetButton();
        }
        numSelected = 0;
    }
}
