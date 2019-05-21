using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Statistics : MonoBehaviour
{
    //LLAMADA A ListCharacters
    public ListCharacters charactersList;

    //IMAGENES PODIO
    public Image first;
    public Image second;
    public Image third;

    //IMAGENES PERSONAJE
    public Sprite[] charactersIcons;

    //TEXTOS
    public Text ranking;
    public Text characterName;
    public Text infoRounds;
    public Text infoRoundsWon;
    public Text infoRoundsLost;
    public Text infoGames;
    public Text infoGamesWon;
    public Text infoGamesLost;

    //Array para guardar las partidas ganadas
    public int[] games;

    //GAMEOBJECTS para setear al normal
    public GameObject rankMenu;
    public GameObject infoMenu;

    // Use this for initialization
    void Start()
    {
        charactersList = GameManager.instance.LoadFileToString();
        games = new int[4];

        AssociatePosition();
    }

    public void AssociatePosition()
    {
        int assigned = 0;
        for (int i = 0; i < 4; i++)
        {
            games[i] = charactersList.characterStats[i].gameStats.gamesWin;
        }

        System.Array.Sort(games);

        ranking.text = "After glorious battles, our \"heroes\" fight to gain control over their enemies, currently this is their rank among the other contestants:\n";

        for (int j = 4; j > 0; j--)
        {
            for (int k = 0; k < 4; k++)
            {
                if (charactersList.characterStats[k].gameStats.gamesWin == games[j - 1])
                {
                    ranking.text += "\n" + charactersList.characterStats[k].name + " Wins: " + games[j - 1];

                    switch (assigned)
                    {
                        case 0:
                            Debug.Log("Entro primero");
                            first.sprite = charactersIcons[k];
                            break;
                        case 1:
                            second.sprite = charactersIcons[k];
                            break;
                        case 2:
                            third.sprite = charactersIcons[k];
                            break;
                    }
                    assigned++;
                }
            }
        }


        //charactersList.characterStats.Sort();
        //for (int i = 0; i < 4; i++)
        //{
        //    int character = charactersList.characterStats[i].gameStats.gamesWin;
        //    if (character > firstPosition)
        //    {
        //        firstPosition = character;
        //        indexCharacter = i;
        //        ranking.text = charactersList.characterStats[indexCharacter].name;
        //        first.sprite = charactersIcons[indexCharacter];
        //    }
        //}
    }

    public void ShowStatisticsCharacter(int index)
    {
        characterName.text = charactersList.characterStats[index].name;

        infoGamesWon.text = charactersList.characterStats[index].gameStats.gamesWin.ToString();
        infoGamesLost.text = charactersList.characterStats[index].gameStats.gamesLose.ToString();

        infoGames.text = (charactersList.characterStats[index].gameStats.gamesLose + charactersList.characterStats[index].gameStats.gamesWin).ToString();


        infoRoundsWon.text = charactersList.characterStats[index].gameStats.roundsWin.ToString();
        infoRoundsLost.text = charactersList.characterStats[index].gameStats.roundsLose.ToString();

        infoRounds.text = (charactersList.characterStats[index].gameStats.roundsLose + charactersList.characterStats[index].gameStats.roundsWin).ToString();
    }

    public void SetBackToNormal()
    {
        infoMenu.SetActive(false);
        rankMenu.SetActive(true);
    }
}