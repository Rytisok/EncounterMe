using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.DataClasses
{
    public class DummyUser
    {
        public int ID { get; set; }
        public string Name { get;  set; }
        private Lazy<List<Achievement>> _achievements;

        public DummyUser(int userId, string userName)
        {
            ID = userId;
            Name = userName;

            _achievements = new Lazy<List<Achievement>>(() =>
            {
                // get from "DB" 
                return GetAchievementsByID(this.ID);
            });

        }
        public List<Achievement> Achievements 
        {
            get
            {
                return _achievements.Value;
            }
        }

        public List<Achievement> GetAchievementsByID(int ID)
        {
            return Achievement.GetAchievements();
        }
    }
}

