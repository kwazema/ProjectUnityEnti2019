using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleChoose : MonoBehaviour
{
    public Text textPlayer;
    public GameManager gameManager;
    public int players;
    private FadeImage fade;

    public int numSelected = 0;

    public ButtonSelector[] button;
    public GameObject prefabButton;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        fade = FindObjectOfType<FadeImage>();
        button = InstantiateButtons(5, new Vector2Int(-6, 1), 2, 1, 1);
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Invoke("FadeToImage", 1);
                Invoke("LoadSceneBattleScene", 3);

                loadScene = true;
            }
        }
    }

    public void Play()
    {
        if (numSelected == 0)
        {
            textPlayer.text = "Choose for Player " + (numSelected + 2);
        }
        else if (numSelected == 1)
        {
            textPlayer.text = "¿Ready?";

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

    ButtonSelector[] InstantiateButtons(int numCharacters, Vector2Int offset, float width, float height, float margin)
    {
        ButtonSelector[] button = new ButtonSelector[numCharacters];

        for (int i = 0; i < numCharacters; i++) // Horizontal
        {
            button[i] = Instantiate(prefabButton).GetComponent<ButtonSelector>();
            button[i].transform.SetParent(transform);
            button[i].numButton = i;
            button[i].transform.position = offset + new Vector2(i * (width + margin), 0 * -(height + margin));
        }

        for (int i = 0; i < gameManager.logoPlayer.Length; i++)
        {
            button[i].sp.sprite = gameManager.logoPlayer[i];
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
