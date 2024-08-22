using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Scene_NorticePower : MonoBehaviour
{
    const int admin_nortice = 0;

    private void Awake()
    {
        TreeNortice.NextID = 0;

        V.Initializing_startScene_Awake();
        InstantiateAllAscensionInfo();
    }

    private void Start()
    {
        V.Initializing_startScene_Start();

        SetToCurrentPos();

        if (V.administrator && admin_nortice > 0) Ascension.nortice = admin_nortice;
    }

    // Update is called once per frame
    void Update()
    {
        ManageSetCursor();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ascension.HigherUnlockedAscension++;
        }

        Ascension.AscensionManagement();

        Update_UI();

        Scene_Main_EchapControl.EchapManagement();
    }

    public Transform AscensionInfo_parent;

    public void InstantiateAllAscensionInfo()
    {
        for (int i = 0; i <= Ascension.HigherAscension; i++)
        {
            AscensionInfo.Create(i, AscensionInfo_parent);
        }
    }

    public void SetToCurrentPos()
    {
        AscensionInfo_parent.GetComponent<AscensionInfo_Holder>().MakeMovement(Ascension.currentAscension, true);
    }

    public void ManageSetCursor()
    {
        Main_UI.RemoveInactiveCursorChange();

        if (Main_UI.dontChangeCursor.Count > 0)
            return;

        Window.SetCursorAndOffset(Main_UI.cursor_normal, Window.CursorMode.click_cursor);
    }
}
