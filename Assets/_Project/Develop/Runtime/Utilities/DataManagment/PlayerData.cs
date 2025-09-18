using Assets._Project.Develop.Runtime.Meta.Wallet;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataKeysStorage;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;

        public int LossesInGame;
        public int VictoriesInGame;
    }
}
