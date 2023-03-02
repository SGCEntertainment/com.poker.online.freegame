using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private int index = 0;
    private List<Card> cards = new List<Card>();
    public List<Card> Cards
    {
        get => cards;

        set
        {
            for (int i = 0; i < value.Count; i++)
            {
                Instantiate(value[i].gameObject, transform.GetChild(1).GetChild(index));
                index++;
            }

            cards.AddRange(value);
        }
    }
}
