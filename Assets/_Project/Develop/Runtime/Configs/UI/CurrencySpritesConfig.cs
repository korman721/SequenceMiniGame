using Assets._Project.Develop.Runtime.Meta.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.UI
{
    [CreateAssetMenu(menuName = "Configs/UI/CurrencySpritesConfig", fileName = "CurrencySpritesConfig")]
    public class CurrencySpritesConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyTypeSprite> _currencyTypeSprite;

        public Sprite GetSpriteBy(CurrencyTypes currencyType)
            => _currencyTypeSprite.First(currencyTypeSprite => currencyTypeSprite.CurrencyType == currencyType).Sprite;

        [Serializable]
        private class CurrencyTypeSprite
        {
            [SerializeField] private CurrencyTypes _currencyType;
            [SerializeField] private Sprite _sprite;

            public CurrencyTypes CurrencyType => _currencyType;

            public Sprite Sprite => _sprite;
        }
    }
}
