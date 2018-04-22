using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleStateMachine : MonoBehaviour
{
	public static SimpleStateMachine instance;
	public State[] states;

	public State currentState
	{
		get
		{
			return statesStack.Peek();
		}
	}

	Canvas canvas;
	Stack<State> statesStack = new Stack<State>();

	void Awake()
	{
		instance = this;
		canvas = GameObject.FindObjectOfType<Canvas>();

		SetState<TitleState>();
	}

	public void SetState<T>() where T : State
	{
		PopState();
		PushState<T>();
	}

	public void PushState<T>() where T : State
	{
		State state = CreateStateInstance<T>();
		statesStack.Push(state);
	}

	public void PopState()
	{
		if (statesStack.Count() > 0)
		{
			State state = statesStack.Pop();
			GameObject.Destroy(state.gameObject);
		}
	}

	public State CreateStateInstance<T>() where T : State
	{
		State prefab = states.FirstOrDefault(s => s is T);
		if (prefab == null)
		{
			Debug.LogError("No prefab found for state: " + typeof(T).Name);
			return null;
		}
		else
		{
			GameObject spawnedState = GameObject.Instantiate(prefab.gameObject, canvas.transform, false) as GameObject;
			return spawnedState.GetComponent<State>();
		}
	}
}