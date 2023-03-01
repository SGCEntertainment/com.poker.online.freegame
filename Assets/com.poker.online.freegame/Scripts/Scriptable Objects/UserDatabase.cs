using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu()]
public class UserDatabase : ScriptableObject
{
    [SerializeField] Profile[] userProfiles;

    public List<Profile> Cards
    {
        get
        {
            for (int i = 0; i < userProfiles.Length; i++)
            {
                Profile tmp = userProfiles[i];
                int rv = Random.Range(i, userProfiles.Length);

                userProfiles[i] = userProfiles[rv];
                userProfiles[rv] = tmp;
            }

            return userProfiles.ToList();
        }
    }
}
