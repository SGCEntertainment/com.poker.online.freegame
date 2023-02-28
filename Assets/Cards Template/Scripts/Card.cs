using UnityEngine;

public class Card : MonoBehaviour
{
    private Vector2 Target;
    private const float speed = 25.0f;

    public CardValue CardValue;
    public CardSuit CardSuit;

    private void Start()
    {
        Target = transform.parent.localPosition;
    }

    private void Update()
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, transform.parent.localPosition, speed * Time.deltaTime);
    }
}
