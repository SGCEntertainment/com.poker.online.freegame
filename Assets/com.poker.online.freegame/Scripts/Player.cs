using UnityEngine;

public class Player : MonoBehaviour
{
    private Card[] cards;
    public Card[] Cards
    {
        get => cards;

        set
        {

            for(int i = 0; i < value.Length; i++)
            {
                Instantiate(value[i].gameObject, transform.GetChild(0).GetChild(i));
            }

            cards = value;
        }
    }
}
