using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
    public int streetId;

    public static bool IsReady { get; set; }
    private static float smoothTime = 0.1f;
    private Vector2 velocity = Vector2.zero;

    private int potCount;
    public TextMeshPro potText;

    [Space(10)]
    public Table table;
    public Player[] players;

    private void Start()
    {
        ResetPot();
        transform.position += Vector3.down * 15.0f;
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, Vector2.zero, ref velocity, smoothTime);
        IsReady = Vector2.Distance(transform.position, Vector2.zero) < 0.1f;
    }

    public void AddToPot(int amount)
    {
        potCount += amount;
        potText.text = $"{potCount:N}";
    }
    
    public void ResetPot()
    {
        potCount = 0;
        potText.text = $"{potCount:N}";
    }
}
