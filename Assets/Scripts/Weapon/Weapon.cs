using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.DynamicLife.RoboBattleground
{
	public class Weapon : MonoBehaviourPunCallbacks
	{
		public GameObject bulletholePrefab;
		public LayerMask canBeShot;
		private AudioSource mAudioSrc;
	    public float damage = 15f;

		void Start()
		{
			mAudioSrc = GetComponent<AudioSource>();
		}

		void Update ()
		{
			if (Pause.paused) return;
			
			if (Input.GetMouseButtonDown(0))
			{
				mAudioSrc.Play();
				Shoot();
			}
		}

		void Shoot ()
		{
			Transform t_spawn = transform.Find("Cameras/Normal Camera");

			RaycastHit t_hit = new RaycastHit();
			if (Physics.Raycast(t_spawn.position, t_spawn.forward, out t_hit, 1000f, canBeShot))
			{
				GameObject t_newHole = Instantiate (bulletholePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
				t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
				Destroy(t_newHole, 1f);
			}
		}
	}
}
