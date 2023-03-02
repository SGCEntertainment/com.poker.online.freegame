using UnityEngine;
using System.Collections;

public class MenuItem : MonoBehaviour
{
    public enum Type
    {
        [InspectorName("Вылетать слева")]
        left,

        [InspectorName("Вылетать справа")]
        right,
    }

    [SerializeField] Type type;

    private const float smoothTime = 0.08f;
    private Vector2 TargetPosition;

    private bool IsEnable { get; set; }
    private bool IsDestinated { get; set; }

    IEnumerator Delay()
    {
        int id = transform.GetSiblingIndex();
        yield return new WaitForSeconds(id * smoothTime);
        IsEnable = true;
    }

    private void Awake()
    {
        TargetPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        IsEnable = false;
        IsDestinated = false;

        float xOffset = type == Type.left ? -2000 : 2000;
        transform.localPosition += Vector3.right * xOffset;

        StartCoroutine(nameof(Delay));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Delay));
    }

    private void Update()
    {
        if(!IsEnable || IsDestinated)
        {
            return;
        }

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, TargetPosition, 6000 * Time.deltaTime);
        if((Vector2)transform.localPosition == TargetPosition)
        {
            IsDestinated = true;
        }
    }
}
