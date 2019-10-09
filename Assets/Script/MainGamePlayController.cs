using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private Button BackButton,PlayButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void playButton()
    {
        Application.LoadLevel("MainMenu");
    }
    public void backButton()
    {
        Application.LoadLevel("Mainmenu");
    }
}
