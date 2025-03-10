using System;
using Scripts.Utilities;

namespace Managers
{
    public class ProgressionManager : PersistentSingleton<ProgressionManager>
    {
        public int Xp { get; private set; } = 0;
        public int RequiredXp { get; private set; } = 100;
        public int Level { get; private set; } = 1;

        public event Action<int> OnLevelUp;
        public event Action<int, int> OnXpGained;

        public void AddXp(int amount)
        {
            int originalXp = Xp;
            Xp += amount;
            OnXpGained?.Invoke(originalXp, Xp);

            // While the user gets enough xp to level up
            // This is a while loop in case the overflow is somehow enough to level up again.
            while (Xp >= RequiredXp){
                Level++;

                // Calculate new amount of Xp needed to level up
                UpdateRequiredXp(Level);

                // Any additional earned xp is carried over to the next level.
                int overflow = Xp - RequiredXp;
                Xp = overflow;

                OnLevelUp?.Invoke(Level);
            }
        }

        // This can be altered to change the progression rate.
        private void UpdateRequiredXp(int level)
        {
            RequiredXp = level * 100;
        }
        
        public void ResetAccountProgress()
        {
            Xp = 0;
            Level = 1;
        }
    }
}