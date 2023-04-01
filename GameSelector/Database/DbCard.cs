using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace GameSelector.Database
{
    [Table(Name = "cards")]
    internal class DbCard
    {
        [Column(Name = "card_id", IsPrimaryKey = true)]
        public string Id { get; set; }

        [Column(Name = "group_id")]
        public long GroupId { get; set; }

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

        public override string ToString()
        {
            return $"(Card: Id = {Id}, GroupId = {GroupId}, ScoutingName = {ScoutingName})";
        }
    }
}
