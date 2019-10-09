using UnityEngine;
using System.Collections;

public class GameManager : BaseMonoBehaviour
{
    #region Singleton
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObj = new GameObject();
                singletonObj.name = "GameManager";
                instance = singletonObj.AddComponent<GameManager>();
            }
            return instance;
        }
        private set { }
    }
    void Awake()
    {
        if (instance == null || instance.GetInstanceID() == this.GetInstanceID())
            instance = this;
    }
    #endregion

    static int levelMax;
    public static int LevelCurret = 1;
    //------------------
    public int isStartgame = 14;
    //------------------
    public bool isWin = false;
    public int Star = 0;
    //------------------
    public bool isCombo = false;
    public int SumCombo = 0;
    public int SumMouseHited;
    public int MaxCombo = 0;
    public int SumDiamond = 0;
    public int SumMiss = 0;
    public int SumGrandfatherHited = 0;

    protected override void OnEnable()
    {
        //--------------------------
        Database.Loaddata();
        for(int i = 0; i < Database.Level.Length; i++)
        {
            print(Database.Level[i]);
            if (Database.Level[i] != 0)
                levelMax = i;
        }
        print("levelmax: " + levelMax + " ://: " + Database.Level[levelMax]);
        print("levelcurret: " + LevelCurret);
        // ------------------------
    SumMouseHited = Database.Target(LevelCurret).x;
        //--------------------------
        GameObject audio = new GameObject();
        audio.transform.parent = transform;
        audio.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioController>();

        //--------------------------
        gameObject.AddComponent<UIControl>();
        gameObject.AddComponent<GrowManager>();
        gameObject.AddComponent<AudioSource>();
        //----------------------
    }

    public void WinGame()
    {
        print("levelMax: " + levelMax);
        print("levelcurret: " + LevelCurret);
        print("star: " + Star);
        if (LevelCurret > levelMax)
        {
            levelMax++;
            Database.Level[levelMax] = 1;
        }
        if (Star > Database.Level[LevelCurret] - 1) Database.Level[LevelCurret-1] = Star + 1;
        Database.Savedata();
    }

    public void NextMap()
    {
        LevelCurret++;
    }
}
