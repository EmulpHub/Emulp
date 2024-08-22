using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundManager : MonoBehaviour
{
    public static void Sound_OpenWindow()
    {
        SoundManager.PlaySound(SoundManager.list.ui_window_open);
    }

    public static void Sound_CloseWindow()
    {
        SoundManager.PlaySound(SoundManager.list.ui_window_close);
    }
}
