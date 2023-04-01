using System.Data.Linq;

namespace GameSelector.Database
{
    internal interface IDatabase
    {
        Table<DbCard> CardTable { get; }

        Table<DbGame> GameTable { get; }

        Table<DbPlayedGame> PlayedGameTable { get; }

        DataContext DataContext { get; }
    }
}
