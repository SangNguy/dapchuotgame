using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourcesGame : MonoBehaviour
{
    #region Singleton
    static ResourcesGame instance;
    public static ResourcesGame Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObj = new GameObject();
                singletonObj.name = "ResourcesGame";
                instance = singletonObj.AddComponent<ResourcesGame>();
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

    public Sprite sprite0Star;
    public Sprite sprite1Star;
    public Sprite sprite2Star;
    public Sprite sprite3Star;

    public Sprite spriteWin;
    public Sprite spriteLose;
}
