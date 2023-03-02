using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu()]
public class Deck : ScriptableObject
{
    [SerializeField] Card[] cards;

    public List<Card> Cards
    {
        get
        {
            for(int i = 0; i < cards.Length; i++)
            {
                Card tmp = cards[i];
                int rv = Random.Range(i, cards.Length);

                cards[i] = cards[rv];
                cards[rv] = tmp;
            }

            return cards.ToList();
        }
    }
}
