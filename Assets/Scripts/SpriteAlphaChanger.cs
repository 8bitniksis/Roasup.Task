using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAlphaChanger : MonoBehaviour
{
    public Image _sprite = null;

    public void UpdateAlpha(bool condition) 
    {
        var color = _sprite.color;

        if (condition) 
            color.a += 2.0f * Time.deltaTime;
        else
            color.a = 0; 

        // Не даем значению выйти за границы, для цвета это (0, 1).
        color.a = Mathf.Clamp(color.a, 0, 1); 

        // Задаем спрайту новый цвет.
        _sprite.color = color; 
    }
}
