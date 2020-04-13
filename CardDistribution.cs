using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;

public class CardDistribution : MonoBehaviourPun
{
    private Dictionary<string, List<int>> deckofCards;
    int n_hearts = 13;
    int n_clubs = 13;
    int n_spades = 13;
    int n_diamonds = 13;

    [SerializeField]
    private Text cardsGotText;
    //private Text cardsGotText;
    int[] currentPlayerSetOfcards = new int[3];

    List<int> firstPlayer2v2 = new List<int>();
    List<int> secondPlayer2v2 = new List<int>();
    private PhotonView PV;
    string set = "";
    int[] arr = new int[3];
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
        PV = this.GetComponent<PhotonView>();
    }

    public void OnStartButtonClick()
    {

        int numberOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;  

        switch (numberOfPlayers)
        {
            case 1:
                List<int> firstPlayer = new List<int>();
                firstPlayer = RandomDistributor(firstPlayer);

                break;
            case 2:               
                firstPlayer2v2 = RandomDistributor(firstPlayer2v2);
                secondPlayer2v2 = RandomDistributor(secondPlayer2v2);

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

        object[] MyobjArray = {firstPlayer2v2.ToArray(), secondPlayer2v2.ToArray()};
        if(PV.IsMine)
        PV.RPC("RPC_function", RpcTarget.All, MyobjArray);
    }

    [PunRPC]
    public void RPC_function(params object[] obj)
    {
        arr = (int[])obj[PhotonNetwork.LocalPlayer.ActorNumber - 1];
        foreach (int i in arr)
            set = set + " " + i.ToString();
        cardsGotText.text = set;
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
            //Debug.Log(RandKey + " " + temp[randomCardNumber]);
            temp.RemoveAt(randomCardNumber);
            deckofCards[RandKey] = temp;  //Copying into the original list 
           // prevRandKey = RandKey;           
        }       
        return groupOfThreeCards;
    }
    #endregion
}

