using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundManager : MonoBehaviour
{
    private static Dictionary<list, AudioSource> sounds_played = new Dictionary<list, AudioSource>();

    public static void TryAddNewSounds(list soundToPlay, AudioSource newAd)
    {
        if (sounds_played.ContainsKey(soundToPlay))
            sounds_played[soundToPlay] = newAd;
        else
            sounds_played.Add(soundToPlay, newAd);
    }

    public static bool ContainSounds(list ad)
    {
        return sounds_played.ContainsKey(ad) && sounds_played[ad] != null;
    }

    public static AudioSource TryGetSounds(list ad, bool CreateNewOneIfNull = true)
    {
        if (sounds_played.ContainsKey(ad) && (!CreateNewOneIfNull || sounds_played[ad] != null))
        {
            return sounds_played[ad];
        }
        else
        {
            return PlaySound(ad);
        }
    }
}
