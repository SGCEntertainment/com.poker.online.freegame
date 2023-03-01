using TMPro;
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

    private Profile profile;
    public Profile Profile
    {
        get => profile;

        set
        {
            SpriteRenderer icon = transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>();
            TextMeshPro nameText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshPro>();

            icon.sprite = value.icon;
            nameText.text = value.name;
        }
    }

    public string Combination
    {
        set
        {
            GetComponentInChildren<TextMeshPro>().text = value;
        }
    }

    private void Start()
    {
        Combination = string.Empty;
    }
}
