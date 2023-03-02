using UnityEngine;

public class Room : MonoBehaviour
{
    public int streetId;

    public static bool IsReady { get; set; }
    private static float smoothTime = 0.1f;
    private Vector2 velocity = Vector2.zero;

    [Space(10)]
    public Table table;
    public Player[] players;

    private void Start()
    {
        transform.position += Vector3.down * 15.0f;
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, Vector2.zero, ref velocity, smoothTime);
        IsReady = transform.position == Vector3.zero;
    }
}
