using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillCore
{
    public class SkillInstance
    {
        public bool m_IsUsed = false;
        public int m_SkillID = -1;
        float m_SkillBeginTime = 0f;
        int m_ExecutedCount = 0;        //已执行了多少个触发器
        public List<ISkillTrigger> m_SkillTriggers = new List<ISkillTrigger>();

        public SkillInstance(int skillID) { m_SkillID = skillID; }
       

        public SkillInstance(SkillInstance other)
        {
            m_SkillID = other.m_SkillID;
            foreach(ISkillTrigger t in other.m_SkillTriggers)
            {
                m_SkillTriggers.Add(t.Clone());
            }
        }

        public bool UpdateTriggers(float time,SkillData data)
        {
            int count = m_SkillTriggers.Count;
            float elapsed = time - m_SkillBeginTime;
            for(int i = 0; i < count;i++)
            {
                var trigger = m_SkillTriggers[i];
                if (elapsed >= trigger.GetStartTime() && !trigger.IsExecuted())
                {
                    m_SkillTriggers[i].Execute(data, time);
                    m_ExecutedCount++;                    
                }
            }
            return CheckEnd();
        }

        public void Reset()
        {
            foreach(ISkillTrigger t in m_SkillTriggers)
            {
                t.Reset();
            }
        }

        public int GetTriggerCount(string typeName)
        {
            int count = 0;
            foreach(ISkillTrigger t in m_SkillTriggers)
            {
                if (t.GetTypeName() == typeName)
                    ++count;
            }
            return count;
        }

        public void Begin(float skillBeginTime)
        {
            m_SkillBeginTime = skillBeginTime;
            m_IsUsed = true;
            m_ExecutedCount = 0;
            Reset();
        }

        public void End()
        {
            m_IsUsed = false;
            SkillSystem.Instance.RecycleSkillInstance(this);
        }

        public bool CheckEnd()
        {
            if(m_ExecutedCount == m_SkillTriggers.Count)  //触发器全部执行完毕
            {
                End();
                return true;
            }
            return false;
        }
    }
}
