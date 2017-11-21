using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SkillCore
{
    public class SkillSystem
    {
        public static readonly SkillSystem Instance = new SkillSystem();
        Dictionary<int, List<SkillInstance>> m_SkillInstances = new Dictionary<int, List<SkillInstance>>();
        private SkillSystem() 
        { 
            if(!Init())
            {
                throw new Exception("register trigger factory failed"); 
            }
        }
        public bool Init()
        {
            SkillTriggerMgr.Instance.RegisterTriggerFactory("PlayAnimation", new SkillTriggerFactory<PlayAnimationTrigger>());
            SkillTriggerMgr.Instance.RegisterTriggerFactory("PlayEffect", new SkillTriggerFactory<PlayEffectTrigger>());
            SkillTriggerMgr.Instance.RegisterTriggerFactory("Bullet", new SkillTriggerFactory<BulletTrigger>());
            SkillTriggerMgr.Instance.RegisterTriggerFactory("FaceToTarget", new SkillTriggerFactory<FaceToTargetTrigger>());
            return true;
        }


        public SkillInstance NewSkillInstance(int skillID)
        {
            SkillInstance instance = null;
            List<SkillInstance> l = null;
            if(!m_SkillInstances.TryGetValue(skillID,out l))
            {
                return null;
            }
            if (l.Count > 1)   //至少有2个技能实例才从技能池里取，因为需要至少保留一个作为可拷贝的对象
            {
                instance = l[0];
                l.RemoveAt(0);
            }
            else if(l.Count > 0)
            {
                instance = new SkillInstance(l[0]);  //拷贝构造
            }         
            return instance;
        }

        public void RecycleSkillInstance(SkillInstance skill)
        {
            AddSkillInstanceToPool(skill.m_SkillID, skill);
        }

        public void AddSkillInstanceToPool(int skillID,SkillInstance skill)
        {
            List<SkillInstance> l = null;
            if(!m_SkillInstances.TryGetValue(skillID,out l))
            {
                l = new List<SkillInstance>();
                m_SkillInstances.Add(skillID, l);
            }
            l.Add(skill);
        }

        public void Reset()
        {
            m_SkillInstances.Clear();
        }

        public bool LoadSkill(string txt)
        {
            bool bracket = false;
            SkillInstance skill = null;  
            if (string.IsNullOrEmpty(txt))
                return false;

            string[] lines = txt.Split('\n');
            foreach(var item in lines)
            {
                string line = item.Trim();
                if (line.StartsWith("//") || line == "")
                    continue;
                if(line.StartsWith("skill"))
                {
                    int start = line.IndexOf("(");
                    int end = line.IndexOf(")");
                    if (start == -1 || end == -1)
                    {
                        throw new Exception(string.Format("ParseScript Error, start == -1 || end == -1  {0}", line));
                    }
                    int len = end - start - 1;
                    if (len <= 0) throw new Exception(string.Format("ParseScript Error, length <= 1, {0}", line));
                    string args = line.Substring(start + 1, len);
                    int skillID = (int)Convert.ChangeType(args, typeof(int));
                    skill = new SkillInstance(skillID);
                    AddSkillInstanceToPool(skillID, skill);
                }
                else if(line.StartsWith("{"))
                {
                    bracket = true;
                }
                else if(line.StartsWith("}"))
                {
                    bracket = false;
//                     skill.m_SkillTriggers.Sort((left, right) =>
//                         {
//                             float time1 = left.GetStartTime();
//                             float time2 = right.GetStartTime();
//                             if (time1 > time2) return -1;
//                             else if (time1 == time2) return 0;
//                             else return 1;
//                             
//                         });
                }
                else
                {
                    if(skill != null && bracket)
                    {
                        int start = line.IndexOf("(");
                        int end = line.IndexOf(")");
                        if (start == -1 || end == -1) throw new Exception(string.Format("ParseScript Error, start == -1 || end == -1  {0}", line));
                        int len = end - start - 1;
                        if (len <= 0) throw new Exception(string.Format("ParseScript Error, length <= 1, {0}", line));
                        string type = line.Substring(0, start);
                        string args = line.Substring(start + 1, len);
                        args = args.Replace(" ", "");
                        ISkillTrigger trigger = SkillTriggerMgr.Instance.CreateTrigger(type, args);
                        if(trigger != null)
                        {
                            skill.m_SkillTriggers.Add(trigger);
                        }
                        else
                        {
                            throw new Exception(string.Format("create trigger failed,type = {0}", type));
                        }
                    }
                }
            }
            return true;
        }
    }
}
