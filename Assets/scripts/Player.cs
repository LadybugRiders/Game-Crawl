using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField] float m_step = 10;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("current_score",0);
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
			SceneManager.LoadScene ("game_over");
		}
	}
}
