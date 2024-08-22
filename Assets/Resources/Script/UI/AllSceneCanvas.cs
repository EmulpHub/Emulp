using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllSceneCanvas : MonoBehaviour
{
    public GameObject pauseMenu, OptionMenu;

    public static bool activePauseMenu, activeOptionMenu;

    public static AllSceneCanvas PauseMenu;

    public void Awake()
    {
        PauseMenu = this;
    }

    public bool Autorization()
    {
        return ClickAutorization.Autorized(this.gameObject);
    }

    public void ParameterButton()
    {
        if (!Autorization())
            return;

        activePauseMenu = !activePauseMenu;

        ClickAutorization.Modify(this.gameObject, activePauseMenu);

        activeOptionMenu = false;

        PlayerMoveAutorization.Instance.AddOrRemove(this.gameObject, activePauseMenu);
    }

    bool remember = false;

    public Slider volumeSlider, principaleSlider, musicSlider;

    public void Update()
    {
        pauseMenu.gameObject.SetActive(activePauseMenu && !activeOptionMenu);

        if (pauseMenu.gameObject.activeSelf != remember)
        {
            if (pauseMenu.gameObject.activeSelf)
                SoundManager.Sound_OpenWindow();
            else
                SoundManager.Sound_CloseWindow();

            remember = pauseMenu.gameObject.activeSelf;
        }

        OptionMenu.gameObject.SetActive(activeOptionMenu);

        Update_UI();
    }

    #region UI

    public Text Volume, Principal, Music;

    public void Update_UI()
    {
        Volume.text = "Volume " + (int)(SoundManager.Volume * 100) + " %";
        Principal.text = (V.IsFr() ? "Principale " : "Main ") + (int)(SoundManager.Volume_principal * 100) + " %";
        Music.text = (V.IsFr() ? "Musique " : "Music ") + (int)(SoundManager.Volume_Music * 100) + " %";
    }

    #endregion

    #region Option

    public void Option()
    {
        if (!Autorization())
            return;

        volumeSlider.value = SoundManager.Volume;
        principaleSlider.value = SoundManager.Volume_principal;
        musicSlider.value = SoundManager.Volume_Music;

        activeOptionMenu = !activeOptionMenu;
    }

    public void Option_Volume(float volume)
    {
        if (!Autorization())
            return;

        SoundManager.Volume = volume;

        SoundManager.UpdateVolume();
    }

    public void Option_Volume_Music(float volume)
    {
        if (!Autorization())
            return;

        SoundManager.Volume_Music = volume;

        SoundManager.UpdateVolume();
    }

    public void Option_Volume_principal(float volume)
    {
        if (!Autorization())
            return;

        SoundManager.Volume_principal = volume;

        SoundManager.UpdateVolume();
    }

    public void EraseSave()
    {
        if (!Autorization())
            return;

        string txt = "Êtes-vous sur de vouloir supprimer votre sauvegarde ? (Le jeu va se fermer)";
        if (V.IsUk())
            txt = "Are you sur to erase save ? (Game will close)";

        Window_Confirmation.Launch(txt, EraseSave_Action);

    }

    private void EraseSave_Action()
    {
        Save_SaveSystem.EraseSave_WithoutWarning();

        Application.Quit();
    }

    #endregion

    public void Quit()
    {
        if (!Autorization())
            return;

        if (activeOptionMenu)
            return;

        Save_SaveSystem.SaveGame_WithoutWarning();

        Application.Quit();
    }
}
