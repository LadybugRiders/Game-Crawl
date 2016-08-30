using UnityEngine;
using System.Collections;

public class SheetsManager : MonoBehaviour {

	public static readonly ushort	SHEET_HEIGHT = 450;
	public static readonly float	SHEET_LIMIT	= -1 * 450 * 3 * 0.01f;

    float m_speed = 1.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		for (ushort i=0; i<transform.childCount; ++i) {
			Transform child = transform.GetChild(i);

			float localY = child.localPosition.y;

			if (localY < SHEET_LIMIT) {
				ushort nextChildIndex = (ushort) ((i+1) % transform.childCount);
				Transform nextChild = transform.GetChild(nextChildIndex);
				localY = nextChild.localPosition.y + (SHEET_HEIGHT * 0.01f);
			}

			float newLocalY = localY + (NoteGridsGenerator.instance.GridSpeed * Time.deltaTime);

			Utils.SetLocalPositionY(child, newLocalY);
		}
	}
}
