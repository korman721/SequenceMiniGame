using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Utilities.GamesManagment.GamesCounterService
{
    public class GamesCounterService : IGamesCounter, IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private ReactiveVariable<int> _lossesCount;
        private ReactiveVariable<int> _victoriesCount;

        public IReadonlyVariable<int> Victories => _victoriesCount;

        public IReadonlyVariable<int> Losses => _lossesCount;

        public GamesCounterService(PlayerDataProvider playerDataProvider)
        {
            _lossesCount = new ReactiveVariable<int>();
            _victoriesCount = new ReactiveVariable<int>();

            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public void Victory() => _victoriesCount.Value++;

        public void Loss() => _lossesCount.Value++;

        public void Reset()
        {
            _lossesCount.Value = 0;
            _victoriesCount.Value = 0;
        }

        public void ReadFrom(PlayerData data)
        {
            _lossesCount.Value = data.LossesInGame;
            _victoriesCount.Value = data.VictoriesInGame;
        }

        public void WriteTo(PlayerData data)
        {
            data.LossesInGame = _lossesCount.Value;
            data.VictoriesInGame = _victoriesCount.Value;
        }
    }
}
