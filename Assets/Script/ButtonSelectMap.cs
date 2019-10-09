using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonSelectMap : MonoBehaviour, IPointerClickHandler
{
    public bool Ispress;

    public void OnPointerClick(PointerEventData eventData)
    {
        Ispress = true;
        EventDispatcher.Instance.PostEvent(EventID.SelectMap);
    }
}
