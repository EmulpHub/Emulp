using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : MonoBehaviour
{
    public enum talentSlot { first, second, third }

    public static Dictionary<talentSlot, Talent_Gestion.talent> equipedTalent = new Dictionary<talentSlot, Talent_Gestion.talent>();

    public static Dictionary<Talent_Gestion.talent, List<Talent_Gestion.talent>> forbideenTalent =
        new Dictionary<Talent_Gestion.talent, List<Talent_Gestion.talent>>();

    public static void InitForbideenTalent()
    {
        forbideenTalent.Clear();

        forbideenTalent.Add(Talent_Gestion.talent.SpeDefense, new List<Talent_Gestion.talent> { Talent_Gestion.talent.SpePower });
        forbideenTalent.Add(Talent_Gestion.talent.SpePower, new List<Talent_Gestion.talent> { Talent_Gestion.talent.SpeDefense });
    }

    public static void SelectEquipedTalent(Talent_Gestion.talent sp, talentSlot slot)
    {
        if (GetTalentAtSlot(talentSlot.first) == sp)
            RemoveEquipedTalent(talentSlot.first);
        if (GetTalentAtSlot(talentSlot.second) == sp)
            RemoveEquipedTalent(talentSlot.second);
        if (GetTalentAtSlot(talentSlot.third) == sp)
            RemoveEquipedTalent(talentSlot.third);

        RemoveEquipedTalent(slot);

        equipedTalent.Add(slot, sp);

        V.player_info.CalculateValue();

        //Check if forbidden talent coexist

        if (forbideenTalent.ContainsKey(sp))
        {
            List<Talent_Gestion.talent> toRemove = forbideenTalent[sp];

            foreach (talentSlot currentSlot in equipedTalent.Keys)
            {
                Talent_Gestion.talent t = equipedTalent[currentSlot];

                if (toRemove.Contains(t))
                {
                    RemoveEquipedTalent(currentSlot);
                    break;
                }
            }
        }

        UpdateAllTalentEffect();
    }

    public static void UpdateAllTalentEffect()
    {
        bool TalentEfficacity = equipedTalent.ContainsValue(Talent_Gestion.talent.efficacity);

        foreach (Talent_Gestion.talent t in equipedTalent.Values)
        {
            talentInfo inf = Talent_Gestion.Get(t);

            inf.Remove();

            if (t != Talent_Gestion.talent.efficacity && TalentEfficacity)
            {
                TalentEfficacity = false;
                inf.Apply(talent_efficacity.DoEffect(1));
            }
            else
            {

                inf.Apply(1);
            }
        }

        V.player_info.CalculateValue();
    }

    public static void RemoveEquipedTalent(talentSlot slot)
    {
        if (!equipedTalent.ContainsKey(slot))
            return;

        Talent_Gestion.Get(equipedTalent[slot]).Remove();

        equipedTalent.Remove(slot);

        V.player_info.CalculateValue();

        UpdateAllTalentEffect();
    }

    public static bool IsTalentEquiped(Talent_Gestion.talent sp)
    {
        foreach (Talent_Gestion.talent talent in equipedTalent.Values)
        {
            if (talent == sp)
            {
                return true;
            }
        }

        return false;
    }

    public static Talent_Gestion.talent GetTalentAtSlot(talentSlot slot)
    {
        if (equipedTalent.ContainsKey(slot))
            return equipedTalent[slot];
        else
        {
            return Talent_Gestion.talent.empty;
        }
    }
}
