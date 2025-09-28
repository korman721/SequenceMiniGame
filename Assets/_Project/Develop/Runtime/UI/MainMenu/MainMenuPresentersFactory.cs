using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Infrastructer.DI;
using Assets._Project.Develop.Runtime.Meta.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.GamesCounter;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.GamesManagment.GamesCounterService;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuPresentersFactory
    {
        private readonly DIContainer _container;

        public MainMenuPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuScreenPresenter CreateMainMenuPresenter(MainMenuScreenView view)
        {
            return new MainMenuScreenPresenter(
                view,
                _container.Resolve<ProjectPresentersFactory>(),
                this,
                _container.Resolve<WalletService>(),
                _container.Resolve<IGamesCounter>(),
                _container.Resolve<ConfigsProviderService>().Get<LossesVictoriesSettingsConfig>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<PlayerDataProvider>());
        }

        public MainMenuGameTypePresenter CreateMainMenuGameTypePresenter(
            TextView view, 
            IReadonlyVariable<int> variable,
            string commonText = null)
        {
            return new MainMenuGameTypePresenter(
                view,
                variable,
                commonText);
        }

        public GamesCounterPresenter CreateGamesCounterPresenter(ListTextView view)
        {
            return new GamesCounterPresenter(
                _container.Resolve<ViewsFactory>(),
                this,
                _container.Resolve<IGamesCounter>(),
                view);
        }
    }
}
