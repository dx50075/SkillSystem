using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class ActorManager
{
    public static readonly ActorManager Instance = new ActorManager();
    private Transform m_Root = null;
    Dictionary<int, Actor> m_dicActors = new Dictionary<int, Actor>();
    private ActorManager()
    {
        m_Root = new GameObject("ActorRoot").transform;
    }
    
    public Actor CreateActor(int uid = 0)
    {
        if(uid == 0)
        {
            uid = m_dicActors.Count + 1;
        }
        GameObject go = new GameObject(uid.ToString());
        Actor actor = go.AddComponent<Actor>();
        actor.Init(uid);
        actor.SetModel(GameObject.Instantiate<GameObject>(Resources.Load("ashe") as GameObject));
        m_dicActors[uid] = actor;
        go.transform.SetParent(m_Root);
        return actor;
    }

    public void RemoveActor(int uid)
    {
        Actor actor = null;
        if(m_dicActors.TryGetValue(uid,out actor))
        {
            GameObject.Destroy(actor);
            m_dicActors.Remove(uid);
        }
    }

    public Actor Find(int uid)
    {
        Actor actor = null;
        m_dicActors.TryGetValue(uid, out actor);
        if(actor == null)
        {
            Debug.LogError("can't find actor , uid = " + uid);
        }
        return actor;

    }

}
