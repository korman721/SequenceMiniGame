using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Meta.Wallet
{
    public class WalletService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private Dictionary<CurrencyTypes, ReactiveVariable<int>> _currencies;

        public WalletService(
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currenices,
            PlayerDataProvider dataProvider)
        {
            _currencies = currenices;
            dataProvider.RegisterWriter(this);
            dataProvider.RegisterReader(this);
        }

        public IReadonlyVariable<int> GetCurrnecy(CurrencyTypes currencyType) => _currencies[currencyType];

        public void Add(CurrencyTypes currency, int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value) + " wrong argument");

            if (_currencies.ContainsKey(currency))
                _currencies[currency].Value += value;
        }

        public void Spend(CurrencyTypes currency, int value)
        {
            if (IsEnough(currency, value) == false)
                throw new InvalidOperationException("Not Enough: " + currency.ToString());

            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            if (_currencies.ContainsKey(currency))
                _currencies[currency].Value -= value;
        }

        private bool IsEnough(CurrencyTypes currency, int value) => _currencies[currency].Value - value >= 0;

        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<CurrencyTypes, int> currencies in data.WalletData)
            {
                if (_currencies.ContainsKey(currencies.Key))
                    _currencies[currencies.Key].Value = currencies.Value;
                else
                    _currencies.Add(currencies.Key, new ReactiveVariable<int>(currencies.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (KeyValuePair<CurrencyTypes, ReactiveVariable<int>> currencies in _currencies)
            {
                if (data.WalletData.ContainsKey(currencies.Key))
                    data.WalletData[currencies.Key] = currencies.Value.Value;
                else
                    data.WalletData.Add(currencies.Key, currencies.Value.Value);
            }
        }
    }
}
