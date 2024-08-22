using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundManager : MonoBehaviour
{
    public static AudioSource PlaySound(list soundToPlay, float pitchModifier = 1)
    {
        AudioSource original = GetSound(soundToPlay);

        AudioSource newAd = Instantiate(original.gameObject).GetComponent<AudioSource>();

        TryAddNewSounds(soundToPlay, newAd);

        return PlaySound(newAd, original, pitchModifier);
    }

    public static AudioSource PlayEndlessSound(list soundToPlay, float pitchModifier = 1)
    {
        AudioSource original = GetSound(soundToPlay);

        AudioSource newAd = Instantiate(original.gameObject).GetComponent<AudioSource>();

        TryAddNewSounds(soundToPlay, newAd);

        return PlayEndlessSound(newAd, original, pitchModifier);
    }

    public static void PlaySoundAndStopAnother(list play, list Stop)
    {
        StopSound(Stop);

        PlaySound(play);
    }

    public static void PlayRandomSound_BetweenTwo(list one, list two)
    {
        if (Random.Range(0, 2) == 1)
            PlaySound(one);
        else
            PlaySound(two);
    }

    public static float TimeBeforeNewPitch = 0.3f;

    public static Dictionary<AudioSource, (float time, int nb)> audioPlayedAtWhatTime = new Dictionary<AudioSource, (float time, int nb)>();

    public static float CheckAddPitch(AudioSource soundToPlay)
    {
        float nb = 0;

        if (audioPlayedAtWhatTime.ContainsKey(soundToPlay))
        {
            (float time, int nb) v = audioPlayedAtWhatTime[soundToPlay];

            if (Time.time - v.time >= TimeBeforeNewPitch)
            {
                audioPlayedAtWhatTime.Remove(soundToPlay);
            }
            else
            {
                audioPlayedAtWhatTime[soundToPlay] = (Time.time + TimeBeforeNewPitch, v.nb + 1);

                nb = v.nb * 0.005f;

            }
        }
        else
        {
            audioPlayedAtWhatTime.Add(soundToPlay, (Time.time + TimeBeforeNewPitch, 1));
        }

        return nb;
    }

    public static AudioSource PlayEndlessSound(AudioSource soundToPlay, AudioSource original, float pitchModifier = 1)
    {
        soundToPlay.volume = ModifyVolume(soundToPlay.volume);

        soundToPlay.pitch = soundToPlay.pitch * pitchModifier + CheckAddPitch(original);

        soundToPlay.Play();

        return soundToPlay;
    }

    public static AudioSource PlaySound(AudioSource soundToPlay, AudioSource original, float pitchModifier = 1)
    {
        var sound = PlayEndlessSound(soundToPlay, original, pitchModifier);

        Destroy(sound.gameObject, 4);

        return soundToPlay;
    }
}
