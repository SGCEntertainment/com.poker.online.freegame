using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool enable;
    [SerializeField] Deck deck;

    [SerializeField] Table table;

    [Space(10)]
    [SerializeField] Player[] players;

    [Space(10)]
    [SerializeField] List<Card> cardsForGame;

    private void OnValidate()
    {
        if(!enable)
        {
            return;
        }

        enable = false;
        cardsForGame = deck.Cards;
        //Debug.Log($"Winner: {GetWinner().name}");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(nameof(DealCards));
        }
    }

    private Card GetCardFromDeck()
    {
        Card card = cardsForGame.GetRandomCard();
        cardsForGame.Remove(card);
        return card;
    }

    private Player GetWinner()
    {
        var tableAndPlayerCards = players.GroupBy(player => new List<Card>()
        {
            player.Cards[0],
            player.Cards[1],

            table.cards[0],
            table.cards[1],
            table.cards[2],
            table.cards[3],
            table.cards[4],
        });

        Dictionary<Player, (int, string)> result = new Dictionary<Player, (int, string)>();
        tableAndPlayerCards.ToList().ForEach((data) =>
        {
            result.Add(data.Single(), Combination.GetCombination(data.Key.ToArray()));
        });

        var winRating = result.Max(res => res.Value.Item1);
        var winCombination = result.Select(res => res.Value).Where(v => v.Item1 == winRating).First();
        Debug.Log(winCombination);
        var winners = result.Where(res => res.Value == winCombination).Select(p => p.Key);

        List<Card> winnerCards = new List<Card>();
        winners.ToList().ForEach((data) =>
        {
            winnerCards.AddRange(data.Cards);
        });

        Card HighCard = Combination.GetHighCard(winnerCards.ToArray());
        Card[] tmp = winners.Select(c => c.Cards.GetMinCard(HighCard)).ToArray();
        HighCard = Combination.GetHighCard(tmp);

        return players.Where(player => player.Cards.Contains(HighCard)).First();
    }

    private IEnumerator DealCards()
    {
        foreach (Player player in players)
        {
            player.Cards = new Card[]
            {
                GetCardFromDeck(), GetCardFromDeck()
            };

            yield return new WaitForSeconds(0.15f);
        }
    }
}
