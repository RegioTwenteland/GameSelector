using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace GameSelector.Database
{
    [Table(Name = "played_games")]
    internal class DbPlayedGame
    {
#pragma warning disable CS0649 // Field 'DbGame._id' is never assigned to, and will always have its default value 0
        // Reason: this field is written to by the ORM
        private long _id;
#pragma warning restore CS0649 // Field 'DbGame._id' is never assigned to, and will always have its default value 0

        private EntityRef<DbCard> _player = new EntityRef<DbCard>();
        private EntityRef<DbGame> _game = new EntityRef<DbGame>();

        [Column(Storage = "_id", Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id { get => _id; }

        [Column(Name = "player")]
        public string PlayerId { get; set; }

        [Association(Storage = "_player", ThisKey = "PlayerId", IsForeignKey = true)]
        public DbCard Player
        {
            get => _player.Entity;
            set => _player.Entity = value;
        }

        [Column(Name = "game")]
        public long GameId { get; set; }

        [Association(Storage = "_game", ThisKey = "GameId", IsForeignKey = true)]
        public DbGame Game
        {
            get => _game.Entity;
            set => _game.Entity = value;
        }

        [Column(Name = "start_time")]
        public long StartTime { get; set; }

        [Column(Name = "end_time")]
        public long EndTime { get; set; }

        public override string ToString()
        {
            return $"(PlayedGame: {Id}, {PlayerId}, {Player}, {GameId}, {Game}";
        }
    }
}
