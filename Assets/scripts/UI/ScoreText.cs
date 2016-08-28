using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {

	private bool m_alive;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (true);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Launch( Note _note){
		m_alive = true;
		Debug.Log ("launch");
		gameObject.SetActive (true);
		Utils.SetLocalPositionY (transform,0);
		Utils.SetPositionX (transform, _note.transform.localPosition.x);
		//set tween
		var dest = _note.transform.position + new Vector3(0,3,0);
		TweenEngine.instance.PositionTo (transform, dest, 1);
	}

	public bool IsAlive {
		get {
			return m_alive;
		}
	}
}
