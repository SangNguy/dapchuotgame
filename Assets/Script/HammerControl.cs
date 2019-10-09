using UnityEngine;
using System.Collections;

public class HammerControl : MonoBehaviour
{
    private void Awake()
    {
        spriteHammer = GetComponent<SpriteRenderer>();
        spriteHammer.enabled = false;

        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;

        anim = GetComponent<Animator>();
        SetAnimation(false);
        time_animationClickHammer = anim.runtimeAnimatorController.animationClips[0].length;
    }

    float time_animationClickHammer;
    Animator anim;
    CircleCollider2D circleCollider;
    SpriteRenderer spriteHammer;

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        mousePos.y -= 0.5f; 
        transform.position = mousePos;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            spriteHammer.enabled = true;
            SetAnimation(true);
            StartCoroutine(TurnOffAnimation());
            circleCollider.enabled = true;
        }
    }

    IEnumerator TurnOffAnimation()
    {
        yield return new WaitForSeconds(time_animationClickHammer);
        circleCollider.enabled = false;
        SetAnimation(false);
        spriteHammer.enabled = false;
    }

    void SetAnimation(bool isActive)
    {
        anim.SetBool(Animator.StringToHash("Hit"), isActive);
    }

    public void HideHammer()
    {
        gameObject.SetActive(false);
    }

    public void DisplayHammer()
    {
        gameObject.SetActive(true);
    }
}
