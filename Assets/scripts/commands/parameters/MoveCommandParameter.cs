using UnityEngine;
using System.Collections;

public class MoveCommandParameter : CommandParameter {

	MoveCommand.Direction m_direction;
	PlayerDebug m_player;

	public MoveCommandParameter(){
	}

	public MoveCommandParameter(MoveCommand.Direction _direction, PlayerDebug _player){
		m_direction = _direction;
		m_player = _player;
	}

	public PlayerDebug Player {
		get { return m_player;}
		set {m_player = value;	}
	}

	public MoveCommand.Direction Direction{
		get{ return m_direction;}
		set{ m_direction = value; }
	}
}
