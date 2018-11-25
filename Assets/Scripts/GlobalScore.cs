using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalScore {

    static int score;
    static bool scoreSet = false;
    public static bool IsScoreSetOnce()
    {
        return scoreSet;
    }

    public static int Score
    {
        get { return score; }
        set { score = value; scoreSet = true; }
    }
}
