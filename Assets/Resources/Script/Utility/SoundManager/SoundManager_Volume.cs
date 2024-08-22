using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundManager : MonoBehaviour
{
    public static float Volume = 0.5f, Volume_Music = 0.5f, Volume_principal = 1;
    public static bool MuteSound = false;

    public static float ModifyVolume(float BaseVolume, bool Music = false)
    {
        float adequateModifier = Music ? Volume_Music : Volume;

        return Mathf.Clamp(BaseVolume * adequateModifier * Volume_principal * (MuteSound ? 0 : 1), 0, 1);
    }



    public static void UpdateVolume()
    {
        foreach (list a in sounds_played.Keys)
        {
            AudioSource ad = TryGetSounds(a, false);

            if (ad == null)
                continue;

            ad.volume = ModifyVolume(sounds[a].volume);
        }

        if (currentMusic != null)
            currentMusic.volume = ModifyVolume(currentMusic_originalVolume, true);
    }
}
