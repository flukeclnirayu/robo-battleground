using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace Com.DynamicLife.RoboBattleground
{
	public class Grenade : MonoBehaviourPunCallbacks
	{
		public float delay = 3f;
		public float radius = 5f;

		public GameObject explosionEffect;

		float countdown;
		bool hasExploded = false;
	    // Start is called before the first frame update
	    void Start()
	    {
	        countdown = delay;
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        countdown -= Time.deltaTime;
	        if (countdown <= 0f && !hasExploded)
	        {
	        	Explode();
	        	hasExploded = true;
	        }
	    }

	    void Explode()
	    {
	    	Instantiate(explosionEffect, transform.position, transform.rotation);

	    	Destroy(gameObject);
	    }
	}
}