
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingState : State
{
	const float MIN_WAIT_TIME = 3f;

	AsyncOperation loading;
	float timer;

	void Awake()
	{
		loading = SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Additive);
		//timer = MIN_WAIT_TIME;
	}

	void Update()
	{
		if (loading != null && loading.isDone)
		{
			SimpleStateMachine.instance.SetState<GameplayState>();
			loading = null;
		}
		//timer -= Time.deltaTime;
	}
}