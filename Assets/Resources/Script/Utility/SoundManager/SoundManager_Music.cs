using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundManager : MonoBehaviour
{
    public static void init_Music()
    {
        Musics = Resources.LoadAll<AudioSource>("Music/AudioSource");
    }

    public static AudioSource[] Musics;

    public static AudioSource currentMusic;

    public static float currentMusic_originalVolume = 0;

    public static float DelayBeforeNewMusic;

    public static bool ReduceDelay;

    public static void MusicGestion()
    {
        if (!ReduceDelay && (currentMusic == null || !currentMusic.isPlaying))
        {
            DelayBeforeNewMusic = 5;

            ReduceDelay = true;
        }

        if (DelayBeforeNewMusic <= 0 && ReduceDelay)
        {
            if (V.administrator && V.script_Scene_Main_Administrator.activateMusic || !V.administrator)
            {
                PlayNewMusic();

                ReduceDelay = false;
            }
        }

        if (ReduceDelay)
            DelayBeforeNewMusic -= Time.deltaTime;
    }

    public static List<AudioClip> LastPlayedMusic = new List<AudioClip>();

    public static void PlayNewMusic()
    {
        if (currentMusic != null)
            Destroy(currentMusic.gameObject);

        DelayBeforeNewMusic = 2;

        AudioSource m = Musics[Random.Range(0, Musics.Length)];

        while (LastPlayedMusic.Contains(m.clip))
            m = Musics[Random.Range(0, Musics.Length)];

        currentMusic = PlayMusic(m);

        LastPlayedMusic.Add(m.clip);
        if (LastPlayedMusic.Count >= 5)
        {
            LastPlayedMusic.RemoveAt(0);
        }
    }

    public static AudioSource PlayMusic(AudioSource MusicToPlay, float pitchModifier = 1)
    {
        AudioSource a = Instantiate(MusicToPlay);

        a.volume = ModifyVolume(MusicToPlay.volume, true);

        a.pitch = MusicToPlay.pitch * pitchModifier;

        currentMusic_originalVolume = MusicToPlay.volume;

        a.Play();

        return a;
    }
}
