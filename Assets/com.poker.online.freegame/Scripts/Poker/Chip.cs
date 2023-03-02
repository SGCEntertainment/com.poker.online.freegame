using UnityEngine;

public class Chip : MonoBehaviour
{
    public Vector2 Target { get; set; }

    private void Update()
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, Target, 15.0f * Time.deltaTime);
    }
}
