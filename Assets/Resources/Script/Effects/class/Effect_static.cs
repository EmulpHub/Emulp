using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class Effect : MonoBehaviour
{
    public static Effect CreateEffect(string title ,effectType type, int strenght, int duration, Sprite sp, Reduction_mode reduction_mode,string codeName = "")
    {
        Effect instance = (Effect)Activator.CreateInstance(Type.GetType("Effect_" + type.ToString()));

        instance.title = title;
        instance.type = type;
        instance.str = strenght;
        instance.durationInTurn = duration;
        instance.img = sp;
        instance.reduction_Mode = reduction_mode;
        instance.unique = true;
        instance.CodeName = codeName;

        return instance;
    }

    public static Effect CreateEffect(effectType type, int strenght, int duration, Sprite sp, Reduction_mode reduction_mode,string codeName = "")
    {
        Effect instance = (Effect)Activator.CreateInstance(Type.GetType("Effect_" + type.ToString()));

        instance.title = "";
        instance.type = type;
        instance.str = strenght;
        instance.durationInTurn = duration;
        instance.img = sp;
        instance.reduction_Mode = reduction_mode;
        instance.unique = true;
        instance.CodeName = codeName;

        return instance;
    }

    public static Effect CreateCustomEffect(int duration, Sprite sp, Reduction_mode reduction_mode, string title, List<Effect> ls,string codeName = "")
    {
        Effect_custom instance = (Effect_custom)Activator.CreateInstance(Type.GetType("Effect_custom"));

        instance.type = effectType.custom;
        instance.durationInTurn = duration;
        instance.img = sp;
        instance.reduction_Mode = reduction_mode;
        instance.title = title;
        instance.effectLs = ls;
        instance.unique = true;
        instance.CodeName = codeName;

        return instance;
    }

    public static Effect CreateEffectForCustom(effectType type, int strenght)
    {
        Effect instance = (Effect)Activator.CreateInstance(Type.GetType("Effect_" + type.ToString()));

        instance.type = type;
        instance.str = strenght;
        instance.id = effect_id;
        instance.unique = false;

        effect_id++;

        return instance;
    }

    public static Effect CreateEffectTxtForCustom(string description)
    {
        Effect_customTxt instance = (Effect_customTxt)Activator.CreateInstance(Type.GetType("Effect_customTxt"));

        instance.description = description;
        instance.unique = false;

        return instance;
    }

    public static Effect CreateCustomTxtEffect(string title, string description, int duration, Sprite sp, Reduction_mode reduction_mode,string codeName = "")
    {
        Effect_customTxt instance = (Effect_customTxt)Activator.CreateInstance(Type.GetType("Effect_customTxt"));

        
        instance.type = effectType.customTxt;
        instance.durationInTurn = duration;
        instance.img = sp;
        instance.reduction_Mode = reduction_mode;

        instance.title = title;
        instance.description = description;
        instance.unique = true;
        instance.CodeName = codeName;

        return instance;
    }

    public static void AddNorticeSurfacePower(int nb)
    {
        Main_UI.Display_movingText_basicValue("+" + nb, V.Color.green, V.player_entity.transform.position, V.norticeSurface_power);

        V.player_entity.AddEffect(
            Effect.CreateEffect(Effect.effectType.effectPercentage, nb, 0, V.norticeSurface_power, Effect.Reduction_mode.never)
            );
    }
}
