using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.DynamicLife.RoboBattleground
{
	public class DefenderWeapon : MonoBehaviourPunCallbacks
	{
		public GameObject bulletholePrefab;
		public LayerMask canBeShot;
		private AudioSource mAudioSrc;
		public GameObject grenadePrefab;

		void Start()
		{
			mAudioSrc = GetComponent<AudioSource>();
		}

		void Update ()
		{
			if (Pause.paused && photonView.IsMine) return;
			
			if (!photonView.IsMine) return;
			
			if (Input.GetMouseButtonDown(0))
			{
				mAudioSrc.Play();
				photonView.RPC("Shoot", RpcTarget.All);
			}
			if (Input.GetMouseButtonDown(1))
			{
				photonView.RPC("ThrowGrenade", RpcTarget.All);
			}
		}

		[PunRPC]
		void ThrowGrenade()
		{
			GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
			Rigidbody rb = grenade.GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * 10f, ForceMode.VelocityChange);
		}

		[PunRPC]
		void Shoot ()
		{
			Transform t_spawn = transform.Find("Cameras/Normal Camera");

			RaycastHit t_hit = new RaycastHit();
			if (Physics.Raycast(t_spawn.position, t_spawn.forward, out t_hit, 1000f, canBeShot))
			{
				GameObject t_newHole = Instantiate (bulletholePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
				t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
				Destroy(t_newHole, 1f);

				if (photonView.IsMine)
				{
					if (t_hit.collider.gameObject.layer == 10)
					{
						t_hit.collider.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, 10);
					}
				}
			}
		}

		[PunRPC]
		private void TakeDamage (int p_damage)
		{
			Debug.Log(p_damage);
			GetComponent<Defender>().TakeDamage(p_damage);
		}
	}
}
