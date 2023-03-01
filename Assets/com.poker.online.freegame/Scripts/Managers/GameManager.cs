using System.Collections.Generic;
using System.Collections;

using System.Linq;
using UnityEngine;

using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get => FindObjectOfType<GameManager>();
    }

    [SerializeField] Deck deck;
    [SerializeField] UserDatabase userDatabase;

    private Table Table { get; set; }
    private Player[] Players { get; set; }
    private List<Card> CardsForGame { get; set; }

    private int potCount;
    [SerializeField] TextMeshPro potText;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        ////////////////////
        /////////////////////
        ///////////////////
        Table = FindObjectOfType<Table>();
        /////////////////
        //////////////////
        //////////////
        Players = FindObjectsOfType<Player>();
        /////////////////
        //////////находит игроков в разных последовтеьномтях, не находит меня первым

        List<Profile> profiles = userDatabase.Profiles;

        for (int i = 0; i < Players.Length; i++)
        {
            Profile profile = profiles[Random.Range(0, profiles.Count)];
            Players[i].Profile = profile;

            profiles.Remove(profile);
        }

        CardsForGame = deck.Cards;
        StartCoroutine(nameof(GameCycle));
    }

    private IEnumerator GameCycle()
    {
        while(true)
        {
            foreach(Player p in Players)
            {
                yield return p.WaitPlayerTurn();
            }

            yield return null;
        }
    }

    public void AddToPot(int amount)
    {
        potCount += amount;
        potText.text = $"{potCount:N}";
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    StartCoroutine(nameof(DealCards));
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    StartCoroutine(DealTableCards(5));
        //}

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Debug.Log($"Winner: {GetWinner().Profile.name}");
        //}

        //if(Input.GetKeyDown(KeyCode.O))
        //{
        //    foreach(Card c in FindObjectsOfType<Card>())
        //    {
        //        Destroy(c.gameObject);
        //    }

        //    cardsForGame = deck.Cards;
        //    StartCoroutine(nameof(DealCards));
        //    StartCoroutine(DealTableCards(5));
        //}
    }

    private Card GetCardFromDeck()
    {
        Card card = CardsForGame.GetRandomCard();
        CardsForGame.Remove(card);
        return card;
    }

    private Player GetWinner()
    {
        var tableAndPlayerCards = Players.GroupBy(player => new List<Card>()
        {
            player.Cards[0],
            player.Cards[1],

            Table.Cards[0],
            Table.Cards[1],
            Table.Cards[2],
            Table.Cards[3],
            Table.Cards[4],
        });

        Dictionary<Player, (int, string)> result = new Dictionary<Player, (int, string)>();
        tableAndPlayerCards.ToList().ForEach((data) =>
        {
            result.Add(data.Single(), Combination.GetCombination(data.Key.ToArray()));
        });

        foreach(var res in result)
        {
            res.Key.Combination = res.Value.Item2;
            //Debug.Log($"<color=red>{res.Key.Name}</color> has a combination <color=red>{res.Value.Item2}</color>");
        }

        var winRating = result.Max(res => res.Value.Item1);
        var winCombination = result.Select(res => res.Value).Where(v => v.Item1 == winRating).First();
        //Debug.Log($"Winning combination among players: <color=red>{winCombination.Item2}</color>");

        var winners = result.Where(res => res.Value == winCombination).Select(p => p.Key);

        if (winners.Count() > 1)
        {
            List<Card> winnerCards = new List<Card>();
            winners.ToList().ForEach((data) =>
            {
                winnerCards.AddRange(data.Cards);
            });

            var t = winnerCards.GroupBy(wc => wc.CardValue).ToDictionary(y => y.Key, y => y.Count());
            foreach(var i in t)
            {
                //Debug.Log($"{i.Key} {i.Value}");
            }

            Card HighCard = Combination.GetHighCard(winnerCards.ToArray());
            //Card[] tmp = winners.Select(c => c.Cards.GetMinCard(HighCard)).ToArray();
            //HighCard = Combination.GetHighCard(tmp);

            Player winner = Players.Where(player => player.Cards.Contains(HighCard)).First();
            //Debug.Log($"{winner.Name} <color=red>high card </color>{HighCard.CardValue} {HighCard.GetCardStringSuit()}");
            return winner;
        }

        return winners.Single();
    }

    private IEnumerator DealCards()
    {
        foreach (Player player in Players)
        {
            player.Cards = new Card[]
            {
                GetCardFromDeck(), GetCardFromDeck()
            };

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator DealTableCards(int count)
    {
        Card[] cards = new Card[count];
        for(int i = 0; i < cards.Length; i++)
        {
            cards[i] = GetCardFromDeck();
        }

        Table.Cards = cards.ToList();
        yield return null;
    }
}
