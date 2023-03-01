using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool IsBot;

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
            if(!IsBot)
            {
                return;
            }

            SpriteRenderer icon = transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>();
            TextMeshPro nameText = transform.GetChild(2).GetChild(1).GetComponent<TextMeshPro>();

            icon.sprite = value.icon;
            nameText.text = value.name;

            profile = value;
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
        if(!IsBot)
        {
            profile = new Profile
            {
                name = "You"
            };
        }

        Combination = string.Empty;
    }
}
