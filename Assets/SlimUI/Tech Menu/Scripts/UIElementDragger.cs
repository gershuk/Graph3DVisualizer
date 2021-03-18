using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
// Fromt Unity Drag and Drop Tutorial - How TO Drag and Drop UI Elements in Unity

public class UIElementDragger : MonoBehaviour {

	public const string DRAGGABLE_TAG = "UIDrag";
	private bool _dragging = false;
	private Vector2 _originalPosition;
	private Transform _objectToDrag;
	private Image _objecttoDragImage;
    readonly List<RaycastResult> _hitObjects = new List<RaycastResult>();

	#region Monobehavior API

	void LateUpdate(){
		if(Input.GetButtonDown("Fire")){
			_objectToDrag = GetDraggableTransformUnderMouse();

			if(_objectToDrag != null){
				_dragging = true;

				_originalPosition = _objectToDrag.position;
				_objecttoDragImage = _objectToDrag.GetComponent<Image>();
				_objecttoDragImage.raycastTarget = false;
			}
		}

		if(_dragging){
			_objectToDrag.position = Input.mousePosition;
		}

		if(Input.GetButtonUp("Fire") && _dragging){
			_dragging = false;
			_objecttoDragImage.raycastTarget = true;
		}
	}

	#endregion

	private GameObject GetObjectUnderMouse(){
		var pointer = new PointerEventData(EventSystem.current);

		pointer.position = Input.mousePosition;
		EventSystem.current.RaycastAll(pointer,_hitObjects);

		if(_hitObjects.Count <= 0) return null;

		return _hitObjects.First().gameObject;
	}

	private Transform GetDraggableTransformUnderMouse(){
		GameObject clickedObject = GetObjectUnderMouse();

		if(clickedObject != null && clickedObject.tag == DRAGGABLE_TAG){
			return clickedObject.transform;
		}

		return null;
	}
}
