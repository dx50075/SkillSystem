using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveType : MonoBehaviour 
{
    public enum MType
    {
        Linear,
        Exponential,
        Parabola,
        Sine
    }

    public MType m_Type = MType.Linear;
    public Vector3 m_Target;
    public float m_Speed;
    public bool m_IsMoving = false;
    public System.Action m_Callback;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!m_IsMoving) return;
        if (m_Type == MType.Linear)
            UpdateLinear();
        else if (m_Type == MType.Parabola)
            UpdatePorabola();

	}

    void UpdateLinear()
    {
        Vector3 dis = m_Target - transform.position;
        float delta = m_Speed * Time.deltaTime;
        if(dis.sqrMagnitude <= delta * delta)
        {
            MoveEnd();
        }
        else
        {
            transform.position += delta * dis.normalized;
            transform.LookAt(m_Target);
        }
    }

    float m_Elapsed = 0f;
    void UpdatePorabola()
    {
        float delta = m_Speed * Time.deltaTime;
        Vector3 dis = m_Target - transform.position;
        m_Elapsed += Time.deltaTime;
        Vector3 h = dis.normalized * m_Speed * Time.deltaTime;  //水平方向
        float y = (m_YStartSpeed - 0.5f * G * m_Elapsed) * m_Elapsed; //竖直方向
        h += transform.position;
        h.y = y;
        transform.position = h;

        if (transform.position.y < m_Target.y)
        {
            MoveEnd();
        }
    }

    float m_YStartSpeed = 10f;      //向上的初速度
    public const float G = 9.8f;    //重力加速度
    public void MoveTo(MType type,Vector3 target,float speed, System.Action callback)
    {
        m_Type = type;
        m_Target = target;
        m_Speed = speed;
        m_Callback = callback;
        if(type == MType.Parabola)
        {
            float time = Vector3.Distance(m_Target,transform.position) / speed;
            m_YStartSpeed = 0.5f * G * time;
            m_Elapsed = 0f;
        }        
        m_IsMoving = true;
    }

    public void MoveEnd()
    {
        m_IsMoving = false;
        if (m_Callback != null)
            m_Callback();
    }
}
