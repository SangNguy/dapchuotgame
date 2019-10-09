using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSceneController : MonoBehaviour
{

    public static MainMenuController instance;
    [SerializeField]
    private AudioClip songbggame;

    [SerializeField]
    private Button MuteButton;

    public Sprite MuteImg;
    Sprite OldImg;

    public GameObject Sound;


    private void Awake()
    {

        OldImg = MuteButton.GetComponent<Image>().sprite;

    }
  
   
  

    public void MusicButton()
    {
        MuteButton.Equals(true);

    }
    int a = 0;
    public void _MuteButton2()
    {
        if (a == 0)
        {
            Sound.GetComponent<AudioSource>().mute = true;
            MuteButton.Equals(false);
            MuteButton.GetComponent<Image>().sprite = MuteImg;
            a++;
        }
        else
        {
            Sound.GetComponent<AudioSource>().mute = false;
            MuteButton.GetComponent<Image>().sprite = OldImg;
            a = 0;
        }

        // Application.LoadLevel("MainMenu");


    }

}
