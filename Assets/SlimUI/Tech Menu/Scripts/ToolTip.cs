using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
 {
	 private RectTransform _toolTip;

	 void Update()
     {
		 _toolTip.anchoredPosition = Input.mousePosition;
	 }
 }