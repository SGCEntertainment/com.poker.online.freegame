using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool IsMyStep;
    [SerializeField] bool IsBot;

    public Card[] cards;
    public Card[] Cards
    {
        get => cards;

        set
        {
            Card[] _cards = new Card[2];
            for(int i = 0; i < value.Length; i++)
            {
                _cards[i] = Instantiate(value[i], transform.GetChild(0).GetChild(i));
            }

            cards = _cards;
            HideCards(IsBot);
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

    private GameObject Handler
    {
        get => transform.GetChild(2).GetChild(3).gameObject;
    }

    private void Start()
    {
        if(!IsBot)
        {
            IsMyStep = false;
            profile = new Profile
            {
                name = "You"
            };
        }

        Combination = string.Empty;
    }

    public void HideCards(bool IsHide)
    {
        foreach (Card card in Cards)
        {
            card.Hide = IsHide;
        }
    }

    public IEnumerator WaitPlayerTurn()
    {
        if(!IsBot)
        {
            yield return new WaitUntil(() => IsMyStep);
            Debug.Log($"{gameObject.name} turned");
            GameManager.Instance.AddToPot(100);
            IsMyStep = false;
            yield break;
        }

        float time = Random.Range(3, 5);
        Handler.SetActive(true);
        yield return new WaitForSeconds(time);
        Handler.SetActive(false);
        GameManager.Instance.AddToPot(100);
        Debug.Log($"{gameObject.name} turned");
    }
}
