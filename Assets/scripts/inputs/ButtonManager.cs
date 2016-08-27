using UnityEngine;
using System.Collections;

public class ButtonManager : SpriteTouchManager {

    [SerializeField] GameObject m_rightButton;
    [SerializeField] GameObject m_leftButton;

    // Use this for initialization
    void Start () {
	
	}
	
    protected override void OnPressed(Collider2D _collider)
    {
        base.OnPressed(_collider);
        if( _collider == m_leftButton.GetComponent<Collider2D>())
        {
            var player = GameObject.Find("Player").GetComponent<PlayerDebug>();
			MoveCommand command = new MoveCommand ();
			MoveCommandParameter param = new MoveCommandParameter (MoveCommand.Direction.LEFT, player);
			CommandManager.instance.AddCommand (command,param);
        }
        if (_collider == m_rightButton.GetComponent<Collider2D>())
        {
			var player = GameObject.Find("Player").GetComponent<PlayerDebug>();
			MoveCommand command = new MoveCommand ();
			MoveCommandParameter param = new MoveCommandParameter (MoveCommand.Direction.RIGHT, player);
			CommandManager.instance.AddCommand (command,param);
        }
    }
}
