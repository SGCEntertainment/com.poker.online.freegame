using UnityEngine;

public class Card : MonoBehaviour
{
    private const float speed = 30.0f;
    private bool IsDestinated { get; set; }

    public CardValue CardValue;
    public CardSuit CardSuit;

    public bool Hide
    {
        set => transform.GetChild(0).gameObject.SetActive(value);
    }

    private void Start()
    {
        transform.position = GameObject.Find("deck").transform.position;
        transform.up = transform.parent.up;
    }

    private void Update()
    {
        if(IsDestinated)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, transform.parent.position, speed * Time.deltaTime);
        IsDestinated = transform.position == transform.parent.position;
    }
}
