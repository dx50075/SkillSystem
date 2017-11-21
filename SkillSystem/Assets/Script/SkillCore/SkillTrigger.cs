using System;
using System.Collections.Generic;

namespace SkillCore
{
    public class SkillData 
    {
        public int m_AttackerID;
        public int m_TargetID;
        public float m_PhysicDamage;
        public float m_MagicDamage;
        public float m_CostMP;
        public string m_EffectName;
        public float m_EffectDuration;
        public string m_AnimationName;
        public float m_BulletSpeed;
    }

    public interface ISkillTrigger
    {
        void Init(string args);
        void Reset();
        ISkillTrigger Clone();
        bool Execute(SkillData data, float curTime);
        float GetStartTime();
        bool IsExecuted();

        string GetTypeName();
    }

    public abstract class AbstractSkillTrigger : ISkillTrigger
    {
        protected float m_StartTime = 0;
        protected bool m_IsExecuted = false;
        protected string m_TypeName;        
        public abstract void Init(string args);
        public virtual void Reset() { m_IsExecuted = false; }
        public abstract ISkillTrigger Clone();
        public abstract bool Execute(SkillData data, float curTime);
        public float GetStartTime() { return m_StartTime; }
        public bool IsExecuted() { return m_IsExecuted; }
        public string GetTypeName() { return m_TypeName; }
    }
}