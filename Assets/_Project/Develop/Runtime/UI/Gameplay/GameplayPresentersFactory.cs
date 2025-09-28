using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using Assets._Project.Develop.Runtime.Infrastructer.DI;
using Assets._Project.Develop.Runtime.UI.CommonViews;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;

        public GameplayPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public PlayerInputPresenter CreatePlayerInputPresenter(TextView view)
        {
            return new PlayerInputPresenter(
                view,
                _container.Resolve<PlayerInput>());
        }

        public SequencePresenter CreateSequencePresenter(TextView view)
        {
            return new SequencePresenter(
                view,
                _container.Resolve<SequenceChecker>());
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
        {
            return new GameplayScreenPresenter(
                view,
                this,
                _container.Resolve<SequenceChecker>());
        }
    }
}
