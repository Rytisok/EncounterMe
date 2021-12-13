using Encounter_Me;
using System;

public static class LevelAndXp
{
	public static int XpToLevelUp(int level)
	{
		return (level * 20) + 100;
	}

	public static int XpPercentageOfLevel(int level, int xp)
    {
		int percentage = (int)((double)xp / (double)XpToLevelUp(level) * 100);
		return percentage;
    }
	public static int XpFromTrail(CapturePoint capturePoint)
    {
		return (int)(50 + (20 * capturePoint.DefenseLevel));
    }
}
