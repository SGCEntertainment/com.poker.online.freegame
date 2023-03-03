using System.Collections;
using UnityEngine;

public class EmojiController : MonoBehaviour
{
    private GameObject[] emojies;

    private void Awake()
    {
        emojies = Resources.LoadAll<GameObject>("emojies");
    }

    private void Start()
    {
        StartCoroutine(nameof(SpawnEmoji));
    }

    private IEnumerator SpawnEmoji()
    {
        while(true)
        {
            float delay = Random.Range(12.5f, 35.0f);
            yield return new WaitForSeconds(delay);

            GameObject emojiPrefab = emojies[Random.Range(0, emojies.Length)];
            Instantiate(emojiPrefab, transform);
        }
    }
}
