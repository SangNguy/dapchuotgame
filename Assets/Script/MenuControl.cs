using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [SerializeField] Button buttonStart;

    private void Awake()
    {
        GetComponent<Animator>().SetBool("Start", true);
    }

    private void Start()
    {
        //buttonStart.onClick.AddListener(delegate
        //{
        //    SceneManager.LoadScene(2);
        //});
    }
}
