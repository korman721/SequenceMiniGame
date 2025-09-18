using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta
{
    [CreateAssetMenu(menuName = "Configs/Meta/LossesVictoriesSettingsConfig", fileName = "LossesVictoriesSettingsConfig")]
    public class LossesVictoriesSettingsConfig : ScriptableObject
    {
        [field: SerializeField] public int AddGoldForVictory { get; private set; } = 20;
        [field: SerializeField] public int SpendGoldForLoss { get; private set; } = 10;
        [field: SerializeField] public int ReloadPrice { get; private set; } = 50;
    }
}
