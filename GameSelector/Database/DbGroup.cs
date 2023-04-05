using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace GameSelector.Database
{
    [Table(Name = "groups")]
    internal class DbGroup
    {
        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id { get; set; }

        [Column(Name = "card_id")]
        public string CardId { get; set; }

        [Column(Name = "group_name")]
        public string GroupName { get; set; }

        [Column(Name = "scouting_name")]
        public string ScoutingName { get; set; }

#pragma warning disable CS0649 // Field 'DbGame._id' is never assigned to, and will always have its default value 0
        // Reason: this field is written to by the ORM
        private EntitySet<DbPlayedGame> _playedGames;
#pragma warning restore CS0649 // Field 'DbGame._id' is never assigned to, and will always have its default value 0

        [Association(Storage = "_playedGames", ThisKey = "Id", OtherKey = "PlayerId")]
        public EntitySet<DbPlayedGame> PlayedGames
        {
            get => _playedGames ?? new EntitySet<DbPlayedGame>();
            set => _playedGames.Assign(value);
        }
    }
}
