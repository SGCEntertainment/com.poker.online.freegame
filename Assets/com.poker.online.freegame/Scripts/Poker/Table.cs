using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private int index = 0;
    public List<Card> cards = new List<Card>();
    public List<Card> Cards
    {
        get => cards;

        set
        {
            for (int i = 0; i < value.Count; i++)
            {
                StartCoroutine(InstantiateCard(value[i], i));
            }

            cards.AddRange(value);
        }
    }

    private IEnumerator InstantiateCard(Card card, int i)
    {
        yield return new WaitForSeconds(i + 0.01f);

        Instantiate(card, transform.GetChild(1).GetChild(index));
        index++;
    }

    public void Clear()
    {
        index = 0;
        cards.Clear();
    }
}
