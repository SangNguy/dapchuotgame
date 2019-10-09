using UnityEngine;
using System.Collections;

public class TestAimation : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Active());
    }
    IEnumerator Active()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator>().SetInteger("Action", 1);
            StartCoroutine(HideObject(0.2f));
        }
        if(Input.GetKey(KeyCode.D))
        {
            GetComponent<Animator>().SetInteger("Action", 2);
            print("parameter: " +
            GetComponent<Animator>().recorderStopTime);
            print("" + GetComponent<Animator>().recorderStopTime);
        }
    }

    IEnumerator HideObject(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
