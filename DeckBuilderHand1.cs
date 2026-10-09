using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilderHand1 : MonoBehaviour
{
    [SerializeField] private GameObject deckExt;
    [SerializeField] private List<GameObject> deck;

    [Header("Deck Settings")]
    public int n;

    [Header("Buttons")]
    public Button drawButton;
    public Button shuffleButton;
    public Button discardButton;
    public Button viewHandButton;

    private List<GameObject> cards;
    private List<GameObject> hand = new List<GameObject>();
    private List<GameObject> equipment = new List<GameObject>();

    private const int MAX_EQUIPMENT = 4;

    private void Awake()
    {
        InitializeDeck();
        Shuffle();

        drawButton.onClick.AddListener(DrawCardButton);
        shuffleButton.onClick.AddListener(ShuffleButton);
        discardButton.onClick.AddListener(DiscardCardButton);
        viewHandButton.onClick.AddListener(ViewHandButton);
    }

    public void InitializeDeck()
    {
        cards = new List<GameObject>();

        for (int i = 0; i < n; i++)
        {
            cards.Add(deck[i]);
        }
    }

    public GameObject DrawCard()
    {
        if (cards.Count == 0)
        {
            Debug.LogWarning("Cannot draw: no cards left.");

            return null;
        }

        GameObject card = cards[0];
        cards.RemoveAt(0);
        hand.Add(card);

        Debug.Log("Drew card: " + card.name);

        return card;
    }

    public void Shuffle()
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            GameObject temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }

        Debug.Log("Deck shuffled.");
    }

    public bool DiscardCard(GameObject card)
    {
        if (!hand.Contains(card))
        {
            Debug.LogWarning("Cannot discard card " + card.name + ": card is not in the hand.");

            return false;
        }

        hand.Remove(card);

        Debug.Log("Discarded card: " + card.name);

        return true;
    }

    public List<GameObject> ViewHand()
    {
        return new List<GameObject>(hand);
    }

    public int CardsRemaining()
    {
        return cards.Count;
    }

    public void DrawCardButton()
    {
        GameObject card = DrawCard();
    }

    public void ShuffleButton()
    {
        Shuffle();
    }

    public void DiscardCardButton()
    {
        if (hand.Count == 0)
        {
            Debug.Log("Your hand is empty!");
            return;
        }

        GameObject card = hand[0];

        if (DiscardCard(card))
        {
            Debug.Log("Discarded card: " + card.name);
        }
    }

    public void ViewHandButton()
    {
        if (hand.Count == 0)
        {
            Debug.Log("Hand is empty.");
            return;
        }

        string handText = "Hand: ";

        foreach (GameObject card in hand)
        {
            handText += card.name + " ";
        }

        Debug.Log(handText);
    }

    public void EquipCard(GameObject card)
    {
        if (!hand.Contains(card))
        {
            throw new System.InvalidOperationException(
                "Cannot equip " + card.name + ": card is not in the hand."
            );
        }

        if (equipment.Count >= MAX_EQUIPMENT)
        {
            throw new System.InvalidOperationException(
                "Cannot equip " + card.name + ": equipment slots are full."
            );
        }

        hand.Remove(card);
        equipment.Add(card);

        Debug.Log("Equipped card: " + card.name);
    }
} 
