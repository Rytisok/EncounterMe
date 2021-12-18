﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProgressStats
{
    public DateTime TrailStartTime;
    public double DistanceWalked;
    public string TrailProgress;
    public string TrailTime;
    public double overallDistance;

    public ProgressStats()
    {
        TrailStartTime = DateTime.UtcNow;
        DistanceWalked = 0;
        TrailProgress = "0%";
        TrailTime = "";
        overallDistance = 0;
    }
}
