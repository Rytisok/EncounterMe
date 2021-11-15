using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me.DataClasses
{
    public class Achievement
    {
        public int AchievementID { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public bool IsUnlocked { get; set; }
        public int CountToUnlock { get; set; }

        //condition - for example exp > 1000 or finished trails count = 5 

        public Achievement(int Id, string name, string imgUrl = null, bool isUnclocked = false)
        {
            AchievementID = Id;
            Name = name;
            ImgUrl = imgUrl;
        }  


        public static List<Achievement> GetAchievements()
        {
            //var ach1 = new Lazy<Achievement>(() => new Achievement(Id: 1, "First Steps"));
            //var ach2 = new Lazy<Achievement>(() => new Achievement(Id: 2, "Curious fellow"));
            //var ach3 = new Lazy<Achievement>(() => new Achievement(Id: 3, "Puzle Solver"));
            var ach1 = new Achievement(Id: 1, "Took First Steps", @"https://www.kindpng.com/picc/m/721-7210152_mq-footsteps-footstep-rainbow-rainbows-colorful-shoe-print.png");
            var ach2 = new Achievement(Id: 2, "I Found A Curious Fellow", @"https://cdn.pixabay.com/photo/2019/04/17/12/35/black-cat-is-curious-4134137_960_720.png");
            var ach3 = new Achievement(Id: 3, "It's a secret...", @"https://www.pinclipart.com/picdir/big/558-5589018_king-crown-png-image-vector-king-crown-png.png");
            //var ach4 = new Achievement(Id: 4, "True traveler", @"https://www.pngfind.com/pngs/m/88-885555_trees-clipart-hd-png-download.png");
            return new List<Achievement>{ ach1, ach2, ach3};
        }

    }


    public enum AchievementType
    { 
        Login,
        GainExpPoints,
        Lvl,
        FinishTrail
    };

}
