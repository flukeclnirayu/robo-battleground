using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace Com.DynamicLife.RoboBattleground
{
	public class Defender : MonoBehaviourPunCallbacks
	{
	    public float speed;
	    public float sprintModifier;
	    public float jumpForce;
	   	public int max_health = 100;
	    public GameObject CameraParent;
	    public Transform groundDetector;
	    public LayerMask ground;
	    public bool t_skill1 = false;
	    public float cooldown = 5.0f;
	    public float timer = 0.0f;

	    private Transform ui_healthbar;

	    private Rigidbody rig;

	    private float current_health;

	    private Manager manager;

	    private void Start()
	    {

	    	manager = GameObject.Find("Manager").GetComponent<Manager>();

	    	current_health = max_health;

			CameraParent.SetActive(photonView.IsMine);

			if (!photonView.IsMine)
			{
				gameObject.layer = 10;
			}

	        rig = GetComponent<Rigidbody>();

	        if (photonView.IsMine)
	        {
		        ui_healthbar = GameObject.Find("HUD/Health/Bar").transform;
		        RefreshHealthBar();
	        }

	        RefreshHealthBar();
	    }

	    private void FixedUpdate()
	    {
	    	if (!photonView.IsMine) return;

	        float t_hmove = Input.GetAxisRaw("Horizontal");
	        float t_vmove = Input.GetAxisRaw("Vertical");

	        bool t_sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
	       	bool pause = Input.GetKeyDown(KeyCode.Escape);

	        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
	        bool isSprinting = t_sprint && t_vmove > 0 && isGrounded;

	        Vector3 t_direction = new Vector3(t_hmove, 0, t_vmove);
	        t_direction.Normalize();

	        float t_adjustSpeed = speed;

	        if (Input.GetKeyDown(KeyCode.U)) TakeDamage(10);

	       	if (Input.GetKeyDown(KeyCode.Alpha1) && current_health == 100) ChangeToStriker();
	        if (Input.GetKeyDown(KeyCode.Alpha2) && current_health == 100) ChangeToDefender();
	        if (Input.GetKeyDown(KeyCode.Alpha3) && current_health == 100) ChangeToSneaker();

	       	if (Input.GetKeyDown(KeyCode.F)) Bulletproof();

	        if (pause)
	        {
	        	GameObject.Find("Pause").GetComponent<Pause>().TogglePause();
	        }

	        if (Pause.paused)
	        {
	        	t_hmove = 0f;
	        	t_vmove = 0f;
	        	t_sprint = false;
	        	pause = false;
	        	isGrounded = false;
	        	isSprinting = false;
	        }

	        if (isSprinting)
	        {
	        	t_adjustSpeed *= sprintModifier;
	        }

	        timer += Time.deltaTime;

	        if (timer > cooldown)
	        {
	        	t_skill1 = false;
	        	timer -= cooldown;
	        }

	        Vector3 t_targetVelocity = transform.TransformDirection(t_direction) * t_adjustSpeed * Time.deltaTime;
			t_targetVelocity.y = rig.velocity.y;	        
	        rig.velocity = t_targetVelocity;
	    }

	    void Bulletproof()
	    {
	    	t_skill1 = true;
	    }

	    void RefreshHealthBar ()
	    {
	    	float t_health_ratio = (float)current_health / (float)max_health;
	    	ui_healthbar.localScale = new Vector3(t_health_ratio, 1, 1);
	    }

	    public void ChangeToStriker()
	    {
	    	manager.SpawnStriker();
	    	PhotonNetwork.Destroy(gameObject);
	    }

	    public void ChangeToDefender()
	    {
	    	manager.SpawnDefender();
	    	PhotonNetwork.Destroy(gameObject);
	    }

	    public void ChangeToSneaker()
	    {
	    	manager.SpawnSneaker();
	    	PhotonNetwork.Destroy(gameObject);
	    }

	    public void TakeDamage(int p_damage)
	    {
	    	if (photonView.IsMine)
	    	{
	    		if (t_skill1 != true)
	    		{
			    	current_health -= p_damage;
			    	RefreshHealthBar();
	    		} 

		    	if (current_health <= 0)
		    	{
		    		manager.Spawn();
		    		PhotonNetwork.Destroy(gameObject);
		    	}
	    	}
	    }
	}
}
