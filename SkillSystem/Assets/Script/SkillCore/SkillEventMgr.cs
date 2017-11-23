using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillCore;

public class MonoHelper : MonoBehaviour
{
    private static MonoHelper _instance = null;
    public static MonoHelper Instance 
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("MonoHelper").AddComponent<MonoHelper>();
            }
            return _instance;
        }
    }

    public void Invoke(System.Action action,float delay)
    {
        StartCoroutine(DelayInvoke(action, delay));
    }

    IEnumerator DelayInvoke(System.Action action,float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
}

public class SkillEventMgr
{
    public static readonly SkillEventMgr Instance = new SkillEventMgr();
    private SkillEventMgr()
    {
        
    }

    public void RegisterSkillEvent()
    {
        EventDispatcher.AddEventListener<SkillData>(EventID.PlayAnimation, Handle_PlayAnimation);
        EventDispatcher.AddEventListener<SkillData>(EventID.PlayEffect, Handle_PlayEffect);
        EventDispatcher.AddEventListener<SkillData>(EventID.Bullet, Handle_Bullet);
        EventDispatcher.AddEventListener<SkillData>(EventID.FaceToTarget, Handle_FaceToTarget);
    }

    public void Handle_PlayAnimation(SkillData data)
    {
        Actor attacker = ActorManager.Instance.Find(data.m_AttackerID);
        attacker.PlayAnimation(data.m_AnimationName);
    }
    public void Handle_PlayEffect(SkillData data)
    {
        Actor attacker = ActorManager.Instance.Find(data.m_AttackerID);
        GameObject effect = GameObject.Instantiate<GameObject>((Resources.Load("52SpecialEffectPack/Effect/" + data.m_EffectName) as GameObject));
        effect.transform.position = attacker.transform.position;
        MonoHelper.Instance.Invoke(() =>
            {
                GameObject.Destroy(effect);
            }, data.m_EffectDuration);
    }
    public void Handle_Bullet(SkillData data)
    {
        Actor attacker = ActorManager.Instance.Find(data.m_AttackerID);
        GameObject effect = GameObject.Instantiate<GameObject>((Resources.Load(data.m_EffectName) as GameObject));
        effect.transform.position = attacker.transform.position;
        MoveType m = effect.AddComponent<MoveType>();

        Actor target = ActorManager.Instance.Find(data.m_TargetID);
        m.MoveTo(MoveType.MType.Bezier, target.transform.position,data.m_BulletSpeed, () =>
            {
                GameObject.Destroy(effect);
            });
    }

    public void Handle_FaceToTarget(SkillData data)
    {
        Actor attacker = ActorManager.Instance.Find(data.m_AttackerID);
        Actor target = ActorManager.Instance.Find(data.m_TargetID);
        if(attacker != null && target != null)
        {
            attacker.transform.LookAt(target.transform);
        }
    }

}
