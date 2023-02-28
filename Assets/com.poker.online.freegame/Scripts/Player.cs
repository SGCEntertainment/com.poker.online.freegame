using UnityEngine;

public class Player : MonoBehaviour
{
    private Card[] cards;
    public Card[] Cards
    {
        get => cards;

        set
        {
            foreach(Card card in value)
            {
                Instantiate(card.gameObject, transform);
            }

            cards = value;
        }
    }
}
