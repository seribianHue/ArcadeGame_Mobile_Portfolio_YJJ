using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    RectTransform lever;
    RectTransform joystick;

    [SerializeField, Range(30, 100)]
    float leverRange = 50f;

    public Vector2 _InputDir { get { return lever.anchoredPosition / leverRange; } }

    private void Awake()
    {
        joystick = GetComponent<RectTransform>();
    }

    void ControlLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - joystick.anchoredPosition;

        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        lever.anchoredPosition = clampedDir;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlLever(eventData);
/*        Debug.Log("eventData.position = " + eventData.position);
        Debug.Log("_joystick.anchoredPosition = " + joystick.anchoredPosition);*/
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
    }

}
