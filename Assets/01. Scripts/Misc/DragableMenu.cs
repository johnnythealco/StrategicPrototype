using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragableMenu : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static GameObject itemBeingDragged;
	Vector3 startposition;
	Vector3 offset;


	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startposition = transform.position;
		offset = startposition - Input.mousePosition;
	}

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition + offset;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
//		transform.position = startposition;
	}




}
