using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class EventTest : MonoBehaviour, IPointerClickHandler
    , IPointerDownHandler
    , IPointerUpHandler
, IPointerEnterHandler
, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(name + "=> OnPointerClick" + eventData.button);
        //if (eventData.button != PointerEventData.InputButton.Left)
        //{

        //}
        //throw new System.NotImplementedException();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name + "=> OnPointerDown" + eventData.button);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(name + "=> OnPointerUp" + eventData.button);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(name + "=> OnPointerEnter" + eventData.button);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log(name + "=> OnPointerExit" + eventData.button);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }


}
