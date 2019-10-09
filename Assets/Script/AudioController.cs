using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    #region Fields
    AudioClip audioclipGround { get { return Resources.Load<AudioClip>("Audio/songbggame"); } }
    AudioClip audioclipHit { get { return Resources.Load<AudioClip>("Audio/sound_mouse_washit"); } }
    AudioClip audioclipMiss { get { return Resources.Load<AudioClip>("Audio/sound_hammer_hitmiss"); } }
    AudioClip audioclipDiamond { get { return Resources.Load<AudioClip>("Audio/sound_diamond_washit"); } }
    AudioClip audioclipGrandfather { get { return Resources.Load<AudioClip>("Audio/boomsound"); } }
    AudioClip audioclipWin { get { return Resources.Load<AudioClip>("Audio/sound_wingame"); } }
    AudioClip audioclipReady { get { return Resources.Load<AudioClip>("Audio/sound_ready"); } }
    AudioClip audioclip321 { get { return Resources.Load<AudioClip>("Audio/sound_123"); } }

    AudioSource audioGround { get { return  transform.GetComponent<AudioSource>(); } }
    AudioSource audioEffect { get { return transform.GetChild(0).GetComponent<AudioSource>(); } }

    GameManager gameManager;
    EventDispatcher eventDispatcher;

    #endregion

    private void Start()
    {
        gameManager = GameManager.Instance;
        eventDispatcher = EventDispatcher.Instance;
        //-------------------
        audioGround.clip = audioclipGround;
        audioGround.loop = true;
        audioGround.Stop();
        audioEffect.Stop();
        //-------------------
        ManageEventSound();
    }

    void ManageEventSound()
    {
        eventDispatcher.RegisterListener(EventID.HitTheMouse, (param) => { audioEffect.PlayOneShot(audioclipHit); });
        eventDispatcher.RegisterListener(EventID.HitOldman, (param) => { audioEffect.PlayOneShot(audioclipGrandfather); });
        eventDispatcher.RegisterListener(EventID.HitTheDiamond, (param) => { audioEffect.PlayOneShot(audioclipDiamond); });
        eventDispatcher.RegisterListener(EventID.HitCombo, (param) => 
        {
            if (!gameManager.isCombo) audioEffect.PlayOneShot(audioclipMiss); 
        });

        eventDispatcher.RegisterListener(EventID.StartGame, (param) => 
        {
            if (gameManager.isStartgame == 0)
                audioGround.Play();
            else if(gameManager.isStartgame == 1)
                audioEffect.PlayOneShot(audioclipReady);
            else
                audioEffect.PlayOneShot(audioclip321);
        });

        eventDispatcher.RegisterListener(EventID.OverGame, (param) =>
        {
            audioGround.Stop();
            if (gameManager.isWin) audioEffect.PlayOneShot(audioclipWin);
        });
    }
}
