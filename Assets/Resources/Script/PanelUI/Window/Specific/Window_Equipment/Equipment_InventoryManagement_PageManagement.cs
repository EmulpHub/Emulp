using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Equipment_InventoryManagement : MonoBehaviour
{
    public int CurrentPage = 1;

    public GameObject NextPage_Button, PreviousPage_Button;

    bool NextPageExist;

    public Text CurrentPage_txt;

    public void PageManagement()
    {
        CurrentPage_txt.text = "" + CurrentPage;

        NextPage_Button.gameObject.SetActive(NextPageExist);
        PreviousPage_Button.gameObject.SetActive(CurrentPage > 1);
    }

    public Dropdown dropdown;

    public sort_mode currentSortingMode = sort_mode.rarity_UpToLower;

    public void SortChangeValue()
    {
        int value = dropdown.value;

        //0 = Rarity_upToLower
        //1 = Rarity_LowerToUp

        CurrentPage = 1;

        switch (value)
        {
            case 0:
                currentSortingMode = sort_mode.rarity_UpToLower;
                break;
            case 1:
                currentSortingMode = sort_mode.rarity_LowertoUp;
                break;
            case 2:
                currentSortingMode = sort_mode.Equipment_helmet;
                break;
            case 3:
                currentSortingMode = sort_mode.Equipment_chest;
                break;
            case 4:
                currentSortingMode = sort_mode.Equipment_Belt;
                break;
            case 5:
                currentSortingMode = sort_mode.Equipment_Pant;
                break;
            case 6:
                currentSortingMode = sort_mode.Equipment_Boot;
                break;
            case 7:
                currentSortingMode = sort_mode.Equipment_Weapon;
                break;
            case 8:
                currentSortingMode = sort_mode.Equipment_Object;
                break;
            case 9:
                currentSortingMode = sort_mode.Equipment_Object_2;
                break;
        }

        CreateAllSlotInventory(CurrentPage, currentSortingMode);
    }

    public void NextPage()
    {
        CurrentPage++;
        CreateAllSlotInventory(CurrentPage, currentSortingMode);
    }

    public void PreviousPage()
    {
        CurrentPage--;
        CreateAllSlotInventory(CurrentPage, currentSortingMode);
    }
}
