using UnityEngine;
using UnityEngine.UI;

public class TitleState : State
{
	public Button startButton;

	void Awake()
	{
		SetActionToButton(startButton, OnStart);
	}

	void OnStart()
	{
		SimpleStateMachine.instance.SetState<LoadingState>();
	}
}