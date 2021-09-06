using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Com.DynamicLife.RoboBattleground
{
	public class Manager : MonoBehaviour
	{
		public string player_prefab;
		public Transform[] spawn_points;
		public Text lifes;
		public Text skill;
		public Text gadget;
		public int score;

		private void Start()
		{
			score = 4;
			Spawn();
		}

		public void SpawnStriker()
		{
			player_prefab = "Striker";
			Transform t_spawn = spawn_points[Random.Range(0, spawn_points.Length)];
			PhotonNetwork.Instantiate(player_prefab, t_spawn.position, t_spawn.rotation);
			skill.text = "Speed Boost";		
		}

		public void SpawnDefender()
		{
			player_prefab = "Defender";
			Transform t_spawn = spawn_points[Random.Range(0, spawn_points.Length)];
			PhotonNetwork.Instantiate(player_prefab, t_spawn.position, t_spawn.rotation);
			skill.text = "Bulletproof";
		}

		public void SpawnSneaker()
		{
			player_prefab = "Sneaker";
			Transform t_spawn = spawn_points[Random.Range(0, spawn_points.Length)];
			PhotonNetwork.Instantiate(player_prefab, t_spawn.position, t_spawn.rotation);
			skill.text = "Invisible";
		}

		public void Spawn()
		{
			score--;
			if (score != 0)
			{
				lifes.text = score.ToString();
				Transform t_spawn = spawn_points[Random.Range(0, spawn_points.Length)];
				PhotonNetwork.Instantiate(player_prefab, t_spawn.position, t_spawn.rotation);
			} else if (score == 0){
				PhotonNetwork.LeaveRoom();
				SceneManager.LoadScene(0);
			}
		}
	}
}