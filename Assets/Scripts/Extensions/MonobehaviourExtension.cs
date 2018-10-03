using UnityEngine;
using System.Collections;

public static class MonobehaviourExtension {
	/// <summary>
	/// Create chain of coroutines that will run in queue order
	/// </summary>
	/// <param name="obj">Put as parametr keyword "this"</param>
	/// <param name="coroutines">Chain of coroutines</param>
	/// <returns></returns>
	public static void Chain (this MonoBehaviour monoBehaviour, MonoBehaviour obj, params IEnumerator[] coroutines) {
		monoBehaviour.StartCoroutine (EnumeratorChain (obj, coroutines));
	}
	private static IEnumerator EnumeratorChain (MonoBehaviour obj, params IEnumerator[] coroutines) {
		for (int i = 0; i < coroutines.Length; i++)
			yield return obj.StartCoroutine (coroutines[i]);
	}
	// ------------------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Do some action without delay
	/// </summary>
	/// <param name="action">Some action or delegate to execute</param>
	/// <returns></returns>
	public static void ExecuteAction (this MonoBehaviour monoBehaviour, System.Action action) {
		monoBehaviour.StartCoroutine (CoroutineExecuteAction (monoBehaviour, action));
	}
	public static IEnumerator CoroutineExecuteAction (this MonoBehaviour monoBehaviour, System.Action action) {
		if (action != null)
			action ();
		yield return null;
	}
	// ------------------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Wait for some time, then execute some action
	/// </summary>
	/// <param name="delay">Waiting time</param>
	/// <param name="action">Some action or delegate to execute</param>
	/// <returns></returns>
	public static void ExecuteAfterDelay (this MonoBehaviour monoBehaviour, float delay, System.Action action) {
		monoBehaviour.StartCoroutine (CoroutineExecuteAfterDelay (monoBehaviour, delay, action));
	}
	public static IEnumerator CoroutineExecuteAfterDelay (this MonoBehaviour monoBehaviour, float delay, System.Action action) {
		while (delay > 0) {
			delay -= Time.deltaTime;
			yield return null;
		}

		if (action != null)
			action ();
	}
	// ------------------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Wait N frames and then execute action or delegate
	/// </summary>
	/// <param name="frames">Frames to wait</param>
	/// <param name="action">Some action or delegate to execute</param>
	/// <returns></returns>
	public static void ExecuteAfterNFrames<T> (this MonoBehaviour monoBehaviour, T frames, System.Action action) where T : struct {
		monoBehaviour.StartCoroutine (CoroutineExecuteAfterNFrames (monoBehaviour, frames, action));
	}
	public static IEnumerator CoroutineExecuteAfterNFrames<T> (this MonoBehaviour monoBehaviour, T frames, System.Action action) where T : struct {
		WaitForEndOfFrame wait = new WaitForEndOfFrame ();
		int frameCount = System.Convert.ToInt32 (frames);
		for (int i = 0; i < frameCount; i++)
			yield return wait;

		if (action != null)
			action ();
	}
	// ------------------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Wait in game seconds delay(take in count timeScale) 
	/// </summary>
	/// <param name="delay">Time to wait</param>
	/// <returns></returns>
	public static void WaitFor (this MonoBehaviour monoBehaviour, float delay) {
		monoBehaviour.StartCoroutine (CoroutineWaitFor (monoBehaviour, delay));
	}
	public static IEnumerator CoroutineWaitFor (this MonoBehaviour monoBehaviour, float delay) {
		yield return new WaitForSeconds (delay);
	}
	// ------------------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	///  Wait for true value
	/// </summary>
	/// <param name="trigger">Some bool value</param>
	/// <returns></returns>
	public static void WaitForTrue (this MonoBehaviour monoBehaviour, bool trigger, System.Action action) {
		monoBehaviour.StartCoroutine (CoroutineWaitForTrue (monoBehaviour, trigger, action));
	}
	public static IEnumerator CoroutineWaitForTrue (this MonoBehaviour monoBehaviour, bool trigger, System.Action action) {
		yield return new WaitUntil (() => trigger);

		if (action != null)
			action ();
	}
}
