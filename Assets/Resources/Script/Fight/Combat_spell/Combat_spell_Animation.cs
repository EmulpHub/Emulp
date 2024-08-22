using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public partial class Spell : MonoBehaviour
{
    #region CreateAnimation_V2

    public GameObject spell_animation;

    /// <summary>
    /// The speed, and strengh and scale of animation
    /// </summary>
    public float animation_speed_brut; //0.2

    #region SetCommonValues

    public (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) SetCommonValues(bool SetTransformParentToTarget, Entity target)
    {
        Transform transform_parent = null;
        if (SetTransformParentToTarget && target != null)
        {
            transform_parent = target.Renderer_movable.transform;
        }

        GameObject newAnimation = null;
        if (transform_parent == null)
        {
            newAnimation = Instantiate(spell_animation, transform_parent);
        }
        else
        {
            newAnimation = Instantiate(spell_animation, transform_parent);
        }

        Transform img_parent = newAnimation.transform.GetChild(0);
        Image img_1 = img_parent.GetChild(0).GetComponent<Image>();
        Image img_2 = img_parent.GetChild(1).GetComponent<Image>();

        img_parent.transform.localScale = new Vector3(1, 1, 1);

        img_2.gameObject.SetActive(false);

        Animator anim_Parent = img_parent.gameObject.GetComponent<Animator>();
        Animator anim = img_1.gameObject.GetComponent<Animator>();
        Animator anim_2 = img_2.gameObject.GetComponent<Animator>();

        anim_Parent.enabled = false;
        anim.enabled = false;
        anim_2.enabled = false;

        return (img_parent, img_1, img_2, newAnimation, anim, anim_2, anim_Parent);
    }

    public (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) SetCommonValues(Entity target)
    {
        return SetCommonValues(false, target);
    }

    public (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
    Animator anim_2, Animator anim_Parent) SetCommonValues()
    {
        return SetCommonValues(false, null);
    }

    public void PlayAnimation(Animator a, string name)
    {
        a.enabled = true;

        a.Play(name);
    }

    public Vector2 TakeNewPos(Vector2 startPosition, float randomness, Vector2 lastPosition, float tolerance)
    {
        Vector2 newPos = startPosition + new Vector2(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness));

        while (F.DistanceBetweenTwoVector_INT(newPos, lastPosition) <= tolerance)
        {
            newPos = startPosition + new Vector2(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness));

        }

        return newPos;
    }

    #endregion

    #region AllAnimation

    #region Player

    #region CommonAnimation

    #region PopDown

    public float animation_PopDown_startScale, animation_PopDown_endScale;

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_PopDown(Sprite sp, Vector2 startPosition, float anim_speed = 1, float size_multiplier = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        anim_speed = animation_speed_brut * anim_speed;

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        //Animation
        v.anim.enabled = false;

        v.img_1.DOKill();
        v.img_1.DOFade(1, 0);
        v.img_1.transform.localScale = new Vector3(animation_PopDown_startScale * size_multiplier, animation_PopDown_startScale * size_multiplier, 1);
        v.img_1.transform.DOScale(animation_PopDown_endScale * size_multiplier, anim_speed + 0.4f);

        yield return new WaitForSeconds((float)((anim_speed + 0.6) / 3));

        v.img_1.DOFade(0, anim_speed + 0.6f);

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region PopDown

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_PopDown_Shaking(Sprite sp, Vector2 startPosition, float anim_speed = 1, float size_multiplier = 1, float shaking_multiplier = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        anim_speed = animation_speed_brut * anim_speed;

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        //Animation
        v.anim.enabled = false;

        v.img_1.DOKill();
        v.img_1.DOFade(1, 0);
        v.img_1.transform.DOPunchRotation(new Vector3(0, 0, 10) * shaking_multiplier, anim_speed + 0.2f, 30, 3);
        v.img_1.transform.localScale = new Vector3(animation_PopDown_startScale * size_multiplier, animation_PopDown_startScale * size_multiplier, 1);
        v.img_1.transform.DOScale(animation_PopDown_endScale * size_multiplier, anim_speed + 0.4f);

        yield return new WaitForSeconds((float)((anim_speed + 0.6) / 3));

        v.img_1.DOFade(0, anim_speed + 0.6f);

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region PopUp

    public float animation_PopUp_startScale, animation_PopUp_endScale;
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_PopUp(Sprite sp, Vector2 startPosition, float anim_speed = 1, float anim_multiplier = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        anim_speed = animation_speed_brut * anim_speed;

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        //Animation

        v.img_1.transform.localScale = new Vector3(animation_PopUp_startScale * anim_multiplier, animation_PopUp_startScale * anim_multiplier, 1);
        v.img_1.transform.DOScale(animation_PopUp_endScale * anim_multiplier, anim_speed);

        yield return new WaitForSeconds(anim_speed * 0.8f);

        v.img_1.DOFade(0, anim_speed);

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region PopUpBig

    float BigScale = 2;//The difference between popUp and popUpBig in scale

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_PopUpBig(Sprite sp, Vector2 startPosition, float anim_speed = 1, float anim_multiplier = 1)
    {
        StartCoroutine(Anim_PopUp(sp, startPosition, anim_speed, anim_multiplier * BigScale));

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region PopUpBigRotated

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_PopUpBigRotated(Sprite sp, Vector2 startPosition, float size_multiplier = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;
        v.newAnimation.transform.localScale = v.newAnimation.transform.localScale * size_multiplier;

        //Animation

        PlayAnimation(v.anim, "spell_PopUpRotated");

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region PopUpBigSpinning

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_PopUpBigSpinning(Sprite sp, Vector2 startPosition, float size_multiplier = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;
        v.newAnimation.transform.localScale = v.newAnimation.transform.localScale * size_multiplier;

        //Animation

        PlayAnimation(v.anim, "spell_PopUpSpinning");

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region PopUpTalent

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_PopUpTalent(Sprite sp, Vector2 startPosition)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        v.newAnimation.GetComponent<Canvas>().sortingLayerID = 7;

        //Animation
        PlayAnimation(v.anim, "PopUp_Talent");

        Destroy(v.newAnimation, 2.4f);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region FallingDown
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_FallingDown(Sprite sp, Vector2 startPosition, float anim_speed = 1, float anim_sizeMultiplicator = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        anim_speed *= animation_speed_brut;

        //Animation
        v.anim.enabled = false;

        v.img_1.transform.localScale = new Vector3(animation_PopUp_startScale * anim_sizeMultiplicator, animation_PopUp_startScale * anim_sizeMultiplicator, 1);
        v.img_1.transform.DOScale(animation_PopUp_endScale * anim_sizeMultiplicator, anim_speed);

        v.img_1.transform.DOMoveY(v.img_1.transform.position.y - 0.6f, anim_speed);

        v.img_1.DOFade(0, anim_speed + 0.4f);

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region Punch

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Punch(Sprite sp, Vector2 startPosition, bool right, float anim_speed = 1, float anim_size = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        anim_speed = animation_speed_brut * anim_speed;

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;
        v.newAnimation.transform.localScale *= anim_size;

        //Animation
        v.anim.enabled = false;

        float yRotation = 0;

        if (right)
        {
            v.newAnimation.transform.DOLocalMoveX(v.newAnimation.transform.localPosition.x + 0.5f, anim_speed);
            yRotation = 180;
        }
        else
        {
            v.newAnimation.transform.DOLocalMoveX(v.newAnimation.transform.localPosition.x - 0.5f, anim_speed);
        }

        v.img_1.transform.localRotation = Quaternion.Euler(0, yRotation, 0);

        yield return new WaitForSeconds(anim_speed * 1.4f);

        v.img_1.DOFade(0, anim_speed * 2);

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region LittleSword
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_LittleSword(Sprite sp, Vector2 startPosition, bool right)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;

        Vector2 offset = new Vector2(-0.15f, 0);

        if (!right)
            offset = new Vector2(0.15f, 0);

        v.newAnimation.transform.position = startPosition - offset;

        v.newAnimation.transform.rotation = Quaternion.Euler(0, right ? 0 : 180, 0);

        //Animation
        PlayAnimation(v.anim, "skill_Dague");

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region Menace

    /// <summary>
    /// ImpactToTarget After 1 sec
    /// </summary>
    /// <param name="sp"></param>
    /// <returns></returns>
    public IEnumerator Anim_Menacing(Entity caster, Sprite sp, Vector2 targetPosition, bool spinning = false, float travelTime = 1, float anim_SizeMultiplicator = 1, float additionalDegree = 0, float shaking_multiplier = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;

        Vector2 startPos = caster.transform.position + new Vector3(0, 0.5f, 0);

        v.newAnimation.transform.position = startPos;

        v.img_parent.transform.localScale *= anim_SizeMultiplicator;

        //Animation
        v.anim.enabled = false;
        v.anim_2.enabled = false;

        v.anim_Parent.enabled = false;

        Vector3 dir = targetPosition - startPos;

        dir.Normalize();

        v.img_parent.transform.DOMove((Vector3)startPos + dir * 0.8f, travelTime);

        //LaunchProjectile_WithoutDoMove(targetPosition, caster.transform.position + V.Entity_DistanceToBody_Vector3);

        if (!spinning)
        {
            float Degre = F.DegreeTowardThisDirection(targetPosition, (Vector2)caster.transform.position + new Vector2(0, 0.5f)) + additionalDegree;

            if (spell == SpellGestion.List.monster_funky_attack)
            {
                Degre -= 45;
            }

            v.img_parent.transform.rotation = Quaternion.Euler(0, 0, Degre);


            yield return new WaitForSeconds(0.35f);

            v.img_1.transform.DOPunchRotation(new Vector3(0, 0, 60) * shaking_multiplier, 0.5f, 30);

            CreateParticle_Impact(v.img_parent.transform.position, 1, Particle_Amount._16, Particle_Color.yellow);

            yield return new WaitForSeconds(0.25f);
        }
        else if (spinning)
        {
            while (travelTime > 0)
            {
                v.img_parent.transform.rotation = v.img_parent.transform.rotation * Quaternion.Euler(0, 0, 1 + 600 * Time.deltaTime);

                travelTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }


        v.img_1.DOFade(0, 1);

        Destroy(v.newAnimation, 6);
    }

    #endregion

    #region Projectile
    /// <summary>
    /// ImpactToTarget After 1 sec
    /// </summary>
    /// <param name="sp"></param>
    /// <returns></returns>
    public IEnumerator Anim_Projectile(Entity caster, Sprite sp, Vector2 targetPosition, bool spinning = false, float travelTime = 1, float anim_SizeMultiplicator = 1, float additionalDegree = 0)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = caster.transform.position + new Vector3(0, 0.5f, 0);

        v.img_parent.transform.localScale *= anim_SizeMultiplicator;

        //Animation
        v.anim.enabled = false;
        v.anim_2.enabled = false;

        v.anim_Parent.enabled = false;

        //v.img_parent.transform.DOMove((Vector3)targetPosition, 0.65f / anim_speed);

        StartCoroutine(LaunchProjectile_WithoutDoMove(caster.transform.position + V.Entity_DistanceToBody_Vector3, targetPosition, v.newAnimation.gameObject, 1 / travelTime));

        //LaunchProjectile_WithoutDoMove(targetPosition, caster.transform.position + V.Entity_DistanceToBody_Vector3);

        if (!spinning)
        {
            float Degre = F.DegreeTowardThisDirection(targetPosition, (Vector2)caster.transform.position + new Vector2(0, 0.5f)) + additionalDegree;

            if (spell == SpellGestion.List.monster_funky_attack)
            {
                Degre -= 45;
            }

            v.img_parent.transform.rotation = Quaternion.Euler(0, 0, Degre);

            yield return new WaitForSeconds(0.65f);
        }
        else if (spinning)
        {
            while (travelTime > 0)
            {
                v.img_parent.transform.rotation = v.img_parent.transform.rotation * Quaternion.Euler(0, 0, 1 + 600 * Time.deltaTime);

                travelTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        Destroy(v.newAnimation, 6);
    }

    public IEnumerator LaunchProjectile_WithoutDoMove(Vector3 StartPos, Vector3 EndPos, GameObject projectile, float speed)
    {
        Vector3 dir = EndPos - StartPos;

        dir.Normalize();

        float dis = F.DistanceBetweenTwoPos_float(EndPos, projectile.transform.position);

        float multiplier = dis * speed; //Target = 1 second

        float timer = dis / (multiplier);

        while (projectile != null && timer > 0)
        {
            projectile.transform.Translate(dir * Time.deltaTime * multiplier);

            timer -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        Sprite damageExplosion = Resources.Load<Sprite>("Image/Monster/Spell/entity_damageExplosion_2");
        StartCoroutine(Anim_PopUp(damageExplosion, projectile.transform.position));

        Destroy(projectile.gameObject);
    }

    #endregion

    #region Projectile Do Move

    public IEnumerator Anim_Projectile_DoMove(Entity caster, Sprite sp, Vector2 targetPosition, bool spinning = false, float travelTime = 1, float anim_SizeMultiplicator = 1, float additionalDegree = 0)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = caster.transform.position + new Vector3(0, 0.5f, 0);

        v.img_parent.transform.localScale *= anim_SizeMultiplicator;

        //Animation
        v.anim.enabled = false;
        v.anim_2.enabled = false;

        v.anim_Parent.enabled = false;

        v.img_parent.transform.DOMove((Vector3)targetPosition, travelTime);

        v.img_1.DOFade(0, travelTime + 0.3f);
        v.img_2.DOFade(0, travelTime + 0.3f);

        // StartCoroutine(LaunchProjectile_WithoutDoMove(caster.transform.position + V.Entity_DistanceToBody_Vector3, targetPosition, v.newAnimation.gameObject, 1 / travelTime));

        //LaunchProjectile_WithoutDoMove(targetPosition, caster.transform.position + V.Entity_DistanceToBody_Vector3);

        if (!spinning)
        {
            float Degre = F.DegreeTowardThisDirection(targetPosition, (Vector2)caster.transform.position + new Vector2(0, 0.5f)) + additionalDegree;

            if (spell == SpellGestion.List.monster_funky_attack)
            {
                Degre -= 45;
            }

            v.img_parent.transform.rotation = Quaternion.Euler(0, 0, Degre);

            yield return new WaitForSeconds(0.65f);
        }
        else if (spinning)
        {
            while (travelTime > 0)
            {
                v.img_parent.transform.rotation = v.img_parent.transform.rotation * Quaternion.Euler(0, 0, 1 + 600 * Time.deltaTime);

                travelTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        Destroy(v.newAnimation, 6);
    }


    #endregion

    #region ComingFromBottom
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_ComingFromBottom(Entity target, Sprite sp, Vector2 startPosition, float speed = 1, float additionalYPos = 0.5f, float size = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues(true, target);

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;
        v.newAnimation.transform.localScale = v.newAnimation.transform.localScale * size;

        //Animation
        v.anim.enabled = false;
        v.anim_2.enabled = false;

        v.newAnimation.transform.DOScale(v.newAnimation.transform.localScale * 0.9f, speed);
        v.newAnimation.transform.DOMoveY(startPosition.y + additionalYPos, speed);

        yield return new WaitForSeconds(0.3f);

        v.img_1.DOFade(0, 0.5f);
        v.img_2.DOFade(0, 0.5f);

        Destroy(v.newAnimation, 2);

    }

    #endregion

    #region Special

    #region Rampart

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Rampart(Sprite sp, Vector2 startPosition, float anim_speed = 1, float size_multiplier = 1)
    {
        int nb = 3;

        float startPitch = 1 + Random.Range(-0.2f, 0.2f);

        Vector2 lastPos = new Vector2(99, 99);

        for (int i = 0; i < nb; i++)
        {
            SoundManager.PlaySound(SoundManager.list.spell_warrior_rampart, startPitch - i * 0.1f); ;

            Vector2 newPos = TakeNewPos(startPosition, 0.5f, lastPos, 0.5f);

            lastPos = newPos;

            StartCoroutine(Anim_PopUp(sp, newPos, anim_speed, size_multiplier));

            CreateParticle_Bonus(newPos);

            yield return new WaitForSeconds(0.3f / anim_speed);
        }

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region EmergencyKit

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_EmergencyKit(Sprite sp, Vector2 startPosition, int count, float anim_speed = 1, float size_multiplier = 1)
    {
        Vector2 lastPos = new Vector2(99, 99);

        bool First = true;

        int nbHeal = 0;

        float multiplicator = 1;

        float multiplicator_down = 0.02f;

        for (int i = 0; i < count; i++)
        {
            Vector2 newPos = TakeNewPos(startPosition, 0.5f, lastPos, 0.5f);

            lastPos = newPos;

            if (First)
            {
                SoundManager.PlaySound(SoundManager.list.spell_warrior_heal, 0.5f);

                StartCoroutine(Anim_PopDown(sp, newPos, anim_speed * 0.5f, size_multiplier * 2.5f));
            }
            else
            {
                SoundManager.PlaySound(SoundManager.list.spell_warrior_heal, (i % 2 == 0) ? 1.2f : 0.8f);

                StartCoroutine(Anim_PopDown(sp, newPos, anim_speed, size_multiplier));

                CreateParticle_Bonus(newPos, 1.2f, Particle_Amount._12);
            }

            if (First)
            {
                First = false;

                yield return new WaitForSeconds(0.3f / anim_speed);
            }
            else
            {
                if (nbHeal == 1)
                {
                    yield return new WaitForSeconds(0.15f / anim_speed * multiplicator);
                    nbHeal = 0;
                }
                else
                {
                    yield return new WaitForSeconds(0.15f / anim_speed * 0.2f * multiplicator);

                    nbHeal++;
                }

                multiplicator -= multiplicator_down;

                multiplicator_down += 0.05f;
            }
        }

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region Kick
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Kick(Sprite sp, Vector2 startPosition, bool right, float anim_speed = 1, float additionalStartDegree = 0)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        float anim_speed_brut = anim_speed;

        anim_speed = animation_speed_brut * anim_speed;

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        //Animation
        v.img_1.transform.DOScale(1.2f, 0);

        float yRotation = 0;

        if (!right)
        {
            yRotation = 180;
        }

        v.img_1.transform.localRotation = Quaternion.Euler(0, yRotation, -50 + additionalStartDegree);
        v.img_1.transform.DORotate(new Vector3(0, yRotation, -70 + additionalStartDegree), 0.2f * anim_speed_brut);

        yield return new WaitForSeconds(0.2f * anim_speed_brut);

        v.img_1.transform.DORotate(new Vector3(0, yRotation, 12 + additionalStartDegree), 0.15f * anim_speed_brut);

        v.img_1.DOFade(0, anim_speed + 0.4f);

        Destroy(v.newAnimation, 2);
    }

    #endregion

    #region Whip
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Whip(Sprite sp, Vector2 startPosition, bool right, float anim_speed = 1, float anim_size = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        anim_speed = animation_speed_brut * anim_speed;

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;
        v.newAnimation.transform.localScale = v.newAnimation.transform.localScale * anim_size;

        //Animation
        v.img_1.transform.DOScale(1.2f, 0);

        float yRotation = 0;

        if (!right)
        {
            yRotation = 180;
        }

        v.img_parent.transform.localRotation = Quaternion.Euler(0, yRotation, 0);

        PlayAnimation(v.anim, "Whip_Animation");

        yield return new WaitForSeconds(0.3f);

        v.img_1.DOFade(0, anim_speed + 0.4f);

        Destroy(v.newAnimation, 2);
    }

    #endregion

    #region Uppercut
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Uppercut(Sprite sp, Vector2 startPosition, bool right)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        //Animation
        v.anim_Parent.enabled = false;

        v.anim.enabled = true;

        v.anim.Play("skill_uppercut");

        if (right)
        {
            v.img_parent.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            v.img_parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region Magic wand
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_MagicWand(Sprite sp, Vector2 startPosition, bool right, float size = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;
        v.newAnimation.transform.localScale = v.newAnimation.transform.localScale * size;

        //Animation
        v.anim_Parent.enabled = false;

        v.anim.enabled = true;

        PlayAnimation(v.anim, "skill_MagicWand");

        //v.anim.Play("skill_MagicWand");

        if (right)
        {
            v.img_parent.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            v.img_parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region Cudgel
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Cudgel(Sprite sp, Vector2 startPosition, bool right, float size = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;
        v.newAnimation.transform.localScale = v.newAnimation.transform.localScale * size;

        //Animation
        v.anim_Parent.enabled = false;

        v.anim.enabled = true;

        PlayAnimation(v.anim, "skill_Cudgel");

        //v.anim.Play("skill_MagicWand");

        if (right)
        {
            v.img_parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            v.img_parent.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region Dagger
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Dagger(Sprite sp, Vector2 startPosition, bool right)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        //Animation
        v.anim_Parent.enabled = false;

        v.anim.enabled = true;

        v.anim.Play("skill_dagger");

        if (right)
        {
            v.img_parent.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            v.img_parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region Lasso
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Lasso(Entity target)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues(true, target);

        v.newAnimation.transform.localPosition = Vector3.zero + new Vector3(0, 0.5f, 0);

        //Animation

        v.img_2.gameObject.SetActive(true);

        v.img_1.sprite = Resources.Load<Sprite>("Image/Animation/lasso_animation_down");
        v.img_2.sprite = Resources.Load<Sprite>("Image/Animation/lasso_animation_up");

        v.anim_Parent.enabled = true;

        v.anim_Parent.Play("skill_lasso");

        //V.ConvertPosInLegalMove(F.ConvertToString(startPosition - new Vector2(0, 0.5f)), target.CurrentPosition_string);

        yield return new WaitForSeconds(0.5f);

        v.img_1.DOFade(0, 0.5f);
        v.img_2.DOFade(0, 0.5f);

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region Heal
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Heal(Entity target, Sprite sp, Vector2 startPosition, float anim_speed = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues(true, target);

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        anim_speed = animation_speed_brut * anim_speed;

        void Mini_heal(Vector2 pos)
        {
            v.newAnimation.transform.position = pos;
            v.newAnimation.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-30, 30));

            v.anim.enabled = false;

            v.img_1.transform.localScale = new Vector3(animation_PopUp_startScale, animation_PopUp_startScale, 1);
            v.img_1.transform.DOScale(animation_PopUp_endScale, anim_speed);

            v.img_1.DOFade(0, anim_speed + 0.5f);
        }

        //Animation
        v.img_parent.gameObject.SetActive(false);

        Vector3 lastHealPos = new Vector3(100, 100);

        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = startPosition + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            while (F.DistanceBetweenTwoPos_float(pos, lastHealPos) <= 0.7f)
            {
                pos = startPosition + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            }

            lastHealPos = pos;

            SoundManager.PlaySound(SoundManager.list.spell_warrior_heal);

            Mini_heal(pos);

            yield return new WaitForSeconds(0.15f);
        }

        Destroy(v.newAnimation, 2);

    }

    #endregion

    #region Drive
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Drive(Sprite sp, Vector2 startPosition, float anim_speed = 1, float anim_sizeMultiplier = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        anim_sizeMultiplier *= 2.3f;

        //Animation
        v.anim.enabled = false;

        v.img_1.transform.localScale = new Vector3(animation_PopUp_startScale * anim_sizeMultiplier, animation_PopUp_startScale * anim_sizeMultiplier, 1);

        int force = 1;
        if (Random.Range(0, 1 + 1) == 1)
        {
            force = -1;
        }

        v.img_1.transform.DOPunchPosition(new Vector3(1, 0, 0) * force * 12, 1.4f * anim_speed, 3);

        yield return new WaitForSeconds(1);

        v.img_1.DOFade(0, 1);

        Destroy(v.newAnimation, 2);

    }

    #endregion

    #region DivineSword
    /*/// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_DivineSword(Entity target, Sprite sp, Vector2 startPosition)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues(true, target);

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        //Animation
        v.anim.enabled = false;
        v.anim_2.enabled = false;

        v.anim_Parent.Play("skill_divineSword");

        yield return new WaitForSeconds(0.65f);

        v.img_1.DOFade(0, 0.5f);
        v.img_2.DOFade(0, 0.5f);

        Destroy(v.newAnimation, 2);

    }*/

    public IEnumerator Anim_DivineSword(Entity target, Sprite sp, Vector2 startPosition, float speed = 1, float additionalYPos = 0.5f, float size = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues(true, target);

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;
        v.newAnimation.transform.localScale = v.newAnimation.transform.localScale * size;

        //Animation
        v.anim.enabled = true;
        v.anim_2.enabled = false;

        v.anim.Play("skill_divineSword");


        Destroy(v.newAnimation, 2);

        yield return null;
    }

    #endregion

    #region Aspiration
    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Aspiration(Sprite sp, Vector2 startPosition, float anim_speed = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        anim_speed *= animation_speed_brut;

        //Animation
        v.anim_Parent.enabled = false;
        v.anim.enabled = false;

        Vector3 newPos = V.player_entity.transform.position + new Vector3(0, 0.5f, 0);

        v.img_parent.transform.DOMove(newPos, anim_speed);

        v.img_1.transform.DOScale(0, anim_speed + 3);

        v.img_1.DOFade(0, anim_speed + 0.4f);

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region MassProtection

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_MassProtection(List<string> monster_pos, Sprite sp, Vector2 casterPosition, float anim_speed = 1, float anim_SizeMultiplicator = 1)
    {
        //SetVariables

        anim_speed *= animation_speed_brut;

        float individualSpeed = anim_speed * 1.5f; //0.3

        //Animation
        IEnumerator individual(string pos)
        {
            //SetVariables
            (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
            Animator anim_2, Animator anim_Parent) v_individual = SetCommonValues();

            v_individual.img_1.sprite = V.massProtection_Individual;
            Vector3 posCalc = F.ConvertToWorldVector2(pos) + new Vector2(0, 0.5f);

            v_individual.newAnimation.transform.position = posCalc;

            v_individual.img_1.transform.localScale = new Vector3(animation_PopUp_startScale * anim_SizeMultiplicator, animation_PopUp_startScale * anim_SizeMultiplicator, 1);
            v_individual.img_1.transform.DOScale(animation_PopUp_endScale * anim_SizeMultiplicator, individualSpeed);

            CreateParticle_Impact(posCalc, 1, Particle_Amount._8);

            SoundManager.PlaySound(SoundManager.list.spell_warrior_rampart);

            yield return new WaitForSeconds(individualSpeed + 0.3f);

            SoundManager.PlaySound(SoundManager.list.spell_warrior_flash);

            v_individual.newAnimation.transform.DOMove(casterPosition + new Vector2(0, 0.5f), anim_speed * 1.3f);

            yield return new WaitForSeconds(anim_speed * 1.3f - 0.2f);

            v_individual.img_1.DOFade(0, anim_speed + 0.4f);

            Destroy(v_individual.newAnimation, 2);
        }

        foreach (string p in monster_pos)
        {
            StartCoroutine(individual(p));
        }

        yield return new WaitForSeconds(individualSpeed * 1.5f + anim_speed * 2); //0.3 + 0.4

        //Player_Animation
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = casterPosition + new Vector2(0, 0.5f);

        anim_SizeMultiplicator *= 2;

        v.img_1.transform.localScale = new Vector3(animation_PopUp_startScale * anim_SizeMultiplicator, animation_PopUp_startScale * anim_SizeMultiplicator, 1);
        v.img_1.transform.DOScale(animation_PopUp_endScale * anim_SizeMultiplicator, anim_speed);

        SoundManager.PlaySound(SoundManager.list.spell_warrior_rampart, 0.5f);

        yield return new WaitForSeconds(anim_speed * 1.2f);

        v.img_1.DOFade(0, anim_speed + 0.4f);

        Destroy(v.newAnimation, 2);
    }

    #endregion

    #region LastBreath

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_LastBreath(Sprite sp, Vector2 startPosition, bool right, float SizeMultiplicator = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;
        v.img_parent.gameObject.transform.localScale = new Vector3(1, 1, 1) * SizeMultiplicator;

        //Animation
        v.anim_Parent.enabled = false;

        v.anim.enabled = true;

        v.anim.Play("Spell_LastBreath");

        if (right)
        {
            v.img_parent.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            v.img_parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region Flash

    /// <summary>
    /// Remember coroutine
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    public IEnumerator Anim_Flash(Sprite sp, Vector2 startPosition, float size_multiplier = 1, bool right = true)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;
        v.newAnimation.transform.localScale = v.newAnimation.transform.localScale * size_multiplier;
        float yRotation = right ? 0 : 180;
        v.newAnimation.transform.rotation = Quaternion.EulerAngles(0, yRotation, 0);
        //Animation

        PlayAnimation(v.anim, "spell_flash");

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region SpikePosture

    public IEnumerator Anim_SpikePosture(Sprite sp, Vector2 startPosition, float size_multiplier = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;
        v.newAnimation.transform.localScale = v.newAnimation.transform.localScale * size_multiplier;
        //Animation

        PlayAnimation(v.anim, "spell_spikePosture");

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #endregion

    #endregion

    #region Monster

    #region Bite

    public IEnumerator Anim_Bite(Sprite sp, Vector2 startPosition, float anim_speed, float anim_sizeMultiplicator)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        anim_speed *= animation_speed_brut;

        //Animation
        v.anim_2.gameObject.SetActive(true);

        v.anim.enabled = true;
        v.anim_2.enabled = true;

        v.anim_Parent.enabled = false;

        v.img_parent.transform.localScale = new Vector3(1.5f * anim_sizeMultiplicator, 1.5f * anim_sizeMultiplicator, 1 * anim_speed);

        v.img_1.sprite = Resources.Load<Sprite>("Image/Animation/monster_bite_bot");
        v.img_2.sprite = Resources.Load<Sprite>("Image/Animation/monster_bite_top");

        v.img_parent.transform.DOScale(new Vector3(1, 1, 1), 1 * anim_speed);

        v.anim.Play("bite_bot");
        v.anim_2.Play("bite_top");

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #region Flame

    public IEnumerator Anim_Flame(Sprite sp, Vector2 startPosition, float anim_speed = 1, float anim_sizeMultiplicator = 1)
    {
        //SetVariables
        (Transform img_parent, Image img_1, Image img_2, GameObject newAnimation, Animator anim,
        Animator anim_2, Animator anim_Parent) v = SetCommonValues();

        v.img_1.sprite = sp;
        v.newAnimation.transform.position = startPosition;

        anim_speed *= animation_speed_brut;

        //Animation
        v.anim.enabled = false;

        v.img_1.transform.localScale = new Vector3(animation_PopUp_startScale * anim_sizeMultiplicator, animation_PopUp_startScale * anim_sizeMultiplicator, 1);
        v.img_1.transform.DOScale(animation_PopUp_endScale * anim_sizeMultiplicator, anim_speed);

        v.img_1.transform.DOMoveY(v.img_1.transform.position.y + 0.3f, anim_speed);

        v.img_1.DOFade(0, anim_speed + 0.4f);

        Destroy(v.newAnimation, 2);

        //Obligatory code
        yield return new WaitForSeconds(0);
    }

    #endregion

    #endregion

    #endregion

    #endregion

    #endregion

    #region OLD


    public IEnumerator CreateAnimation_AttackRecoil_Individual(GameObject g, Vector3 dir)
    {
        SpriteRenderer s = g.GetComponent<SpriteRenderer>();

        g.transform.localScale = new Vector3(g.transform.localScale.x * Random.Range(70, 130 + 1) * 0.01f, g.transform.localScale.y, 0);

        g.transform.localScale *= Random.Range(70, 110 + 1) * 0.01f;

        g.transform.DOMove(g.transform.position + dir * Random.Range(-60, 60 + 1) * 0.01f * 4, 0);

        g.transform.DOMove(g.transform.position + dir * 4, Random.Range(90, 130) * 0.01f);

        s.DOFade(0, 0);

        s.DOFade(1, 0.3f);
        yield return new WaitForSeconds(0.5f);

        s.DOFade(0, 0.3f);

        Destroy(g, 1);
    }

    #endregion
}
