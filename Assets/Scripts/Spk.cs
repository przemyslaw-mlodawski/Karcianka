using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;

public class Spk : MonoBehaviour
{
    public Sprite[] cardFaces;
    public Sprite frame;
    public Sprite countdown5;
    public Sprite countdown4;
    public Sprite countdown3;
    public Sprite countdown2;
    public Sprite countdown1;   
    public GameObject cardPrefab1;
    public GameObject cardPrefab2;
    public GameObject Hand1;
    public GameObject Hand2;
    public GameObject Hand3;
    public GameObject Hand4;
    public GameObject Bottom1;
    public GameObject Bottom2;
    public GameObject Bottom3;
    public GameObject Bottom4;
    public GameObject Bottom5;
    public GameObject Top1;
    public GameObject Top2;
    public GameObject Top3;
    public GameObject Top4;
    public GameObject Top5;

    /*public Transform Bottom;
    public Transform Top;
    public Transform Hand;*/
    public List<Cardclass> deck;
    public List<Cardclass> hand;
    public List<Cardclass> discardpile;
    public List<Cardclass> field;
    /*public List<Cardclass> enemydeck;
    public List<Cardclass> enemyhand;
    public List<Cardclass> enemydiscardpile;
    public List<Cardclass> enemyfield;*/
    public List<Cardclass> graveyard;
    public List<GameObject> HandObjects;
    public List<GameObject> BottomObjects;
    public List<GameObject> TopObjects;

    public List<Cardclass> countdownCards;
    private int fieldsize = 5;
    private int handsize = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hand = new List<Cardclass>();
        discardpile = new List<Cardclass>();
        field = new List<Cardclass>();
        field = Countdown();
        graveyard = new List<Cardclass>();
        deck = GenerateDeck();
        deck = ShuffleDeckAtTheStart(deck);
        for (int i = 0; i < 4; i++)
        {
            DrawCard();
        }
        DisplaySpritesinHand();
        DisplaySpritesinBottom();

        StartCoroutine(Takt());
        /*Debug.Log("-----------------deal 3");
        DealCard(3);
        DisplayHand();
        DisplayField();
        Debug.Log("-----------------deal 0");
        DealCard(0);
        DisplayHand();
        DisplayField();
        Debug.Log("-----------------draw");
        DrawCard();
        DisplayHand();
        DisplayField();
        Debug.Log("-----------------discard 1");
        DiscardCard(1);
        DisplayHand();
        DisplayField();
        DisplayDiscardPile();
        Debug.Log("-----------------discard last from field");
        DiscardLastFromTheField();
        DisplayHand();
        DisplayField();
        DisplayDiscardPile();*/



        //DisplayDeck();
        //for (int i = 0; i < 4; i++)
        //{
        //DrawCard();
        //}
        //Debug.Log("Cards have been drawn!");
        //DisplayHand();
        //Debug.Log("--------------");
        //DisplayDeck();
        //Debug.Log("--------------");
        //DisplayField();
        //Debug.Log("--------------");
        //DealCard(1);
        //Debug.Log("A card from pos 1 has been dealt!");
        /*DisplayHand();
        Debug.Log("--------------");
        DisplayDeck();
        Debug.Log("--------------");
        DisplayField();
        Debug.Log("--------------");
        DrawCard();
        Debug.Log("A card has been drawn!");
        DisplayHand();
        Debug.Log("--------------");
        DisplayDeck();
        Debug.Log("--------------");
        DisplayField();
        Debug.Log("--------------");
        DealCard(2);
        Debug.Log("A card from pos 2 has been dealt!");
        DisplayHand();
        Debug.Log("--------------");
        DisplayDeck();
        Debug.Log("--------------");
        DisplayField();
        Debug.Log("--------------");
        DiscardLastFromTheField();
        Debug.Log("The last card from the field has been discarded!");
        DisplayHand();
        Debug.Log("--------------");
        DisplayDeck();
        Debug.Log("--------------");
        DisplayField();
        Debug.Log("--------------");
        DisplayDiscardPile();
        Debug.Log("--------------");
        DealCard(0);
        Debug.Log("A card from pos 0 has been dealt!");
        DisplayHand();
        Debug.Log("--------------");
        DisplayDeck();
        Debug.Log("--------------");
        DisplayField();
        Debug.Log("--------------");
        DisplayDiscardPile();
        Debug.Log("--------------");
        DiscardLastFromTheField();
        Debug.Log("The last card from the field has been discarded!");
        DisplayHand();
        Debug.Log("--------------");
        DisplayDeck();
        Debug.Log("--------------");
        DisplayField();
        Debug.Log("--------------");
        DisplayDiscardPile();
        Debug.Log("--------------");*/
    }

    // Update is called once per frame
    void Update()
    {

        // Upewniam się, że gracz ma zawsze z czego dobierać.
        if (deck.Count == 0 && discardpile.Count > 0)
        {
            ShuffleDiscardIntoDeck();
            Debug.Log(string.Format("Shuffle! Deck count: {0}, Hand.Count = {4},Field count: {1}, Discard pile count: {2}, Sum of cards: {3}", deck.Count, field.Count, discardpile.Count, deck.Count + field.Count + discardpile.Count + hand.Count, hand.Count));
        }

        // Klawisze 1-4 odpowiadają za zagranie odpowienio kart 1, 2, 3, 4 z ręki na pięciolinię.
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                DealCard(i);
            }
        }

        // Klawisze Q, W, E, R odpowiadają za odrzucenie odpowiednio kart 1, 2, 3, 4 z ręki do discardpile (kart odrzuconych / użytych).
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DiscardCard(0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            DiscardCard(1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DiscardCard(2);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DiscardCard(3);
        }

        // Awaryjne dobieranie kart z decku (ponieważ nie mam zaimplementowanych jeszcze kart dobierających karty, więc gracz mógłby bez tego 
        // pozostać bez kart w ręce.
        if (Input.GetKeyDown(KeyCode.D))
        {
            DrawCard();
        }

        // Odpowiada za manualne wywołanie kolejnego taktu (patrz: Takt())
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DiscardLastFromTheField();
            DrawCard();
        }

        // Awaryjne wywoływanie taktu bez 
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            DiscardLastFromTheField();
        }
    }

    // GenerateDeck() tworzy testowy deck kard.
    public List<Cardclass> GenerateDeck()
    {
        List<Cardclass> newDeck = new List<Cardclass>
    {
        new Cardclass("Attack", 1, cardFaces[0]),
        new Cardclass("Attack", 2, cardFaces[1]),
        new Cardclass("Attack", 3, cardFaces[2]),
        new Cardclass("Attack", 4, cardFaces[3]),
        new Cardclass("Attack", 5, cardFaces[4]),
        new Cardclass("Attack", 6, cardFaces[5]),
        new Cardclass("Attack", 7, cardFaces[6]),
        new Cardclass("Attack", 8, cardFaces[7]),
        new Cardclass("Attack", 9, cardFaces[8]),
        new Cardclass("Attack", 10, cardFaces[9]),
        new Cardclass("Attack", 11, cardFaces[10]),
        new Cardclass("Attack", 12, cardFaces[11]),
        new Cardclass("Attack", 13, cardFaces[12]),
        new Cardclass("Block", 1, cardFaces[13]),
        new Cardclass("Block", 2, cardFaces[14]),
        new Cardclass("Block", 3, cardFaces[15]),
        new Cardclass("Block", 4, cardFaces[16]),
        new Cardclass("Block", 5, cardFaces[17]),
        new Cardclass("Block", 6, cardFaces[18]),
        new Cardclass("Block", 7, cardFaces[19]),
        new Cardclass("Block", 8, cardFaces[20]),
        new Cardclass("Block", 9, cardFaces[21]),
        new Cardclass("Block", 10, cardFaces[22]),
        new Cardclass("Block", 11, cardFaces[23]),
        new Cardclass("Block", 12, cardFaces[24]),
        new Cardclass("Block", 13, cardFaces[25]),
        new Cardclass("Heal", 1, cardFaces[26]),
        new Cardclass("Heal", 2, cardFaces[27]),
        new Cardclass("Heal", 3, cardFaces[28]),
        new Cardclass("Heal", 4, cardFaces[29]),
        new Cardclass("Heal", 5, cardFaces[30]),
        new Cardclass("Heal", 6, cardFaces[31]),
        new Cardclass("Heal", 7, cardFaces[32]),
        new Cardclass("Heal", 8, cardFaces[33]),
        new Cardclass("Heal", 9, cardFaces[34]),
        new Cardclass("Heal", 10, cardFaces[35]),
        new Cardclass("Heal", 11, cardFaces[36]),
        new Cardclass("Heal", 12, cardFaces[37]),
        new Cardclass("Heal", 13, cardFaces[38]),
        new Cardclass("MoreCards", 1, cardFaces[39]),
        new Cardclass("MoreCards", 2, cardFaces[40]),
        new Cardclass("MoreCards", 3, cardFaces[41]),
        new Cardclass("MoreCards", 4, cardFaces[42]),
        new Cardclass("MoreCards", 5, cardFaces[43]),
        new Cardclass("MoreCards", 6, cardFaces[44]),
        new Cardclass("MoreCards", 7, cardFaces[45]),
        new Cardclass("MoreCards", 8, cardFaces[46]),
        new Cardclass("MoreCards", 9, cardFaces[47]),
        new Cardclass("MoreCards", 10, cardFaces[48]),
        new Cardclass("MoreCards", 11, cardFaces[49]),
        new Cardclass("MoreCards", 12, cardFaces[50]),
        new Cardclass("MoreCards", 13, cardFaces[51])
    };

        return newDeck;
    }

    // Countdown() tworzy listę kart, które odliczają czas do rozpoczęcia rozgrywki (od momentu, gdy zniknie karta z numerem 5, gracz może zacząć
    // kłaść karty na pięciolinii.
    public List<Cardclass> Countdown()
    {
        List<Cardclass> countdownCards = new List<Cardclass>
        {
            new Cardclass("Countdown", 5, countdown5),
            new Cardclass("Countdown", 4, countdown4),
            new Cardclass("Countdown", 3, countdown3),
            new Cardclass("Countdown", 2, countdown2),
            new Cardclass("Countdown", 1, countdown1)
        };
        return countdownCards;
    }

    // DrawCard() zabiera kartę z deck i przenosi ją do ręki (hand).
    public void DrawCard()
    {
        if (deck.Count == 0)
        {
            Debug.Log("Deck is empty. Cannot draw a card.");
            DisplaySpritesinHand();
            DisplaySpritesinBottom();
            return;
        }
        if (hand.Count >= handsize)
        {
            Debug.Log("Hand is full. Cannot draw a card.");
            DisplaySpritesinHand();
            DisplaySpritesinBottom();
            return;
        }
        else
        {
            Cardclass drawnCard = deck[0];
            hand.Add(drawnCard);
            deck.RemoveAt(0);

            DisplaySpritesinBottom();
            DisplaySpritesinHand();
        }
        return;
    }

    // DiscardCard() odrzuca wybraną kartę z ręki i przenosi ją do discardpile.
    public void DiscardCard(int pos)
    {
        if (pos < 0 || pos >= hand.Count)
        {
            Debug.Log("Invalid position. Cannot discard a card.");
            DisplaySpritesinHand();
            DisplaySpritesinBottom();
            return;
        }
        if (hand.Count == 0)
        {
            Debug.Log("Hand is empty. Cannot discard a card.");
            DisplaySpritesinHand();
            DisplaySpritesinBottom();
            return;
        }
        else
        {
            Cardclass discardedCard = hand[pos];
            discardpile.Add(discardedCard);
            hand.RemoveAt(pos);
            Debug.Log(string.Format("Discarded card: {0}, {1}", discardedCard.type, discardedCard.value));

            DisplaySpritesinHand();
            DisplaySpritesinBottom();  
        }
        return;
    }

    //DealCard() bierze wybraną kartę z ręki i zagrywa ją na pięciolinię gracza na tyle na prawo na niej, jak to możliwe.
    //Uznaję, że indeks 0 to piąta biała ramka na pięciolinii, indeks 4 to druga od prawej, itd.
    public void DealCard(int pos)
    {
        if (pos < 0 || pos >= hand.Count)
        {
            Debug.Log("Invalid position. Cannot deal a card.");
            DisplaySpritesinHand();
            DisplaySpritesinBottom();
            return;
        }
        if (hand.Count == 0)
        {
            Debug.Log("Hand is empty. Cannot deal a card.");
            DisplaySpritesinHand();
            DisplaySpritesinBottom();
            return;
        }
        if (field.Count >= fieldsize)
        {
            Debug.Log("Field is full. Cannot deal a card.");
            DisplaySpritesinHand();
            DisplaySpritesinBottom();
            return;
        }
        else
        {
            Cardclass dealtCard = hand[pos];
            field.Add(dealtCard);
            hand.RemoveAt(pos);

            DisplaySpritesinHand();
            DisplaySpritesinBottom();

            return;
        }
    }

    // DisplayDeck() wyświetla w konsoli wszystkie karty w talii (deck).
    public void DisplayDeck()
    {
        foreach (var card in deck)
        {
            Debug.Log(string.Format("In deck: {0}, {1}", card.type, card.value));
        }
        return;
    }

    // DisplayHand() wyświetla w konsoli wszystkie karty w ręce (hand).
    public void DisplayHand()
    {
        foreach (var card in hand)
        {
            Debug.Log(string.Format("In hand:{0}, {1}", card.type, card.value));
        }
        return;
    }

    // DisplayField() wyświetla w konsoli wszystkie karty na pięciolinii (field).
    public void DisplayField()
    {
        foreach (var card in field)
        {
            Debug.Log(string.Format("In field: {0}, {1}", card.type, card.value));
        }
        return;
    }

    // DisplayDiscardPile() wyświetla w konsoli wszystkie karty w discardpile'u.
    public void DisplayDiscardPile()
    {
        foreach (var card in discardpile)
        {
            Debug.Log(string.Format("In discard pile: {0}, {1}", card.type, card.value));
        }
        return;
    }

    // DiscardLastFromTheField() usuwa ostatnią kartę (tę, ktora zostaje zagrana na pięciolinii) z pola i przerzuca ją do discardpile.
    // Zakładamy, że indeksy na pięcioliniii (field) od lewej do prawej to 4, 3, 2, 1, 0 i ostatnia karta to ta o indeksie 0.
    // Jeśli usuwana jest karta typu Countdown, czyli odliczająca czas do rozpoczęcia rozgrywki, zamiast do discardpile'u, zostaje ona wyrzucona na cmentarz.
    public void DiscardLastFromTheField()
    {
        if (field.Count == 0)
        {
            Debug.Log("Field is empty. Cannot discard a card.");
            return;
        }
        if (field[0].type == "Countdown")
        {
            Cardclass discardedCard = field[0];
            graveyard.Add(discardedCard);
            field.RemoveAt(0);

            DisplaySpritesinBottom();
            DisplaySpritesinHand();
        }
        else
        {
            Cardclass discardedCard = field[0];
            discardpile.Add(discardedCard);
            field.RemoveAt(0);
     
            DisplaySpritesinBottom();
            DisplaySpritesinHand();
        }
        return;
    }

    // ShuffleDiscardIntoDeck() przerzuca wszystkie karty z discardpile'u do talii i tasuje je. Nie można tego zrobić, dopóki deck nie jest pusty.
    public void ShuffleDiscardIntoDeck()
    {
        if (deck.Count != 0)
        {
            Debug.Log("Can't shuffle until the deck is empty!");
            return;
        }
        else
        {
            List<Cardclass> temp = new List<Cardclass>(discardpile);
            discardpile.Clear();
            while (temp.Count > 0)
            {
                int k = UnityEngine.Random.Range(0, temp.Count);
                deck.Add(temp[k]);
                temp.RemoveAt(k);
            }
        }
        return;
    }

    // SmallDeck() tworzy mniejszy testowy deck kard.
    /*public List<Cardclass> SmallDeck()
    {
        List<Cardclass> newDeck = new List<Cardclass>
    {
        new Cardclass("A", 1, cardFaces[0]),
        new Cardclass("A", 2, cardFaces[1]),
        new Cardclass("A", 3, cardFaces[2]),
        new Cardclass("B", 1, cardFaces[3]),
        new Cardclass("B", 2, cardFaces[4]),
        new Cardclass("B", 3, cardFaces[5]),
        new Cardclass("C", 1, cardFaces[6]),
        new Cardclass("C", 2, cardFaces[7]),
        new Cardclass("C", 3, cardFaces[8]),
    };
        return newDeck;
    }*/

    // ShuffleDeckAtTheStart() tasuje deck na początku gry.
    public List<Cardclass> ShuffleDeckAtTheStart(List<Cardclass> deck)
    {
        if (deck.Count == 0)
        {
            Debug.Log("Deck is empty. Cannot shuffle.");
            return deck;
        }
        List<Cardclass> temp = new List<Cardclass>(deck);
        deck.Clear();
        while (temp.Count > 0)
        {
            int k = UnityEngine.Random.Range(0, temp.Count);
            deck.Add(temp[k]);
            temp.RemoveAt(k);
        }
        return deck;
    }

    //DisplaySpritesinHand() wyświetla karty z dłoni w czterech białych ramkach na dole sceny.
    //Jeśli ręka nie jest zapełniona, to w pozostałych pozycjach wyświetlany jest sprite białej ramki (frame).
    void DisplaySpritesinHand()
    {
        List<GameObject> handObjects = new List<GameObject> { Hand1, Hand2, Hand3, Hand4 };
        for (int i = 0; i < 4; i++)
        {
            SpriteRenderer sr = handObjects[i].GetComponent<SpriteRenderer>();

            if (i < hand.Count)
            {
                sr.sprite = hand[i].sprite;
                sr.enabled = true;
            }
            else
            {
                sr.sprite = frame;
                sr.enabled = true;
            }
        }
    }



    // DisplaySpritesinBottom() wyświetla karty z pięciolinii po stronie gracz (dolne pięć obramowań).
    // Jeśli na pięciolinii są wolne miejsce, to wyświetlane są w tych pustych miejscach białe ramki.
    void DisplaySpritesinBottom()
    {
        List<GameObject> bottomObjects = new List<GameObject> { Bottom1, Bottom2, Bottom3, Bottom4, Bottom5 };
        for (int i = 0; i < 5; i++)
        {
            SpriteRenderer sr = bottomObjects[4-i].GetComponent<SpriteRenderer>();
            if (i < field.Count)
            {
                sr.sprite = field[i].sprite;
                sr.enabled = true;
            }
            else
            {
                sr.sprite = frame;
                sr.enabled = true;
            }
        }



    }

    // Takt(), jak nazwa wskazuje, odpowiada za takty. W momencie "tiku" ostatnia karta jest wywoływana i porównywana z kartą wroga 
    // (na tę chwilę system wywoływania i porównywania z wrogiem jest do stworzenia), karty na pięciolinii się przesuwają w prawo
    // oraz jedna karta zostaje dobrana do ręki gracza.
    IEnumerator Takt()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);

            DiscardLastFromTheField();
            DrawCard();
        }
    }

    [System.Serializable]
    public class Cardclass
    {
        public string type;
        public int value;
        public Sprite sprite;

        public Cardclass(string _type, int _value, Sprite _sprite)
        {
            type = _type;
            value = _value;
            sprite = _sprite;
        }
    }
}
