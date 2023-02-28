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
        (int rating, string name) = Combination.GetCombination(cards);
        Combination.IsStraight(cards);
        //Debug.Log($"{name}({rating})");
    }
}
