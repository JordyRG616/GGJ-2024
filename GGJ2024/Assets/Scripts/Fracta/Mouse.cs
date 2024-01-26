using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Mouse
{
    private static Vector2 resolution = new Vector2(1280, 720);


    public static Vector2 Position
    {
        get
        {
            var cam = Camera.main;
            Vector2 pos = cam.ScreenToViewportPoint(Input.mousePosition);

            pos -= Vector2.one / 2;
            pos.x *= resolution.x;
            pos.y *= resolution.y;

            return pos;
        }
    }

    public static bool FindUnder<T>(PointerEventData eventData, out T result)
    {
        result = default;
        var hits = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, hits);

        foreach (var hit in hits)
        {
            if(hit.gameObject.TryGetComponent<T>(out var t))
            {
                result = t;
                return true;
            }
        }

        return false;
    }

    public static bool FindUnder<T>(PointerEventData eventData, GameObject caster, out T result)
    {
        result = default;
        var hits = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, hits);

        foreach (var hit in hits)
        {
            if (hit.gameObject == caster) continue;

            if (hit.gameObject.TryGetComponent<T>(out var t))
            {
                result = t;
                return true;
            }
        }

        return false;
    }
}
