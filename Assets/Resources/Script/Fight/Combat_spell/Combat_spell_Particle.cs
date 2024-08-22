using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class Spell : MonoBehaviour
{
    public static GameObject Particle_bloodLoss, Particle_Impact, Particle_Icon, Particle_Leaf, Particle_Impact_Entering, Particle_Impact_Entering_Uncomplete, Particle_Impact_Entering_Uncomplete_Static, Particle_Circle_Endless, Particle_Raising,
        Particle_Impact_Entering_Static_Img, particle_Bleeding;

    public static void Initialize_Particle()
    {
        Particle_bloodLoss = Resources.Load<GameObject>("Prefab/Particle/Particle_BloodLoss");
        Particle_Impact = Resources.Load<GameObject>("Prefab/Particle/Particle_Impact");
        Particle_Impact_Entering = Resources.Load<GameObject>("Prefab/Particle/Particle_Impact_Entering");
        Particle_Impact_Entering_Uncomplete = Resources.Load<GameObject>("Prefab/Particle/Particle_Impact_Entering_Uncomplete");
        Particle_Impact_Entering_Uncomplete_Static = Resources.Load<GameObject>("Prefab/Particle/Particle_Impact_Entering_Uncomplete_Static");
        Particle_Icon = Resources.Load<GameObject>("Prefab/Particle/Particle_Icon");
        Particle_Leaf = Resources.Load<GameObject>("Prefab/Particle/Particle_Leaf");
        Particle_Circle_Endless = Resources.Load<GameObject>("Prefab/Particle/Particle_Circle_Endless");
        Particle_Raising = Resources.Load<GameObject>("Prefab/Particle/Particle_Raising");
        Particle_Impact_Entering_Static_Img = Resources.Load<GameObject>("Prefab/Particle/Particle_Impact_Entering_Static_Img");
        particle_Bleeding = Resources.Load<GameObject>("Prefab/Particle/Particle_bleeding_endless");
    }

    public GameObject CreateParticle_BloodLoss(Vector2 position, float scale = 1, Particle_Amount Amount = Particle_Amount._8, float rotationDegre = 0)
    {
        GameObject g = Instantiate(Particle_bloodLoss);

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifyBurst(ps, ReturnAmount(Amount));

        var Shape = ps.shape;

        Shape.rotation = new Vector3(0, rotationDegre, 0);

        g.transform.localScale = new Vector3(scale, scale, 1);

        g.transform.position = position;

        Destroy(g, 4);

        return g;

    }

    public GameObject CreateParticle_Raising(Vector2 position, float scale = 1, Particle_Amount Amount = Particle_Amount._8, Particle_Color Color = Particle_Color.impact)
    {
        GameObject g = Instantiate(Particle_Raising);

        g.transform.rotation = Quaternion.Euler(-90, 0, 0);

        g.transform.localScale = new Vector3(scale, scale, 1);

        g.transform.position = position;

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifyColor(ps, ReturnColor(Color));

        ModifyBurst(ps, ReturnAmount(Amount));

        Destroy(g, 4);
        return g;

    }


    public GameObject CreateParticle_Lowering(Vector2 position, float scale = 1, Particle_Amount Amount = Particle_Amount._8, Particle_Color Color = Particle_Color.impact)
    {
        GameObject g = Instantiate(Particle_Raising);

        g.transform.rotation = Quaternion.Euler(90, 0, 0);

        g.transform.localScale = new Vector3(scale, scale, 1);

        g.transform.position = position;

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifyColor(ps, ReturnColor(Color));

        ModifyBurst(ps, ReturnAmount(Amount));

        Destroy(g, 4);
        return g;

    }

    public GameObject CreateParticle_Impact(Vector2 position, float scale = 1, Particle_Amount Amount = Particle_Amount._8, Particle_Color Color = Particle_Color.impact)
    {
        GameObject g = Instantiate(Particle_Impact);

        g.transform.localScale = new Vector3(scale, scale, 1);

        g.transform.position = position;

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifyColor(ps, ReturnColor(Color));

        ModifyBurst(ps, ReturnAmount(Amount));

        Destroy(g, 4);
        return g;

    }

    public GameObject CreateParticle_Circle_Endless(Vector2 position, float scale = 1, Particle_Amount Amount = Particle_Amount._8, Particle_Color Color = Particle_Color.impact)
    {
        GameObject g = Instantiate(Particle_Circle_Endless);

        g.transform.localScale = new Vector3(scale, scale, 1);

        g.transform.position = position;

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifyColor(ps, ReturnColor(Color));

        ModifyBurst(ps, ReturnAmount(Amount));

        return g;
    }

    public GameObject CreateParticle_Impact_Entering(Vector2 position, float scale = 1, Particle_Amount Amount = Particle_Amount._8, Particle_Color Color = Particle_Color.impact)
    {
        GameObject g = Instantiate(Particle_Impact_Entering);

        g.transform.localScale = new Vector3(scale, scale, 1);

        g.transform.position = position;

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifyColor(ps, ReturnColor(Color));

        ModifyBurst(ps, ReturnAmount(Amount));

        Destroy(g, 4);

        return g;
    }


    public GameObject CreateParticle_Impact_Entering_Uncomplete(Vector2 position, float scale = 1, Particle_Amount Amount = Particle_Amount._8, Particle_Color Color = Particle_Color.impact)
    {
        GameObject g = Instantiate(Particle_Impact_Entering_Uncomplete);

        g.transform.localScale = new Vector3(scale, scale, 1);

        g.transform.position = position;

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifyColor(ps, ReturnColor(Color));

        ModifyBurst(ps, ReturnAmount(Amount));

        Destroy(g, 4);

        return g;
    }

    public GameObject CreateParticle_Impact_Entering_Static_Img(Vector2 position, Texture2D sp, float scale = 1, Particle_Amount Amount = Particle_Amount._8, float rotationDegree = 0)
    {
        GameObject g = Instantiate(Particle_Impact_Entering_Static_Img);

        g.transform.localScale = new Vector3(scale, scale, 1);

        g.transform.position = position;

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ParticleSystemRenderer ps_renderer = g.GetComponent<ParticleSystemRenderer>();

        Material a = new Material(ps_renderer.material);

        a.mainTexture = sp;

        ps_renderer.material = a;

        ModifyBurst(ps, ReturnAmount(Amount));

        g.transform.DORotate(new Vector3(0, 0, rotationDegree), 1.5f);

        Destroy(g, 4);

        return g;
    }

    public GameObject CreateParticle_Impact_Entering_Uncomplete_Static(Vector2 position, float scale = 1, Particle_Amount Amount = Particle_Amount._8, Particle_Color Color = Particle_Color.impact)
    {
        GameObject g = Instantiate(Particle_Impact_Entering_Uncomplete_Static);

        g.transform.localScale = new Vector3(scale, scale, 1);

        g.transform.position = position;

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifyColor(ps, ReturnColor(Color));

        ModifyBurst(ps, ReturnAmount(Amount));

        Destroy(g, 4);

        return g;
    }

    public GameObject CreateParticle_Icon(Vector2 position, Sprite sp, float scale = 1, Particle_Amount Amount = Particle_Amount._8)
    {
        GameObject g = Instantiate(Particle_Icon);

        g.transform.localScale = new Vector3(scale, scale, 1);

        g.transform.position = position;

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifySprite(ps, sp);

        ModifyBurst(ps, ReturnAmount(Amount));

        Destroy(g, 4);

        return g;
    }

    public GameObject CreateParticle_Impact_Blood(Vector2 position, float scale = 1, Particle_Amount Amount = Particle_Amount._8)
    {
        GameObject g = Instantiate(Particle_Impact);

        g.transform.localScale = new Vector3(scale, scale, 1);

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifyColor(ps, color_blood);

        ModifyBurst(ps, ReturnAmount(Amount));

        g.transform.position = position;

        Destroy(g, 4);

        return g;
    }

    public GameObject CreateParticle_Endless_Bleeding(Vector2 position, float scale = 1)
    {
        GameObject g = Instantiate(particle_Bleeding);

        g.transform.localScale = new Vector3(scale, scale, 1);

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifyColor(ps, color_blood);

        g.transform.position = position;

        return g;
    }

    public enum Particle_Color { impact, bonus, blood, yellow, green }

    public Color32 color_impact, color_bonus, color_blood, color_yellow, color_green;

    public Color32 ReturnColor(Particle_Color color)
    {
        switch (color)
        {
            case Particle_Color.impact:
                return color_impact;
            case Particle_Color.bonus:
                return color_bonus;
            case Particle_Color.blood:
                return color_blood;
            case Particle_Color.yellow:
                return color_yellow;
            case Particle_Color.green:
                return color_green;
        }

        throw new System.Exception("No color found particle.return color");
    }

    public void CreateParticle_Bonus(Vector2 position, float scale = 1, Particle_Amount Amount = Particle_Amount._8)
    {
        GameObject g = Instantiate(Particle_Impact);

        g.transform.localScale = new Vector3(scale, scale, 1);

        ParticleSystem ps = g.GetComponent<ParticleSystem>();

        ModifyColor(ps, color_bonus);

        ModifyBurst(ps, ReturnAmount(Amount));

        g.transform.position = position;

        Destroy(g, 4);
    }

    public void CreateParticle_Leaf(Vector2 position, float scale = 1)
    {
        GameObject g = Instantiate(Particle_Leaf);

        g.transform.localScale = new Vector3(scale, scale, 1);

        g.transform.position = position;

        Destroy(g, 4);
    }

    #region Particle

    public void ModifyColor(ParticleSystem ps, Color32 color)
    {
        ParticleSystem.MainModule settings = ps.main;

        settings.startColor = new ParticleSystem.MinMaxGradient(color);
    }

    public void ModifySprite(ParticleSystem ps, Sprite sp)
    {
        ParticleSystem.TextureSheetAnimationModule a = ps.textureSheetAnimation;

        a.SetSprite(0, sp);
    }

    #endregion

    #region Burst System

    public enum Particle_Amount { _2, _3, _4, _5, _6, _8, _12, _16, _24, _36, _30, _20, _48 }

    public int ReturnAmount(Particle_Amount amount)
    {
        switch (amount)
        {

            case Particle_Amount._2:
                return 2;
            case Particle_Amount._3:
                return 3;
            case Particle_Amount._4:
                return 4;
            case Particle_Amount._5:
                return 5;
            case Particle_Amount._6:
                return 6;
            case Particle_Amount._8:
                return 8;
            case Particle_Amount._12:
                return 12;
            case Particle_Amount._16:
                return 16;
            case Particle_Amount._24:
                return 24;
            case Particle_Amount._36:
                return 36;
            case Particle_Amount._30:
                return 30;
            case Particle_Amount._20:
                return 20;
            case Particle_Amount._48:
                return 48;
        }

        throw new System.Exception("No valid Particle_Amount");
    }

    public static void ModifyBurst(ParticleSystem theParticleSystem, int Amount)
    {
        //Burst
        ParticleSystem.Burst NewBurst = theParticleSystem.emission.GetBurst(0);

        NewBurst.count = Amount;

        theParticleSystem.emission.SetBurst(0, NewBurst);
    }

    public static void ModifyRotation(ParticleSystem theParticleSystem, Vector3 rota)
    {

    }

    #endregion
}
