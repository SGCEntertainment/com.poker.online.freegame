using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool enable;
    [SerializeField] Card[] cards;

    private void OnValidate()
    {
        if(!enable)
        {
            return;
        }

        enable = false;
        Debug.Log($"Royal Flush: {Combination.IsRoyalFlush(cards)}");
    }
}
