using Assets._Project.Develop.Runtime.Configs.UI;
using Assets._Project.Develop.Runtime.Infrastructer.DI;
using Assets._Project.Develop.Runtime.Meta.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.UI
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public WalletPresenter CreateWalletPresenter(ListIconTextView view)
        {
            return new WalletPresenter(
                _container.Resolve<WalletService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IReadonlyVariable<int> variable,
            CurrencyTypes type,
            IconTextView view)
        {
            return new CurrencyPresenter(
                variable,
                type,
                view,
                _container.Resolve<ConfigsProviderService>().Get<CurrencySpritesConfig>());
        }
    }
}
