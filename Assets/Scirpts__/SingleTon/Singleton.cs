using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T:class {

	// Use this for initialization Is


	protected static T _instance =null;
	public static T Instance{

		get{
			if(_instance==null){
				_instance=SingletonManager.gameobject.AddComponent(typeof(T)) as T;
			
			}
			return _instance;
		}

	}
	public static void Instantiate(){
		_instance = Instance;
	}

	public Singleton(){
		_instance = this as T;
	}
	public void ExcecuteAfterCoroutine(IEnumerator coroutine,System.Action action){

		StartCoroutine (ExecuteAfterCoroutineActual (coroutine, action));

	}
	public IEnumerator ExecuteAfterCoroutineActual(IEnumerator coroutine,System.Action action){

		yield return StartCoroutine (coroutine);
		action ();
	}
}



	public class SingletonManager{

		private static GameObject _gameobject=null;
		public static GameObject gameobject{
			get{
				if(_gameobject==null){
					_gameobject=new GameObject("-SingletonManager");
					Object.DontDestroyOnLoad(_gameobject);

				}
			return _gameobject;

			}
	
		}
	}





