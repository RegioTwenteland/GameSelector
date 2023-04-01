using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace GameSelector.Database
{
    [Table(Name = "games")]
    internal class DbGame
    {
#pragma warning disable CS0649 // Field 'DbGame._id' is never assigned to, and will always have its default value 0
        // Reason: this field is written to by the ORM
        private long _id;
#pragma warning restore CS0649 // Field 'DbGame._id' is never assigned to, and will always have its default value 0

        private EntityRef<DbCard> _card = new EntityRef<DbCard>();

        [Column(Storage = "_id", Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id { get => _id; }

        [Column(Name = "code")]
        public string Code { get; set; }

        [Column(Name = "description")]
        public string Description { get; set; }

        [Column(Name = "explanation")]
        public string Explanation { get; set; }

        [Column(Name = "color")]
        public string Color { get; set; }

        [Column(Name = "priority")]
        public long Priority { get; set; }

        [Column(Name = "occupied_by")]
        public string OccupiedById { get; set; }

        [Association(Storage = "_card", ThisKey = "OccupiedById", IsForeignKey = true)]
        public DbCard OccupiedBy
        {
            get => _card.Entity;
            set => _card.Entity = value;
        }

        [Column(Name = "start_time")]
        public long? StartTime { get; set; }

        public override string ToString()
        {
            return $"(Game: {Id}, {Code}, {Description}, {Explanation}, {Color}, {Priority}, {OccupiedById}, {OccupiedBy})";
        }
    }
}
