using Match.View;
using UnityEngine;

namespace Match
{
    public class GameManager : MonoBehaviour
    {
        // singleton instance
        public static GameManager Instance;

        public ItemSpawner itemSpawner;

        // Declare skill objects
        public UIController uiController;
        public MixSkill mixSkill;
        public DoubleItSkill doubleItSkill;
        public ResetSkill resetSkill;
        public EasyMatchSkill easyMatchSkill;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            // Initialize scene objects
            itemSpawner.SpawnObjects();

            // Initialize skills (without itemSpawner if not needed)
            mixSkill.Initialize(itemSpawner);
            //doubleItSkill.Initialize(uiController);
            //resetSkill.Initialize(itemSpawner, uiController);
            //easyMatchSkill.Initialize();
        }
    }
}
