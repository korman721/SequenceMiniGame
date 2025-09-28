using Assets._Project.Develop.Runtime.Meta.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.Wallet
{
    public class WalletPresenter : IPresenter
    {
        private readonly WalletService _wallet;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly ViewsFactory _viewsFactory;
        private readonly ListIconTextView _listIconTextView;

        private Dictionary<CurrencyPresenter, IconTextView> _presenterToView = new();

        public WalletPresenter(
            WalletService wallet,
            ProjectPresentersFactory projectPresentersFactory,
            ViewsFactory viewsFactory,
            ListIconTextView listIconTextView)
        {
            _wallet = wallet;
            _projectPresentersFactory = projectPresentersFactory;
            _viewsFactory = viewsFactory;
            _listIconTextView = listIconTextView;
        }

        public void Initialize()
        {
            foreach(CurrencyTypes type in _wallet.CurrencyTypes)
            {
                IconTextView view = _viewsFactory.CreateView<IconTextView>(ViewID.IconTextView);

                _listIconTextView.Add(view);

                CurrencyPresenter currencyPresenter = _projectPresentersFactory.CreateCurrencyPresenter(
                    _wallet.GetCurrnecy(type),
                    type,
                    view);

                currencyPresenter.Initialize();
                _presenterToView.Add(currencyPresenter, view);
            }
        }

        public void Dispose()
        {
            foreach (KeyValuePair<CurrencyPresenter, IconTextView> presenterToView in _presenterToView)
            {
                _listIconTextView.Remove(presenterToView.Value);
                _viewsFactory.Release(presenterToView.Value);
                presenterToView.Key.Dispose();
            }

            _presenterToView.Clear();
        }
    }
}
