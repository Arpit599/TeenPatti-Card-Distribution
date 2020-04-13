using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CardDistribution : MonoBehaviourPun
{
    private Dictionary<string, List<int>> deckofCards;
    int n_hearts = 13;
    int n_clubs = 13;
    int n_spades = 13;
    int n_diamonds = 13;

    [SerializeField]
    private Text cardsGot_Text;

    List<int> firstPlayer2v2 = new List<int>();
    List<int> secondPlayer2v2 = new List<int>();
    private PhotonView PV;
    string cardsToDisp = "";
    int[] cardsGot_array = new int[3];

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
        List<int>[] Players = new List<int>[numberOfPlayers];  //Making an array of list of type int in order to make my code scalable for n number of players and each element will have different set of 3 cards assigned to them

        for (int i = 0; i < numberOfPlayers; i++)
        {
            Players[i] = RandomDistributor();
        }

        object[] MyobjArray = new object[numberOfPlayers];
        for (int j = 0; j < numberOfPlayers; j++)
        {
            MyobjArray[j] = Players[j].ToArray();
        }

        if (PV.IsMine)
            PV.RPC("RPC_function", RpcTarget.All, MyobjArray);
    }

    [PunRPC]
    public void RPC_function(params object[] obj)
    {
        cardsGot_array = (int[])obj[PhotonNetwork.LocalPlayer.ActorNumber - 1];
        foreach (int i in cardsGot_array)
            cardsToDisp = cardsToDisp + " " + i.ToString();
        cardsGot_Text.text = cardsToDisp;
    }

    #region TeenPattiDistributor
    private List<int> RandomDistributor()
    {
        string RandKey;           // To generate random suits
        List<int> groupOfThreeCards = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            List<string> keysList = new List<string>(deckofCards.Keys); //Collecting the availabel keys(suits) from the deckofCards dictionary
            RandKey = keysList[rand.Next(4)];
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
            temp.RemoveAt(randomCardNumber);
            deckofCards[RandKey] = temp;  //Copying into the original list 
        }       
        return groupOfThreeCards;
    }
    #endregion
}
