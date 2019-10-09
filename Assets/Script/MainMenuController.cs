using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public static MainMenuController instance;
    [SerializeField]
    private AudioClip songbggame;
    //[SerializeField]
    //private GameObject infoPanel, helpPanel;
    [SerializeField]
    private Button MuteButton;

    public Sprite MuteImg;
    Sprite OldImg;

    public GameObject MainCam;
    

    private void Awake()
    {
        
        OldImg = MuteButton.GetComponent<Image>().sprite;
        //infoPanel.transform.parent.gameObject.SetActive(false);
        ////helpPanel.transform.parent.gameObject.SetActive(false);
        //infoPanel.SetActive(false);
        //helpPanel.SetActive(false);
    }
    public void _PlayButton()
    {
        Application.LoadLevel("Map");
    }
    //public void InfoButton()
    //{
    //    infoPanel.transform.parent.gameObject.SetActive(true);
    //    infoPanel.SetActive(true);
    //}
    //public void HelpButton()
    //{
    //    helpPanel.transform.parent.gameObject.SetActive(true);
    //    helpPanel.SetActive(true);
    //}
    public void BackButton()
    {
        Application.LoadLevel("MenuGame");
        //Application.LoadLevel(Application.loadedLevel);
    }

    public void MusicButton()
    {
        MuteButton.Equals(true);
        
    }
    int a = 0;
    public void _MuteButton()
    {
        if(a == 0)
        {
            MainCam.GetComponent<AudioSource>().mute = true;
            MuteButton.Equals(false);
            MuteButton.GetComponent<Image>().sprite = MuteImg;
            a++;
        }
        else
        {
            MainCam.GetComponent<AudioSource>().mute = false;
            MuteButton.GetComponent<Image>().sprite = OldImg;
            a = 0;
        }
        
       // Application.LoadLevel("MainMenu");


    }
 
}
