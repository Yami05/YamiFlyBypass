using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
	public static GameEvents instance;

	private void Awake()
	{
		instance = this;
	}


	public Action WingPosition;
	public Action OnFly;
	public Action OnStart;
	public Action OnFinish;
	public Action OnEnd;
	public Action GameOver;
}
