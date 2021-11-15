using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.DataClasses
{
    public class AchievementEventArg : EventArgs
    {
        public Achievement Data;

        public AchievementEventArg(Achievement e)
        {
            Data = e;
        }
    }

    public class AchievementsManagerService
    {
        private Dictionary<AchievementType, List<Achievement>> _achievements;
        private Dictionary<AchievementType, int> _achievementKeeper;

        public event EventHandler AchievementUnlocked;

        protected virtual void RaiseAchievementUnlocked(Achievement ach)
        {
            ach.IsUnlocked = true;
            var del = AchievementUnlocked as EventHandler;
            if (del != null)
            {
                del(this, new AchievementEventArg(ach));
            }
        }

        public AchievementsManagerService()
        {
            _achievementKeeper = new Dictionary<AchievementType, int>();
            _achievementKeeper.Add(AchievementType.Login, 0);
            // here should load previous, saved values

            _achievements = new Dictionary<AchievementType, List<Achievement>>();

            var listLogin = new List<Achievement>();
            listLogin.Add(new Achievement(1, "Welcome!"));
            listLogin.Add(new Achievement(5, "Fifth time here huh?"));

            var listTrailFin = new List<Achievement>();
            listTrailFin.Add(new Achievement(1, "Finished first trail! A great journey begins with first few steps."));
            listTrailFin.Add(new Achievement(5, "Finished fifth trail. Five times further - getting used to it yet?"));

            var listExp = new List<Achievement>();
            listExp.Add(new Achievement(1000, "First Thousand points"));

            //var listLvl = new List<Achievement>();
            //listLvl.Add(new Achievement(null, 5, "Not a begginer anymore"));



            _achievements.Add(AchievementType.Login, listLogin);
            _achievements.Add(AchievementType.FinishTrail, listTrailFin);
            _achievements.Add(AchievementType.GainExpPoints, listExp);
            //_achievements.Add(AchievementType.Lvl, listLvl);
        }

        public void RegisterEvent(AchievementType type)
        {
            if (!_achievementKeeper.ContainsKey(type))
            {
                return;
            }
            _achievementKeeper[type]++;
            ParseAchievements(type);
        }


        public void ParseAchievements(AchievementType type)
        {
            foreach (var keyValPair in _achievements.Where(a => a.Key == type))
            {
                foreach (var ach in keyValPair.Value.Where(a => a.IsUnlocked == false))
                {
                    /*if (type == AchievementType.GainExpPoints)
                    {
                        if (_achievementKeeper[type] >= ach.CountToUnlock)
                            RaiseAchievementUnlocked(ach);

                    }*/
                    if (_achievementKeeper[type] == ach.AchievementID)
                    {
                        RaiseAchievementUnlocked(ach);
                    }
                }
            }
        }

    }
}
