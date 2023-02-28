using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool enable;
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
        Card[] cards = new Card[players.Length * 2];
        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                cards[i * 2 + j] = players[i].cards[j];
            }
        }

        Card HighCard = Combination.GetHighCard(cards);
        return players.Where(player => player.cards.Contains(HighCard)).First();
    }
}
