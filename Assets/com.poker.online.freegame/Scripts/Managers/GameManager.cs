using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool enable;

    [SerializeField] Table table;
    [SerializeField] Player[] players;

    private void OnValidate()
    {
        if(!enable)
        {
            return;
        }

        enable = false;
        Debug.Log($"Winner: {GetWinner().name}");
    }

    private Player GetWinner()
    {
        var t = players.GroupBy(player => new List<Card>()
        {
            player.cards[0],
            player.cards[1],

            table.cards[0],
            table.cards[1],
            table.cards[2],
            table.cards[3],
            table.cards[4],
        });

        foreach(var i in t)
        {
            foreach (Card card in i.Key)
            {
                Debug.Log($"{i.Single(p => p).name}.{card.CardValue} {card.CardSuit}");
            }
        }




        Card[] cards = new Card[players.Length * 2];
        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                cards[i * 2 + j] = players[i].cards[j];
            }
        }

        Card HighCard = Combination.GetHighCard(cards);
        var winners = players.Where(player => player.cards.IsContains(HighCard)).ToArray();

        if(winners.Length > 1)
        {
            Card[] tmp = winners.Select(c => c.cards.GetMinCard(HighCard)).ToArray();
            HighCard = Combination.GetHighCard(tmp);
        }

        return players.Where(player => player.cards.Contains(HighCard)).First();
    }
}
