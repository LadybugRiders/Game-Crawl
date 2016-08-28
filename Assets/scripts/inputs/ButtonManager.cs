using UnityEngine;
using System.Collections;

public class ButtonManager : SpriteTouchManager {

    [SerializeField] GameObject m_rightButton;
    [SerializeField] GameObject m_leftButton;

	Player m_player;

    // Use this for initialization
    void Start () {
		m_player = FindObjectOfType<Player> ();
	}
	
    protected override void OnPressed(Collider2D _collider)
    {
        base.OnPressed(_collider);
        if( _collider == m_leftButton.GetComponent<Collider2D>())
        {
			MoveCommand command = new MoveCommand ();
			MoveCommandParameter param = new MoveCommandParameter (MoveCommand.Direction.LEFT, m_player);
			CommandManager.instance.AddCommand (command,param);
        }
        if (_collider == m_rightButton.GetComponent<Collider2D>())
        {
			MoveCommand command = new MoveCommand ();
			MoveCommandParameter param = new MoveCommandParameter (MoveCommand.Direction.RIGHT, m_player);
			CommandManager.instance.AddCommand (command,param);
        }
    }
}
