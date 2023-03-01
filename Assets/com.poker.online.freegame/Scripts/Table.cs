using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private List<Card> cards = new List<Card>();
    public List<Card> Cards
    {
        get => cards;

        set
        {
            for (int i = 0; i < value.Count; i++)
            {
                Instantiate(value[i].gameObject, transform.GetChild(1).GetChild(i));
            }

            cards.AddRange(value);
        }
    }
}
