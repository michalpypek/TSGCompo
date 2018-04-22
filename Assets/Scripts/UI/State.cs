using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class State : MonoBehaviour
{

	protected void SetActionToButton(Button button, Action action)
	{
		if (button != null && action != null)
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => {
				action();
			});
		}
	}
}