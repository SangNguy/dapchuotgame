using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIControl : MonoBehaviour
{
    #region Fields
    //  ------------------
    GameObject objTarget { get { return GameObject.FindObjectOfType<Canvas>().transform.Find("PanelTarget").gameObject; } }
    GameObject objInformation { get { return GameObject.FindObjectOfType<Canvas>().transform.Find("PanelInfomation").gameObject; } }
    GameObject objPausegame { get { return GameObject.FindObjectOfType<Canvas>().transform.Find("PanelPause").gameObject; } }
    GameObject objOvergame { get { return GameObject.FindObjectOfType<Canvas>().transform.Find("PanelOverGame").gameObject; } }
    GameObject objStargame { get { return GameObject.FindObjectOfType<Canvas>().transform.Find("PanelStartGame").gameObject; } }
    Button buttonPause { get { return GameObject.FindObjectOfType<Canvas>().transform.Find("ButtonPause").GetComponent<Button>(); } }
    Text txtCombo { get { return GameObject.FindObjectOfType<Canvas>().transform.Find("TextCombo").GetComponent<Text>(); } }
    //-----------------
    GameManager gameManager;
    EventDispatcher eventDispatcher;
    HammerControl hammerControl;
    //-----------------
    float oldmanHiited = 3;

    #endregion
    private void Start()
    {
        SetUpBeforeStart();
        //--------------------
        eventDispatcher = EventDispatcher.Instance;
        gameManager = GameManager.Instance;
        hammerControl = GameObject.FindObjectOfType<HammerControl>();
        //--------------------
        SetMouseHited(gameManager.SumMouseHited);
        SetCombo(gameManager.SumCombo);
        SetDiamondHited(gameManager.SumDiamond);
        //--------------------
        ShowPanelTarget();
        //--------------------
        ControlPanelOvergame();
        ControlPanelInformation();
        ControlPanelPause();
        //--------------------
        buttonPause.onClick.AddListener(delegate
        {
            hammerControl.HideHammer();
            objPausegame.SetActive(true);
            buttonPause.gameObject.SetActive(false);
            Time.timeScale = 0;
        });
        //--------------------
        eventDispatcher.RegisterListener(EventID.HitOldman, (param) =>
        {
            oldmanHiited -= 1;
            if (oldmanHiited == 0)
            {
                gameManager.isWin = false;
                eventDispatcher.PostEvent(EventID.OverGame);
            }
        });
        //------------------
        eventDispatcher.RegisterListener(EventID.HitCombo, (param) =>
        {
            if (gameManager.isCombo)
            {
                txtCombo.text = "Combo x" + gameManager.SumCombo;
                txtCombo.enabled = true;
                StartCoroutine(HideCombo());
            }
        });
    }

    IEnumerator HideCombo()
    {
        yield return new WaitForSeconds(0.5f);
        txtCombo.enabled = false;
    }

    #region ShowPanelTarget Complete
    void ShowPanelTarget()
    {
        hammerControl.HideHammer();
        objTarget.transform.Find("ButtonBack").GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene(1);
        });

        objTarget.transform.Find("ButtonStart").GetComponent<Button>().onClick.AddListener(delegate
        {
            StartCoroutine(StartRunTimeBeforePlay());
            buttonPause.gameObject.SetActive(true);
            objTarget.SetActive(false);
            hammerControl.DisplayHammer();
        });

        objTarget.transform.Find("TextTargetMouse").GetComponent<Text>().text = "" + Database.Target(GameManager.LevelCurret).x;
        objTarget.transform.Find("TextTargetCombo").GetComponent<Text>().text = "" + Database.Target(GameManager.LevelCurret).y;
        objTarget.transform.Find("TextTargetDiamond").GetComponent<Text>().text = "" + Database.Target(GameManager.LevelCurret).z;
    }

    IEnumerator StartRunTimeBeforePlay()
    {
        objStargame.SetActive(true);
        GameObject _0 = objStargame.transform.GetChild(0).gameObject;
        GameObject _1 = objStargame.transform.GetChild(1).gameObject;
        GameObject _2 = objStargame.transform.GetChild(2).gameObject;
        GameObject _3 = objStargame.transform.GetChild(3).gameObject;
      




        //dem 3 2 1 ready 

        yield return new WaitForSeconds(1);
        _3.SetActive(true);
        gameManager.isStartgame = 4;
       eventDispatcher.PostEvent(EventID.StartGame);

        yield return new WaitForSeconds(1);
        _3.SetActive(false);
        _2.SetActive(true);
        gameManager.isStartgame = 3;
       eventDispatcher.PostEvent(EventID.StartGame);

        yield return new WaitForSeconds(1);
        _2.SetActive(false);
        _1.SetActive(true);
        gameManager.isStartgame = 2;
       eventDispatcher.PostEvent(EventID.StartGame);

        yield return new WaitForSeconds(1);
        _1.SetActive(false);
        _0.SetActive(true);
        gameManager.isStartgame = 1;
       eventDispatcher.PostEvent(EventID.StartGame);

        yield return new WaitForSeconds(1);
        _0.SetActive(false);
        objStargame.SetActive(false);
        objInformation.SetActive(true);
        gameManager.isStartgame = 0;
       eventDispatcher.PostEvent(EventID.StartGame);
    }
    #endregion

    void ScoreGameOver(int hitedMouse, int hitedGrandfather, int hitedDiamond, int combo, int star)
    {
        objOvergame.transform.Find("HitedMouse").GetChild(0).GetComponent<Text>().text = "" + Database.Target(GameManager.LevelCurret).x;
        objOvergame.transform.Find("HitedGrandfather").GetChild(0).GetComponent<Text>().text = "" + hitedGrandfather;
        objOvergame.transform.Find("HitedDiamond").GetChild(0).GetComponent<Text>().text = "" + hitedDiamond;
        objOvergame.transform.Find("Combo").GetChild(0).GetComponent<Text>().text = "" + combo;

        switch(star)
        {
            case 3: objOvergame.transform.Find("StartImage").GetComponent<Image>().sprite = ResourcesGame.Instance.sprite3Star; break;
            case 2: objOvergame.transform.Find("StartImage").GetComponent<Image>().sprite = ResourcesGame.Instance.sprite2Star; break;
            case 1: objOvergame.transform.Find("StartImage").GetComponent<Image>().sprite = ResourcesGame.Instance.sprite1Star; break;
            default: objOvergame.transform.Find("StartImage").GetComponent<Image>().sprite = ResourcesGame.Instance.sprite0Star; break;
        }
    }

    void ControlPanelOvergame()
    {
        objOvergame.transform.Find("ButtonHome").GetComponent<Button>().onClick.AddListener(delegate 
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        });
        objOvergame.transform.Find("ButtonReset").GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene(2);
            objOvergame.transform.Find("ButtonNext").gameObject.SetActive(true);
            Time.timeScale = 1;
        });

        objOvergame.transform.Find("ButtonNext").GetComponent<Button>().onClick.AddListener(delegate
        {
            gameManager.NextMap();
            SceneManager.LoadScene(2);
            objOvergame.transform.Find("ButtonNext").gameObject.SetActive(true);
            Time.timeScale = 1;
        });

       eventDispatcher.RegisterListener(EventID.OverGame, (param) =>
        {
            StartCoroutine(OverGameRuntime());
        });
    }

    IEnumerator OverGameRuntime()
    {
        yield return new WaitForSeconds(0.3f);
        hammerControl.HideHammer();
        txtCombo.enabled = false;
        Time.timeScale = 0;
        gameManager.Star = 0;
        if (gameManager.isWin)
        {
            objOvergame.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ResourcesGame.Instance.spriteWin;
            if (gameManager.SumDiamond >= Database.Target(GameManager.LevelCurret).z) { gameManager.Star += 1; }
            if (gameManager.MaxCombo >= Database.Target(GameManager.LevelCurret).y) { gameManager.Star += 1; }
            if (gameManager.SumMouseHited <= 0) { gameManager.Star += 1; }
            gameManager.WinGame();
        }
        else
        {
            objOvergame.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ResourcesGame.Instance.spriteLose;
            objOvergame.transform.Find("ButtonNext").gameObject.SetActive(false);
            print("code lose");
        }

        objOvergame.SetActive(true);
        ScoreGameOver(gameManager.SumMouseHited, gameManager.SumGrandfatherHited, gameManager.SumDiamond, gameManager.MaxCombo, gameManager.Star);
    }

    void ControlPanelPause()
    {
        objPausegame.transform.Find("ButtonHome").GetComponent<Button>().onClick.AddListener(delegate 
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        });

        objPausegame.transform.Find("ButtonReset").GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadScene(2);
            Time.timeScale = 1;
        });

        objPausegame.transform.Find("ButtonResume").GetComponent<Button>().onClick.AddListener(delegate
        {
            objPausegame.SetActive(false);
            buttonPause.gameObject.SetActive(true);
            Time.timeScale = 1;
            hammerControl.DisplayHammer();
        });
    }

    void ControlPanelInformation()
    {
       eventDispatcher.RegisterListener(EventID.HitTheMouse, (param) =>
        {
            gameManager.SumMouseHited -= 1;
            SetMouseHited(gameManager.SumMouseHited);
            if (gameManager.SumMouseHited == 0)
            {
                if (gameManager.SumCombo >= Database.Target(GameManager.LevelCurret).y && gameManager.SumDiamond >= Database.Target(GameManager.LevelCurret).z)
                {
                    gameManager.isWin = true;
                   eventDispatcher.PostEvent(EventID.OverGame);
                }
                else
                {
                    gameManager.isWin = false;
                   eventDispatcher.PostEvent(EventID.OverGame);
                }
            }
        });

       eventDispatcher.RegisterListener(EventID.HitTheDiamond, (param) =>
        {
            gameManager.SumDiamond += 1;
            SetDiamondHited(gameManager.SumDiamond);
        });

       eventDispatcher.RegisterListener(EventID.HitOldman, (param) =>
        {
            gameManager.SumGrandfatherHited += 1;
        });

       eventDispatcher.RegisterListener(EventID.HitCombo, (param) =>
        {
            if (!gameManager.isCombo)
            {
                gameManager.SumCombo = 0;
                gameManager.SumMiss += 1;
            }
            else
            {
                gameManager.SumCombo += 1;
                if (gameManager.SumCombo > gameManager.MaxCombo)
                    gameManager.MaxCombo = gameManager.SumCombo;
            }
            SetCombo(gameManager.SumCombo);
        });
    }

    #region Setup
    void SetMouseHited(int mouseHited)
    {
        objInformation.transform.Find("SumTheHited").transform.GetChild(0).GetComponent<Text>().text = "x" + mouseHited;
    }
    void SetDiamondHited(int diamondHited)
    {
        objInformation.transform.Find("SumDiamond").transform.GetChild(0).GetComponent<Text>().text = "" + diamondHited;
    }
    void SetCombo(int combo)
    {
        objInformation.transform.Find("SumCombo").transform.GetChild(0).GetComponent<Text>().text = "x" + combo;
    }

    void SetUpBeforeStart()
    {
        objStargame.transform.GetChild(0).gameObject.SetActive(false);
        objStargame.transform.GetChild(1).gameObject.SetActive(false);
        objStargame.transform.GetChild(2).gameObject.SetActive(false);
        objStargame.transform.GetChild(3).gameObject.SetActive(false);
        objStargame.SetActive(false);
        //-----------------
        objPausegame.SetActive(false);
        objOvergame.SetActive(false);
        //------------------
        buttonPause.gameObject.SetActive(false);
        objInformation.SetActive(false);
        //-----------------
        objTarget.SetActive(true);
    }
    #endregion
}
