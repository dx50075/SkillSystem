using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveType : MonoBehaviour 
{
    public enum MType
    {
        Linear,
        Bezier,
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
        else if (m_Type == MType.Bezier)
            UpdateBezier();

	}

   

    float m_YStartSpeed = 10f;      //向上的初速度
    public const float G = 9.8f;    //重力加速度
    Vector3 m_P0;
    Vector3 m_P1; 
    Vector3 m_P2;
    float m_ArrivedTime = 0f;
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
        else if(type == MType.Bezier)
        {
            m_P0 = transform.position;
            m_P2 = target;
            m_P1 = GetP1(m_P0, m_P2);
            m_ArrivedTime = Vector3.Distance(m_Target, transform.position) / speed;
            m_Elapsed = 0f;
        }
        m_IsMoving = true;
    }

    void UpdateLinear()
    {
        Vector3 dis = m_Target - transform.position;
        float delta = m_Speed * Time.deltaTime;
        if (dis.sqrMagnitude <= delta * delta)
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

    //计算二次贝塞尔曲线的第2个点
    Vector3 GetP1(Vector3 p0,Vector3 p2)
    {
        Vector3 z = Vector3.Cross(p2 - p0, Vector3.up).normalized;
        Vector3 center = (p2 + p0) / 2f;
        return center + z * Random.Range(-5, 5);
       
    }

    void UpdateBezier()
    {               
        Vector3 dis = m_Target - transform.position;
        //float delta = m_Speed * Time.deltaTime;
        if (dis.sqrMagnitude <= 0.25f)
        {
            MoveEnd();
        }
        else
        {
            Vector3 pos = Bezier.BezierCurve(m_P0, m_P1, m_P2, m_Elapsed / m_ArrivedTime);
            transform.position = pos;
            m_Elapsed += Time.deltaTime;
        }
    }

    public void MoveEnd()
    {
        m_IsMoving = false;
        if (m_Callback != null)
            m_Callback();
    }
}
