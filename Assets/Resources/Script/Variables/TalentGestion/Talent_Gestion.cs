using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public partial class Talent_Gestion : MonoBehaviour
{
    public enum talent
    {
        empty, power, hardSkin, energy, knowledge, evolution, throwingExpert, DeGauche,
        SpePower, SpeDefense, Optimisation, Cycle, InfiniteSpike, InfiniteEffect, BleedingMaster,
        goodView, cc, armor, heal, efficacity, pa, pm, distance, adaptation, TirAuCoeur, protection,
        slower, Iccanobif, InfiniteEnergy, Evolution_mage,spikeDamage,warrior_powerReduction
    }

    public static void Init()
    {
        if (ls.Count > 0)
        {
            ls.Clear();
            lockedTalent.Clear();
        }

        if (V.IsFr())
        {
            AddTalent(talent.empty, "Vide", "Talent vide");
            AddTalent(talent.DeGauche, "Degauche", "Vos sorts les plus a gauche ont *bon+20%*end d'effet\n<i>*infvous pouvez réorganiser vos *inlsorts*end lorsque vous n'etes pas en combat*end</i>");
            //AddTalent(talent.SpePower, "Spécialisation en puissance", "*dmg+20%*end dégâts\nLorsque vous gagnez de la *wardéfense*end gagnez de la *warpuissance*end a la place.");
            //AddTalent(talent.SpeDefense, "Spécialisation en défense", "*dmg+100%*end épines\nLorsque vous gagnez de la *warpuissance*end gagnez de la *wardefense*end a la place.");
            //AddTalent(talent.Optimisation, "Optimisation", "*infFin du tour:*end\n *infpuissance majoritaire:*end *bon20%*end de votre puissance en dégâts brut autour de vous *inf(2 cases)*end\n*infdefense majoritaire:*end *bon20%*end de votre defense en soin");
            //AddTalent(talent.Cycle, "Cycle", "*infLe premier tour et tous les 2 prochains tours:*end *bon+50% d'effet*end\n*infTous les 2 tours:*end *mal-50% de dégâts*end");
            //AddTalent(talent.InfiniteSpike, "Épines infini", "Lorsque vous gagnez de la défense *bon+3 épine*end pour le reste du combat");
            //AddTalent(talent.InfiniteEffect, "Effets infini", "Lorsque vous gagnez de la puissance *bon+3% d'effet*end pour le reste du combat");
            //AddTalent(talent.BleedingMaster, "Maitrise du saignement", "Lorsque un ennemie subis des dégâts de saignement *bon+10 défense*end");
            //AddTalent(talent.goodView, "Bonne vue", "*bon+1 po*end\nVos sorts a distance ont *bon+10% d'effets*end");
            //AddTalent(talent.cc, "Coup critique", "*bon+15% chance de coup critique(cc)*end\n*bon+25% effets de coup critique(ec)*end");
            //AddTalent(talent.heal, "Soins", "Infligez des dégâts vous soigne de *bon5%*end de vos pdv max");
            AddTalent(talent.armor, "Armure", "Au début de votre tour gagnez *arm10%*end de vos pdv max en armure");
            AddTalent(talent.efficacity, "Efficacité", "Votre talent le plus a gauche a *bon+100% d'effet*end");
            //AddTalent(talent.protection, "Protection", "A la fin de votre tour gagnez *arm5 armure*end pour chaque po que vous possédez");
            //AddTalent(talent.TirAuCoeur, "Tir au coeur", "Frapper un ennemie lui inflige *bon5 saignement(1 fois par tour et par ennemie)*end");
            //AddTalent(talent.adaptation, "Adapatation", "Au debut de votre tour si aucune ennemie est a moins de 3 cases gagnez *bon1 po*end sinon gagnez *bon1 pm*end");
            //AddTalent(talent.slower, "Ralentisseur", "Retirez des pa ou pm donne *arm10%*end de votre vie max en armure");
            //AddTalent(talent.Iccanobif, "Iccanobif", "Retirez des pa ou pm donne *bon+5%*end d'effet pour ce combat(cumulable)");
            AddTalent(talent.spikeDamage, "Dégâts d'epines", "Les épines infligent *bon3*end dégâts supplementaire");
        }
        else if (V.IsUk())
        {
            AddTalent(talent.empty, "Empty", "Empty talent");
            AddTalent(talent.DeGauche, "Fromleft", "Your spell the most at the left have *bon+20%*end effect\n<i>*infyou can reorganize your *inlspell*end while not in combat *end</i>");
            //AddTalent(talent.SpePower, "Power specialisation", "*dmg+20%*end damage\nWhen you gain *wardefense*end gain *warpower*end instead");
            //AddTalent(talent.SpeDefense, "Defense specialisation", "*dmg+100%*end spikes\nWhen you gain *warpower*end gain *wardefense*end instead");
            //AddTalent(talent.InfiniteSpike, "Infinite spike", "When you gagne defense *bon+3 spike*end for this combat");
            //AddTalent(talent.InfiniteEffect, "Infinite Effect", "When you gagne power *bon+3% effect*end for this combat");
            //AddTalent(talent.BleedingMaster, "Bleeding mastery", "When an ennemy receive bleeding damage *bon+10 defense*end");
            //AddTalent(talent.goodView, "Great view", "*bon+1 po*end\nYour distance spell have *bon+10% effect*end");
            //AddTalent(talent.cc, "Critical hit", "*bon+15% critical hit chance(cc)*end\n*bon+25% critical hit effect(ce)*end");
            //AddTalent(talent.heal, "Heal", "Dealing damage heal *bon5%*end of your max healt");
            AddTalent(talent.armor, "Armor", "At the end of your turn gain *arm10%*end of your max healt to amor");
            AddTalent(talent.efficacity, "Efficacity", "Your talent the most at the left gain *bon+100% effect*end");
            //AddTalent(talent.protection, "Protection", "At the end of your turn gain *arm5 armor*end for each po you have");
            //AddTalent(talent.TirAuCoeur, "Tir au coeur", "Hiting ennemie deal *bon5 bleeding(once per turn per ennemy)*end");
            //AddTalent(talent.adaptation, "Adapatation", "At the start of your turn if any ennemy is around 3 square gain *bon1 po*end else gain *bon1 mp*end");
            //AddTalent(talent.slower, "Slower", "Removing ap or mp give *arm10%*end max life as armor");
            //AddTalent(talent.Iccanobif, "Iccanobif", "Removing ap or mp give *bon+5%*end effect for this combat(cumulative)");
            AddTalent(talent.spikeDamage, "Spike damage", "Spike deal *bon3*end additional damage");

        }
    }

    public static List<talent> lockedTalent = new List<talent>();

    public static talent takeRandomLockedTalent(bool withCon)
    {
        List<talent> tls = new List<talent>(lockedTalent);

        int i = 0;
        while (i < tls.Count)
        {
            talent t = tls[i];

            talentInfo inf = ls[t];

            if (!inf.CanBeObtained() || (withCon && !inf.Con()))
            {
                tls.Remove(t);

                i--;
            }

            i++;
        }

        if (tls.Count == 0)
            return talent.empty;

        return tls[UnityEngine.Random.Range(0, tls.Count)];
    }

    public static List<talent> unlockedTalent = new List<talent>();

    public static List<talent> newTalentUnlocked = new List<talent>();

    public static void UnlockTalent(talent t)
    {
        if (lockedTalent.Contains(t))
            lockedTalent.Remove(t);
        else
            return;

        unlockedTalent.Add(t);
        newTalentUnlocked.Add(t);
    }

    public static Dictionary<talent, talentInfo> ls = new Dictionary<talent, talentInfo>();

    public static void AddTalent(talent t, string title, string description)
    {
        if (t != talent.empty)
        {
            lockedTalent.Add(t);
        }

        talentInfo inf = (talentInfo)Activator.CreateInstance(Type.GetType("talent_" + t.ToString()));

        inf.Init(title, description, t);

        ls.Add(t, inf);
    }

    public static string GetTitle(talent t)
    {
        return ls[t].title;
    }

    public static string GetDescription(talent t)
    {
        return ls[t].description;
    }

    public static Sprite GetSprite(talent t)
    {
        return ls[t].sp;
    }

    public static talentInfo Get(talent t)
    {
        return ls[t];
    }
}

public class talentInfo
{
    public string title, description;

    public Talent_Gestion.talent t;

    public Sprite sp;

    public virtual void Init(string title, string description, Talent_Gestion.talent t)
    {
        this.title = title;
        this.description = description;

        this.t = t;

        sp = Resources.Load<Sprite>("Image/talent/" + t.ToString());
    }

    public virtual void Apply(float power)
    {

    }

    public static float calcPower(float b, float power)
    {
        return (float)b * power;
    }

    public static int calcPower_int(int b, float power)
    {
        return Mathf.CeilToInt((float)b * power);
    }

    public virtual void Remove()
    {

    }

    public virtual bool CanBeObtained()
    {
        return true;
    }

    public virtual bool Con()
    {
        return true;
    }

}
