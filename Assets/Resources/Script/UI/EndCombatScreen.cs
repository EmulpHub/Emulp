using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndCombatScreen : MonoBehaviour
{
    public static bool IsScreenActive;

    public static void ShowEndCombat(gainObtened v)
    {
        IsScreenActive = true;

        GameObject g = Instantiate(Resources.Load<GameObject>("Prefab/UI_EndScreen"));

        EndCombatScreen EndScreenScript = g.GetComponent<EndCombatScreen>();

        EndScreenScript.info = v;

        PlayerMoveAutorization.Instance.Add(g);


    }

    public Text level;

    public Image xpProgressionBar, xpProgressionBar_behind;

    public float xpProgressionBar_fillAmount;

    private void Start()
    {
        WindowInfo.Instance.DeselectionnateAllWindow();

        provisoryLevel = V.player_info.level;
        provisoryXp = V.player_info.xp;

        totalXp = provisoryXp + info.xpToGain;
        provisoryXpMax = V.player_info.xp_max;

        xpProgressionBar_fillAmount = F.CalculateFillAmount(provisoryXp, provisoryXpMax);

        xpProgressionBar.fillAmount = xpProgressionBar_fillAmount;
        xpProgressionBar_behind.fillAmount = F.CalculateFillAmount(totalXp, provisoryXpMax);

        StopAllCoroutines();
        StartCoroutine(XpProgessionBar_Animation(F.CalculateFillAmount(totalXp, provisoryXpMax), 0.3f));

        delayBeforeSkipping = delayBeforeSkipping_Max;

        transform.DOScale(0.02f, 0);
        transform.DOScale(0.025f, 0.5f);

        //UpdateGainObject();

        Update_UI();
    }

    bool levelUpgraded = false;

    /// <summary>
    /// Make the xpBar receive the animation to receive xp
    /// </summary>
    /// <param name="fillAmountBeforeDamage">The fill amount before player receive xp</param>
    /// <param name="FillAmountToGo">The fill amount the xp Bar must reach</param>
    /// <param name="StartTime">The start time waited at the begining of the void</param>
    /// <returns></returns>
    public IEnumerator XpProgessionBar_Animation(float FillAmountToGo, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //Mathf.Clamp(FillAmountToGo, 0, 1 -);

        xpProgressionBar_fillAmount = FillAmountToGo;

        xpProgressionBar.DOFillAmount(Mathf.Clamp(xpProgressionBar_fillAmount, 0, 1), 0.5f);

        while (xpProgressionBar.fillAmount < FillAmountToGo && xpProgressionBar.fillAmount < 0.99f)
        {
            yield return new WaitForEndOfFrame();
        }

        if (xpProgressionBar.fillAmount >= 0.99f)
        {
            totalXp -= provisoryXpMax;

            provisoryLevel++;

            provisoryXpMax = V.player_info.CalcXpMax(provisoryLevel);

            provisoryXp = 0;

            levelUpgraded = true;

            xpProgressionBar_fillAmount = 0;

            xpProgressionBar.DOKill();
            xpProgressionBar.fillAmount = 0;

            StartCoroutine(XpProgessionBar_Animation(F.CalculateFillAmount(totalXp, provisoryXpMax), 0.1f));
        }
    }

    [HideInInspector]
    public float delayBeforeSkipping;

    public float delayBeforeSkipping_Max;

    [HideInInspector]
    public float provisoryXpMax, provisoryXp, totalXp;

    [HideInInspector]
    public int provisoryLevel;

    private void Update()
    {
        xpProgressionBar_behind.fillAmount = F.CalculateFillAmount(totalXp, provisoryXpMax);

        delayBeforeSkipping -= 1 * Time.deltaTime;

        //If we click and the delay passe then remove the endscreen
        if (Input.GetMouseButtonUp(0) && delayBeforeSkipping < 0)
        {
            print("xp ?");
            V.player_info.GainXp(info.xpToGain);

            PlayerMoveAutorization.Instance.Remove(this.gameObject);


            IsScreenActive = false;

            ClickAutorization.Remove(this.gameObject);

            Main_UI.Display_EraseAllType();

            Destroy(this.gameObject);
        }

        Update_UI();
    }

    public Color32 colorLevel_normal, coloLevel_NewLevel;

    public void Update_UI()
    {
        string n = V.IsFr() ? "Niv. " : "Lvl. ";

        level.text = n + provisoryLevel;

        if (levelUpgraded)
        {
            level.color = coloLevel_NewLevel;
        }
        else
        {
            level.color = colorLevel_normal;
        }
    }

    public class gainObtened
    {
        public int xpToGain;

        public bool victory;

        public List<monsterIndividualDropInfo> ls = new List<monsterIndividualDropInfo>();

        public gainObtened(int xp, bool victory, List<monsterIndividualDropInfo> ls)
        {
            this.ls = ls;

            xpToGain = xp;
            this.victory = victory;
        }
    }

    public class monsterIndividualDropInfo
    {
        public int lvl;

        public SingleEquipment.rarity rar;

        public monsterIndividualDropInfo(int lvl)
        {
            this.lvl = lvl;

            List<SingleEquipment.rarity> ls = new List<SingleEquipment.rarity> { SingleEquipment.rarity.Common };

            if (lvl >= 3)
                ls.Add(SingleEquipment.rarity.Uncommon);

            if (lvl >= 5)
                ls.Add(SingleEquipment.rarity.Rare);

            //rar = Equipment_Card.ChooseRarity(ls);
        }
    }

    public gainObtened info;

    /*
    public void UpdateGainObject()
    {
        
        foreach (Transform child in gainObjectParent)
        {
            DestroyImmediate(child.gameObject);
        }

        float GetRandomNumberFromLvl(int lvl, int max)
        {
            return Mathf.Clamp(Random.Range(1, max) * (1 + lvl * 0.1f), 1, max);
        }

        int i = 0, nbTalent = 0;

        while (i < info.ls.Count)
        {
            monsterIndividualDropInfo dropInfo = info.ls[i];

            bool getObject = GetRandomNumberFromLvl(dropInfo.lvl, 12) > 8;

            nbTalent += GetRandomNumberFromLvl(dropInfo.lvl, 24) > 18 && dropInfo.lvl >= 2 ? 1 : 0;

            i++;

            int nbObject = 0;

            if (!getObject)
            {
                continue;
            }
            else
            {
                nbObject++;

                if (GetRandomNumberFromLvl(dropInfo.lvl, 15) > 13)
                    nbObject++;
            }

            while (nbObject > 0)
            {
                (bool findOne, SingleEquipment e) v = Equipment_Card.SearchNewEquipment(dropInfo.lvl, SearchEquip_Helper.All, dropInfo.rar);

                if (v.findOne)
                    EndScreenObject_Equipment.Add(gainObjectParent, v.e);

                nbObject--;
            }
        }

        NewTalentManagement(Mathf.Clamp(nbTalent, 0, 1));
    }

    public void NewTalentManagement(int nbTalent)
    {
        while (nbTalent > 0)
        {
            (bool find, Talent_Gestion.talent t) v = card_Talent.SearchTalent(true);

            if (v.find)
            {
                EndScreenObject_Talent.Add(gainObjectParent, v.t);
            }

            nbTalent--;
        }
    }

    public void NewSkillManagement(int nbSkill)
    {
        while (nbSkill > 0)
        {
            EndScreenObject_SkillPoint.Add(gainObjectParent);

            nbSkill--;

            break;
        }
    }*/
}
