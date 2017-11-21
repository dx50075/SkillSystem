using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour {

    ETCJoystick m_Joystick = null;
    void Awake()
    {
        m_Joystick = GetComponent<ETCJoystick>();
    }
	void OnEnable()
    {
        //ETCJoystick.OnMoveStartHandler += OnMoveStart;
        m_Joystick.onMoveStart.AddListener(OnMoveStart);
        m_Joystick.onMove.AddListener(OnMove);
        m_Joystick.onMoveEnd.AddListener(OnMoveEnd);
    }

    void OnDisable()
    {
        m_Joystick.onMoveStart.RemoveListener(OnMoveStart);
        m_Joystick.onMove.RemoveListener(OnMove);
        m_Joystick.onMoveEnd.RemoveListener(OnMoveEnd);
    }

    void OnMoveStart()
    {
        
    }

    void OnMove(Vector2 offset)
    {
        Vector3 pos = TestSkill.m_Actor.transform.position  + new Vector3(offset.x,0f,offset.y);
        TestSkill.m_Actor.MoveTo(pos);

    }
    void OnMoveEnd()
    {
        TestSkill.m_Actor.StopMove();
    }
}
