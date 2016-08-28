using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField] float m_step = 10;

	MusicTriggerer m_triggerer;

	int m_position = 2;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("current_score",0);
		m_triggerer = FindObjectOfType<MusicTriggerer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveLeft()
	{
		if (m_position <= 0)
			return;
		m_position--;
		Utils.SetLocalPositionX(transform,transform.localPosition.x - m_step);
	}

	public void MoveRight()
	{
		if (m_position >=4)
			return;
		m_position++;
		Utils.SetLocalPositionX(transform, transform.localPosition.x + m_step);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == LayerMask.NameToLayer ("Note")) {
			//SceneManager.LoadScene ("game_over");
		}else if (other.gameObject.layer == LayerMask.NameToLayer ("Bonus")) {
			m_triggerer.HitScore(other.GetComponent<Note>());
		}
	}
}
