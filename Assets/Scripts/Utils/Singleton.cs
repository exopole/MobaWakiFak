using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		protected static T _instance;
		public static T Instance { get => _instance; }

		protected virtual void Awake()
		{
			if (_instance == null)
			{
				_instance = (T)this;
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}