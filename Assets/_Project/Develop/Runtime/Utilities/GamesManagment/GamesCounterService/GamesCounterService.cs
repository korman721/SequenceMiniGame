using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;

namespace Assets._Project.Develop.Runtime.Utilities.GamesManagment.GamesCounterService
{
    public class GamesCounterService : IGamesCounter, IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private int _lossesCount;
        private int _victoriesCount;

        public int Victories => _victoriesCount;

        public int Losses => _lossesCount;

        public GamesCounterService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public void Victory() => _victoriesCount++;

        public void Loss() => _lossesCount++;

        public void Reset()
        {
            _lossesCount = 0;
            _victoriesCount = 0;
        }

        public void ReadFrom(PlayerData data)
        {
            _lossesCount = data.LossesInGame;
            _victoriesCount = data.VictoriesInGame;
        }

        public void WriteTo(PlayerData data)
        {
            data.LossesInGame = _lossesCount;
            data.VictoriesInGame = _victoriesCount;
        }
    }
}
