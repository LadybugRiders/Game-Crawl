using UnityEngine;
using System.Collections;

public class PlayerDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveLeft()
    {
        Utils.SetLocalPositionX(transform,transform.localPosition.x - 1);
    }

    public void MoveRight()
    {
        Utils.SetLocalPositionX(transform, transform.localPosition.x + 1);
    }
}
