using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance
    {
        get => FindObjectOfType<SFXManager>();
    }

    [SerializeField] AudioSource source;

    [Space(10)]
    [SerializeField] AudioClip onclick;

    [Space(10)]
    [SerializeField] AudioClip win;
    [SerializeField] AudioClip lose;

    [Space(10)]
    [SerializeField] AudioClip pot;

    /// <summary>
    /// 0 - lose
    /// 1 - win
    /// 2 - onclick
    /// 3 - pot
    /// </summary>
    /// <param name="id"></param>
    public void PlayEffect(int id)
    {
        source.Stop();

        source.PlayOneShot(id switch
        {
            0 => lose,
            1 => win,

            2=> onclick,
            3 => pot,

            _ => onclick
        });
    }
}
