using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{

    //List for storing game objects
    public List<GameObject> myInv = new List<GameObject>();

    



    void Start()
    {
        //Method to shuffle the deck
        void shuffleObjectsinDeck(List<GameObject> shuffleDeck)
        {
            //Looping through the deck
            for(int i = shuffleDeck.Count - 1; i > 0; i--)
            {
                //Choosing a random card from said deck
                int randomCard = Random.Range(0, i + 1);

                GameObject temp = shuffleDeck[i];

                shuffleDeck[i] = shuffleDeck[randomCard];

                shuffleDeck[randomCard] = temp;

            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
