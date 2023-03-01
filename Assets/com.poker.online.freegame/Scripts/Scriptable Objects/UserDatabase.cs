using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu()]
public class UserDatabase : ScriptableObject
{
    [SerializeField] Profile[] userProfiles;

    public List<Profile> Profiles
    {
        get
        {
            Profile[] copy = userProfiles;
            for (int i = 0; i < copy.Length; i++)
            {
                Profile tmp = copy[i];
                int rv = Random.Range(i, copy.Length);

                copy[i] = copy[rv];
                copy[rv] = tmp;
            }

            return copy.ToList();
        }
    }
}
