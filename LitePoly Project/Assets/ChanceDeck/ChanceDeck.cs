using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChanceDeck : MonoBehaviour
{
    // SINGLETON
    private static ChanceDeck _instance;

    public static ChanceDeck Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField] private List<ChanceCard> deck;

    private List<ChanceCard> cardsList = new List<ChanceCard>();

    void ResetDeck()
    {
        cardsList.Clear();
        cardsList.AddRange(deck);

        for (int i = 0; i < cardsList.Count; i++)
        {
            ChanceCard temp = cardsList[i];
            int randomIndex = Random.Range(i, cardsList.Count);
            cardsList[i] = cardsList[randomIndex];
            cardsList[randomIndex] = temp;
        }
    }

    private void Start()
    {
        ResetDeck();
    }

    public void DealNewChanceCard()
    {
        print("congrat, go one more turn");
        ChanceCard temp = cardsList[0];
        ChanceCard go = Instantiate(temp, gameObject.transform);
        cardsList.RemoveAt(0);
        cardsList.Add(temp);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DealNewChanceCard();
        }
    }
}
