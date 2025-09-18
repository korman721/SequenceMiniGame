namespace Assets._Project.Develop.Runtime.Utilities.GamesManagment.GamesCounterService
{
    public interface IGamesCounter
    {
        void Victory();
        void Loss();
        void Reset();
        int Victories { get; }
        int Losses { get; }
    }
}
