using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ControlMap : MonoBehaviour
{
    [SerializeField] Sprite spirteLock;
    [SerializeField] Sprite sprite0Start;
    [SerializeField] Sprite sprite1Start;
    [SerializeField] Sprite sprite2Start;
    [SerializeField] Sprite sprite3Start;
    Canvas objCanvans { get { return GameObject.FindObjectOfType<Canvas>(); } }
    GameObject[] maps;              

    private void Awake()
    {
        Database.Loaddata();
        maps = new GameObject[14];
        for(int i = 0; i < maps.Length; i++)
        {
            maps[i] = objCanvans.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < maps.Length; i++)
        {
            if (Database.Level[i] == 0)
            {
                maps[i].transform.GetChild(1).gameObject.SetActive(false);
                maps[i].transform.GetChild(0).GetComponent<Image>().sprite = spirteLock;
            }
            if (Database.Level[i] == 1)
            {
                maps[i].transform.GetChild(1).GetComponent<Button>().enabled = true;
                maps[i].transform.GetChild(0).GetComponent<Image>().sprite = sprite0Start;
            }
            if (Database.Level[i] == 2)
            {

                maps[i].transform.GetChild(1).GetComponent<Button>().enabled = true;
                maps[i].transform.GetChild(0).GetComponent<Image>().sprite = sprite1Start;
            }
            if (Database.Level[i] == 3)
            {

                maps[i].transform.GetChild(1).GetComponent<Button>().enabled = true;
                maps[i].transform.GetChild(0).GetComponent<Image>().sprite = sprite2Start;
            }
            if (Database.Level[i] == 4)
            {

                maps[i].transform.GetChild(1).GetComponent<Button>().enabled = true;
                maps[i].transform.GetChild(0).GetComponent<Image>().sprite = sprite3Start;
            }
            
        }
    }

    private void Start()
    {
       objCanvans.transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate
        {
            print("click");
            SceneManager.LoadScene(0);
        });

        EventDispatcher.Instance.RegisterListener(EventID.SelectMap, (param) =>
        {
            for (int i = 0; i < Database.Level.Length; i++)
            {
                if (maps[i].transform.GetChild(1).GetComponent<ButtonSelectMap>().Ispress)
                {
                    GameManager.LevelCurret = i+1;
                    SceneManager.LoadScene(2);
                }
            }
        });
    }
}
