using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundManager : MonoBehaviour
{
    public static void StartSound(list ad)
    {
        if (MuteSound)
            return;

        if (ContainSounds(ad))
        {
            AudioSource newAd = TryGetSounds(ad, false);

            if (!newAd.isPlaying)
            {
                newAd.Play();
            }
        }
        else
        {
            AudioSource newAd = Instantiate(GetSound(ad).gameObject).GetComponent<AudioSource>();

            newAd.loop = true;

            newAd.volume = ModifyVolume(newAd.volume);

            newAd.Play();

            TryAddNewSounds(ad, newAd);
        }
    }
}
