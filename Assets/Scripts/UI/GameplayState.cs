using UnityEngine;
using UnityEngine.UI;

public class GameplayState : State
{
	public static GameObject player;
	public Button pauseButton;

	void Awake()
	{
		//player = FindObjectOfType<Player>().gameObject;
		//SetActionToButton(pauseButton, OnPause);
		Time.timeScale = 1f;
	}

	void OnPause()
	{
		//SimpleStateMachine.instance.SetState<PauseState>();
	}
}