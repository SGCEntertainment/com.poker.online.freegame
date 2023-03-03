using System.Collections;
using UnityEngine;

public class Emoji : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(nameof(MoveUp));
    }

    private IEnumerator MoveUp()
    {
        yield return new WaitForSeconds(1.0f);

        Vector2 target = (Vector2)transform.localPosition + Vector2.up * 2;
        while((Vector2)transform.localPosition != target)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, 2.0f * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
