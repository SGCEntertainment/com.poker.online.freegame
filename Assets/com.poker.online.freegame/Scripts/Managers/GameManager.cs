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
        GetWinner();
    }

    private Player GetWinner()
    {
        Card[] cards = new Card[players.Length * 2];
        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                cards[i] = players[i].cards[j];
            }
        }

        Card HighCard = Combination.GetHighCard(cards);
        Debug.Log($"{HighCard.CardValue}({HighCard.CardSuit})");
        return null;
    }
}
