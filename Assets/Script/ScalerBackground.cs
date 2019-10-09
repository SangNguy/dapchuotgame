using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerBackground : MonoBehaviour
{
    int HeightScreen { get { return Screen.height; } }
    int WidthScreen { get { return Screen.width; } }
    SpriteRenderer sprite { get { return GetComponent<SpriteRenderer>(); } }


    private void Start()
    {
        Vector3 scale = transform.localScale;

        float h = sprite.bounds.size.y;
        float w = sprite.bounds.size.x;

        float worldH = Camera.main.orthographicSize * 2f;
        float worldW = worldH * WidthScreen / HeightScreen;

        scale.y = worldH / h;
        scale.x = worldW / w;

        transform.localScale = scale;
    }
}
