using System;

namespace Match
{
    public static class GameEvents
    {
        public static Action<ItemData> OnItemMatched;
        public static Action OnItemsSpawned;
        public static Action OnMixSkillUsed;
        public static Action OnEasyMatchSkillUsed;
        public static Action OnDoubleItSkillUsed;
        public static Action OnResetSkillUsed;


    }
}
