using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	

namespace Com.DynamicLife.RoboBattleground
{
	public class MainMenu : MonoBehaviour
	{
		public Launcher launcher;

		private void Start()
		{
			Pause.paused = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		public void SinglePlayer()
		{
			SceneManager.LoadScene("S");
		}

		public void JoinMath()
		{
			launcher.Join();
		}

		public void CreateMath()
		{
			launcher.Create();
		}

		public void QuitGame()
		{
			Application.Quit();
		}
	}
}
