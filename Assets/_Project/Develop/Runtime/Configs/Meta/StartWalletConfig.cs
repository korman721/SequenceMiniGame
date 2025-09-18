using Assets._Project.Develop.Runtime.Meta.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta
{
    [CreateAssetMenu(menuName = "Configs/Meta/StartWalletConfig", fileName = "StartWalletConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [field: SerializeField] private List<Currency> _currencies;

        public int GetValueFor(CurrencyTypes currencyType)
            => _currencies.First(currency => currency.CurrencyType == currencyType).Value;

        [Serializable]
        private class Currency
        {
            [field: SerializeField] public CurrencyTypes CurrencyType { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}
