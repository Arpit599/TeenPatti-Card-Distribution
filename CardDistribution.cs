﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardDistribution : MonoBehaviour
{
    private Dictionary<string, List<int>> deckofCards;
    public bool a = true;
    int n_hearts = 13;
    int n_clubs = 13;
    int n_spades = 13;
    int n_diamonds = 13;
//Had to declare once and for all here because in the block it initializes everytime it is called and is most likely to give the same values again and again
    System.Random rand = new System.Random();

    void Start()
    {
        //Creating deck of cards;
        deckofCards = new Dictionary<string, List<int>>()
        {
            { "Hearts", new List<int>{1,2,3,4,5,6,7,8,9,10,11,12,13 } } ,
            { "Clubs",  new List<int>{1,2,3,4,5,6,7,8,9,10,11,12,13 } } ,
            { "Spades", new List<int>{1,2,3,4,5,6,7,8,9,10,11,12,13 } } ,
            { "Diamonds", new List<int>{1,2,3,4,5,6,7,8,9,10,11,12,13 } } 
        };
    }

    void Update()
    {


        /**foreach (int d in deckofCards["Hearts"])
            Debug.Log("Hearts: " + d);
        foreach (int d in deckofCards["Clubs"])
            Debug.Log("Clubs:" + d);
        foreach (int d in deckofCards["Diamonds"])
            Debug.Log("Diamonds: " + d);
        foreach (int d in deckofCards["Spades"])
            Debug.Log("Spades: " + d);**/
        if (a)
        {
            int numberOfPlayers = 3;
            switch (numberOfPlayers)
            {
                case 1:
                    List<int> firstPlayer = new List<int>();
                    firstPlayer = RandomDistributor(firstPlayer);

                    break;
                case 2:
                    List<int> firstPlayer2 = new List<int>();
                    firstPlayer2 = RandomDistributor(firstPlayer2);
           
                    List<int> secondPlayer2 = new List<int>();
                    secondPlayer2 = RandomDistributor(secondPlayer2);
 
                    break;
                case 3:
                    List<int> firstPlayer3 = new List<int>();
                    firstPlayer3 = RandomDistributor(firstPlayer3);

                    List<int> secondPlayer3 = new List<int>();
                    secondPlayer3 = RandomDistributor(secondPlayer3);

                    List<int> thirdPlayer3 = new List<int>();
                    thirdPlayer3 = RandomDistributor(thirdPlayer3);

                    break;
                case 4:
                    List<int> firstPlayer4 = new List<int>();
                    firstPlayer4 = RandomDistributor(firstPlayer4);

                    List<int> secondPlayer4 = new List<int>();
                    secondPlayer4 = RandomDistributor(secondPlayer4);

                    List<int> thirdPlayer4 = new List<int>();
                    thirdPlayer4 = RandomDistributor(thirdPlayer4);

                    List<int> fourthPlayer4 = new List<int>();
                    fourthPlayer4 = RandomDistributor(fourthPlayer4);

                    break;
                default:
                    break;
            }
            a = false;
        }
    }

    #region TeenPattiDistributor
    private List<int> RandomDistributor(List<int>groupOfThreeCards)
    {
        string RandKey;           // To generate random suits
       // string prevRandKey = "";  // To make sure previous random selected suit is not equal to the current selected suit

        for (int i = 0; i < 3; i++)
        {
            List<string> keysList = new List<string>(deckofCards.Keys); //Collecting the availabel keys(suits) from the deckofCards dictionary

            RandKey = keysList[rand.Next(4)];

           // while (prevRandKey == RandKey)
           //     RandKey = keysList[rand.Next(4)];

            int randomCardNumber = -1;

            switch (RandKey)
            {
                case "Hearts":
                    randomCardNumber = rand.Next(n_hearts);   //To pick a card from the generated random suit
                    n_hearts -= 1;
                    break;
                case "Clubs":
                    randomCardNumber = rand.Next(n_clubs);
                    n_clubs -= 1;
                    break;
                case "Spades":
                    randomCardNumber = rand.Next(n_spades);
                    n_spades -= 1;
                    break;
                case "Diamonds":
                    randomCardNumber = rand.Next(n_diamonds);
                    n_diamonds -= 1;
                    break;
                default:
                    break;
            }

            List<int> temp = new List<int>(deckofCards[RandKey]);  //Copying the selected list into a temp list           

            groupOfThreeCards.Add(temp[randomCardNumber]);
            Debug.Log(RandKey + " " + temp[randomCardNumber]);
            temp.RemoveAt(randomCardNumber);
            deckofCards[RandKey] = temp;  //Copying into the original list 
           // prevRandKey = RandKey;           
        }       
        return groupOfThreeCards;
    }
    #endregion
}