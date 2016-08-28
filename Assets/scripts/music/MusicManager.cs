using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	[SerializeField] AudioSource m_crackSource;
	[SerializeField] AudioSource m_musicSource;

	bool m_musicLaunched = false;

	public void Awake(){

	}

	public void StartMusic(){
		m_crackSource.volume = 0.3f;
		m_musicSource.Play ();
	}

}
