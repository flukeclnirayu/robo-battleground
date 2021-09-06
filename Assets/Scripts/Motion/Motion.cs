using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.DynamicLife.RoboBattleground
{
	public class Motion : MonoBehaviour
	{
		public float speed;

		private Rigidbody rig;
	    // Start is called before the first frame update
	    void Start()
	    {
	        rig = GetComponent<Rigidbody>();
	    }

	    // Update is called once per frame
	    void FixedUpdate()
	    {
	        float t_hmove = Input.GetAxisRaw("Horizontal");
	        float t_vmove = Input.GetAxisRaw("Vertical");

	        bool pause = Input.GetKeyDown(KeyCode.Escape);

	        Vector3 t_direction = new Vector3(t_hmove, 0, t_vmove);
	        t_direction.Normalize();

	        rig.velocity = transform.TransformDirection(t_direction) * speed * Time.deltaTime;

	       	if (pause)
	        {
				SceneManager.LoadScene(0);
	        }
	    }
	}
}