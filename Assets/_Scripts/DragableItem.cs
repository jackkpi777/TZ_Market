using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    [HideInInspector]public Transform parentAfterDrag;

    private Image image;
    public int cost;
    public int index;
    public string Owner;

    private void Start()
    {
        image = GetComponent<Image>();
        transform.GetChild(0).GetComponent<Text>().text = cost.ToString();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
#if UNITY_EDITOR
        transform.position = Input.mousePosition;
#endif
#if PLATFORM_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                transform.position = Input.GetTouch(0).position;
            }
        }

#endif
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        
    }
   
      
  
}
