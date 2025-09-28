using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Utilities.GamesManagment.GamesCounterService
{
    public interface IGamesCounter
    {
        void Victory();
        void Loss();
        void Reset();
        IReadonlyVariable<int> Victories { get; }
        IReadonlyVariable<int> Losses { get; }
    }
}
