using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Introduction_Main : MonoBehaviour
{
    public Text StoryTxt;

    public List<string> allMessage_fr = new List<string>();
    public List<string> allMessage_uk = new List<string>();

    private int curMessageIndex = 0;

    public void Start()
    {
        ResetStoryTxt();

        Invoke("UpdateStoryTxt", 1.5f);

        Invoke("playAnim_entering", 1.5f);

        SoundManager.Initialize();
        Main_UI.InitalizeCursorTexture();
    }

    public void Update()
    {

        Main_UI.RemoveInactiveCursorChange();

        if (Main_UI.dontChangeCursor.Count == 0)
        {
            Window.SetCursorAndOffset(Main_UI.cursor_normal, Window.CursorMode.click_cursor);
        }


        CheckIfChangeStoryLine();

        Scene_Main_EchapControl.EchapManagement();
    }

    public void CheckIfChangeStoryLine()
    {
        if (Input.GetKey(KeyCode.Escape) || AllSceneCanvas.activePauseMenu || !Input.anyKeyDown)
            return;

        ChangeToNextStoryLine();

    }

    public void ChangeToNextStoryLine()
    {
        curMessageIndex++;
        UpdateStoryTxt();

        playAnim_changing();
    }

    public void UpdateStoryTxt()
    {
        List<string> allMessage = new List<string>(V.IsFr() ? allMessage_fr : allMessage_uk);

        if (curMessageIndex >= allMessage.Count)
        {
            V.eventLoadingNewScene.Call();

            SceneManager.LoadScene("Main");
            return;
        }

        StoryTxt.text = allMessage[curMessageIndex];
    }

    public void ResetStoryTxt()
    {
        StoryTxt.text = "";
    }

    public Animator StoryTxtAnimator;

    public void playAnim_entering()
    {
        StoryTxtAnimator.Play("storytxt_entrance", -1);
    }

    public void playAnim_changing()
    {
        StoryTxtAnimator.Play("storytxt_changing", -1, 0f);

    }

}
