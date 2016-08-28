using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField] float m_step = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveLeft()
	{
		Utils.SetLocalPositionX(transform,transform.localPosition.x - m_step);
	}

	public void MoveRight()
	{
		Utils.SetLocalPositionX(transform, transform.localPosition.x + m_step);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == LayerMask.NameToLayer ("Note")) {
			Debug.Log ("dead");
		}
	}
}
