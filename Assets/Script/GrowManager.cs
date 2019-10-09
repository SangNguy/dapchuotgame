using UnityEngine;
using System.Collections;

public class GrowManager : BaseMonoBehaviour
{
    GameObject objHole;
    GameObject[] Holes;
    GameManager gameManager;
    GameObject[] objCurret;
    float timeLoader { get { return Database.TimeHole(GameManager.LevelCurret - 1).x; } }
    float timeHide { get { return Database.TimeHole(GameManager.LevelCurret - 1).y; } }

    protected override void Awake()
    {
        objHole = Resources.Load("Hole") as GameObject;
        Holes = new GameObject[Database.PositionHoles.Length];
        for (int i = 0; i < Holes.Length; i++)
        {
            GameObject hole = Instantiate(objHole);
            hole.transform.localScale = Vector3.one * 0.7f; //dieu chinh do lon cua hole
            hole.transform.position = Database.PositionHoles[i];
            hole.SetActive(true);
            for (int j = 0; j < hole.transform.childCount; j++)
            {
                hole.transform.GetChild(j).gameObject.SetActive(false);
            }
            Holes[i] = hole;
        }
    }

    protected override void Start()
    {
        if (GameManager.LevelCurret < 2) objCurret = new GameObject[1];
        else if (GameManager.LevelCurret < 14) objCurret = new GameObject[2]; // dieu chinh so luong level map 
        else objCurret = new GameObject[3];

        gameManager = GameManager.Instance;
        EventDispatcher.Instance.RegisterListener(EventID.StartGame, (param) =>
        {
            if(gameManager.isStartgame == 0)
                StartCoroutine(ObjGrow());
        });
    }

    /// <summary>
    /// thiet lap cho cac gameobj hien len
    /// </summary>
    /// <returns></returns>
    IEnumerator ObjGrow()
    {
        yield return new WaitForSeconds(timeLoader);

        if(GameManager.LevelCurret < 2)
        {
            objCurret[0]= Holes[GetHole].transform.GetChild(GetObject).gameObject;
            if(GetObject == 2)
            {
                objCurret[0].transform.localPosition = new Vector2(-0.12f, 1.06f);
                objCurret[0].transform.localScale = Vector3.one;
            }
            objCurret[0].GetComponent<CircleCollider2D>().enabled = true;
            objCurret[0].SetActive(true);
            objCurret[0].GetComponent<Grow>().StartGrow();
        }
        else
        {
            objCurret[0] = Holes[GetHole].transform.GetChild(GetObject).gameObject;
            if (GetObject == 2)
            {
                objCurret[0].transform.localPosition = new Vector2(-0.12f, 1.06f);
                objCurret[0].transform.localScale = Vector3.one;
            }
            objCurret[0].GetComponent<CircleCollider2D>().enabled = true;
            objCurret[0].SetActive(true);
            objCurret[0].GetComponent<Grow>().StartGrow();

            yield return new WaitForSeconds(timeHide / 1.5f);

            objCurret[1] = Holes[GetHole].transform.GetChild(GetObject).gameObject;
            if (GetObject == 2)
            {
                objCurret[1].transform.localPosition = new Vector2(-0.12f, 1.06f);
                objCurret[1].transform.localScale = Vector3.one;
            }
            objCurret[1].GetComponent<CircleCollider2D>().enabled = true;
            objCurret[1].SetActive(true);
            objCurret[1].GetComponent<Grow>().StartGrow();
        }
        //else
        //{
        //    objCurret[0] = Holes[GetHole].transform.GetChild(GetObject).gameObject;
        //    if (GetObject == 2)
        //    {
        //        objCurret[0].transform.localPosition = new Vector2(-0.12f, 1.06f);
        //        objCurret[0].transform.localScale = Vector3.one;
        //    }
        //    objCurret[0].GetComponent<CircleCollider2D>().enabled = true;
        //    objCurret[0].SetActive(true);
        //    objCurret[0].GetComponent<Grow>().StartGrow();

        //    yield return new WaitForSeconds(timeHide / 4);

        //    objCurret[1] = Holes[GetHole].transform.GetChild(GetObject).gameObject;
        //    if (GetObject == 2)
        //    {
        //        objCurret[1].transform.localPosition = new Vector2(-0.12f, 1.06f);
        //        objCurret[1].transform.localScale = Vector3.one;
        //    }
        //    objCurret[1].GetComponent<CircleCollider2D>().enabled = true;
        //    objCurret[1].SetActive(true);
        //    objCurret[1].GetComponent<Grow>().StartGrow();

        //    yield return new WaitForSeconds(timeHide / 4);

        //    objCurret[2] = Holes[GetHole].transform.GetChild(GetObject).gameObject;
        //    if (GetObject == 2)
        //    {
        //        objCurret[2].transform.localPosition = new Vector2(-0.12f, 1.06f);
        //        objCurret[2].transform.localScale = Vector3.one;
        //    }
        //    objCurret[2].GetComponent<CircleCollider2D>().enabled = true;
        //    objCurret[2].SetActive(true);
        //    objCurret[2].GetComponent<Grow>().StartGrow();
        //}
        StartCoroutine(ObjGrow());
    }

    int GetObject
    {
        get
        {
            if (GameManager.LevelCurret == 1) return 0;
            if(GameManager. LevelCurret == 2)
            {
                int rand = Random.Range(0, 10);
                if (rand >= 8) return 1;
                else return 0;
            }
            else if (GameManager.LevelCurret == 3)
            {
                int rand = Random.Range(0, 10);
                if (rand >= 7) return 1;
                else return 0;
            }
            else if (GameManager.LevelCurret == 4)
            {
                int rand = Random.Range(0, 10);
                if (rand >= 7) return 1;
                else return 0;
            }
            else if(GameManager.LevelCurret >= 5)
            {
                int rand = Random.Range(0, 10);
                if (rand >= 8) return 2;   // diamond 10%)
                else if (rand >= 5) return 1; // old man 30%
                return 0;
            }
            return -1;
        }
    }

    int GetHole
    {
        get
        {
            bool isExist = true;
            int rand = 0;
            do
            {
                rand = Random.Range(0, Holes.Length);
                int count = 0;
                for (int i = 0; i < Holes[rand].transform.childCount; i++)
                {
                    if (Holes[rand].transform.GetChild(i).gameObject.activeInHierarchy)
                        count++;
                }
                if (count == 0) isExist = false;
            }
            while (isExist);
            return rand;
        }
    }
}
