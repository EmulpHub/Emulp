using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundManager : MonoBehaviour
{
    public static void StopSound(list ad)
    {
        if (ContainSounds(ad))
        {
            AudioSource newAd = TryGetSounds(ad);

            if (newAd.isPlaying)
            {
                newAd.Stop();
                return;
            }
        }

        if (ContainSounds(ad))
        {
            sounds_played[ad].Stop();
        }
    }

    public static void StopAllSound()
    {
        foreach (list a in sounds.Keys)
        {
            StopSound(a);
        }
    }
}
