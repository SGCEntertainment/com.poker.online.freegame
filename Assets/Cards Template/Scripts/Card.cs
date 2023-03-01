using UnityEngine;

public class Card : MonoBehaviour
{
    private const float speed = 15.0f;

    public CardValue CardValue;
    public CardSuit CardSuit;

    private void Start()
    {
        transform.position = GameObject.Find("deck").transform.position;
        transform.up = transform.parent.up;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.parent.position, speed * Time.deltaTime);
    }
}
