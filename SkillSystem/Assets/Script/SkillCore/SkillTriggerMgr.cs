using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillCore
{
    class SkillTriggerMgr
    {
        public static readonly SkillTriggerMgr Instance = new SkillTriggerMgr();
        Dictionary<string, BaseSkillTriggerFactory> m_Factories = new Dictionary<string, BaseSkillTriggerFactory>();
        private SkillTriggerMgr() { }
        public void RegisterTriggerFactory(string name, BaseSkillTriggerFactory factory)
        {
             if(!m_Factories.ContainsKey(name))
             {
                 m_Factories.Add(name, factory);
             }
        }

        public BaseSkillTriggerFactory GetTriggerFactory(string type)
        {
            BaseSkillTriggerFactory ret = null;
            m_Factories.TryGetValue(type, out ret);
            return ret;
        }

        public ISkillTrigger CreateTrigger(string type,string args)
        {
            ISkillTrigger ret = null;
            BaseSkillTriggerFactory factory = GetTriggerFactory(type);
            if (factory == null)
                return null;
            ret = factory.Create();
            ret.Init(args);
            return ret;
        }
    }
}
