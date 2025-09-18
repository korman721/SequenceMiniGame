using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Meta.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private ConfigsProviderService _configProvider;

        public PlayerDataProvider(
            ISaveLoadService saveLoadService,
            ConfigsProviderService configsProvider) : base(saveLoadService)
        {
            _configProvider = configsProvider;
        }

        public override PlayerData GetOriginData()
        {
            return new PlayerData()
            {
                WalletData = InitializeWallet(),
                LossesInGame = 0,
                VictoriesInGame = 0
            };
        }

        private Dictionary<CurrencyTypes, int> InitializeWallet()
        {
            StartWalletConfig startWalletConfig = _configProvider.Get<StartWalletConfig>();

            Dictionary<CurrencyTypes, int> startWallet = new Dictionary<CurrencyTypes, int>()
            {
                { CurrencyTypes.Gold, startWalletConfig.GetValueFor(CurrencyTypes.Gold)}
            };

            return startWallet;
        }
    }
}
