using UnityEngine;
using System.Collections;

public class Grow : BaseMonoBehaviour
{
    //---------------
    GameManager gameManager;
    //---------------
    public string Tag_Mouse = "Mouse";
    public string Tag_Oldman = "Oldman";
    public string Tag_Diamond = "Diamond";
    //---------------
    Animator anim;
    CircleCollider2D circleCollider2D;
    //---------------
    float timeExists = Database.TimeHole(GameManager.LevelCurret - 1).y;
    //---------------

    public void StartGrow()
    {
        gameManager = GameManager.Instance;
        anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = true;
        StartCoroutine(HideObjectWhenHitMiss());
    }

    #region Hide Object When Hit Miss
    IEnumerator HideObjectWhenHitMiss()
    {
        // time out show then change animation hide 
        yield return new WaitForSeconds(timeExists);

        if (gameObject.tag== Tag_Mouse)
        {
            gameManager.isCombo = false;
            EventDispatcher.Instance.PostEvent(EventID.HitCombo);
        }

        yield return new WaitForSeconds(GetTimeObjHideWhenHitMiss());
        gameObject.SetActive(false);
    }

    float GetTimeObjHideWhenHitMiss()
    {
        anim.SetInteger(gameObject.tag, 1);
        circleCollider2D.enabled = false;
        return anim.runtimeAnimatorController.animationClips[1].length;
    }

    #endregion

    #region Hide OBject When Hitted
    IEnumerator HideObjectWhenHitted()
    {
        if (gameObject.tag == Tag_Diamond)
            StartCoroutine(DiamondMoveToPanelInformation());
        else
        {
            yield return new WaitForSeconds(GetTimeObjHideWithHitted());
            gameObject.SetActive(false);
        }
    }

    float GetTimeObjHideWithHitted()
    {
        return anim.runtimeAnimatorController.animationClips[2].length;
    }

    IEnumerator DiamondMoveToPanelInformation()
    {
        // -7/-0.5
        yield return null;
        Vector3 nor = new Vector3(-9, -0.5f, 0) - gameObject.transform.position;
        gameObject.transform.localScale = nor.magnitude / 10f * Vector3.one;
        gameObject.transform.Translate(nor * 3 * Time.deltaTime);
        if (gameObject.transform.position.x < -7)
        {
            StopCoroutine(DiamondMoveToPanelInformation());
            gameObject.SetActive(false);
        }
        else StartCoroutine(DiamondMoveToPanelInformation());
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        circleCollider2D.enabled = false;
        if(gameObject.tag == Tag_Mouse)
        {
            EventDispatcher.Instance.PostEvent(EventID.HitTheMouse);
            gameManager.isCombo = true;
            EventDispatcher.Instance.PostEvent(EventID.HitCombo);
        }
        if (gameObject.tag == Tag_Oldman)
        {
            EventDispatcher.Instance.PostEvent(EventID.HitOldman);
        }
        if (gameObject.tag == Tag_Diamond)
        {
            EventDispatcher.Instance.PostEvent(EventID.HitTheDiamond);
        }
        
        anim.SetInteger(gameObject.tag, 2);
        StopAllCoroutines();
        StartCoroutine(HideObjectWhenHitted());
    }
}

