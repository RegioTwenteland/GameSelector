using System.Text;

namespace GameSelector.Model
{
    internal class GameData
    {
        public uint Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Explanation { get; set; }

        public string Color { get; set; }

        public uint Priority { get; set; }

        public CardData OccupiedBy { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("GameData {");
            sb.AppendLine($"\tId: {Id}");
            sb.AppendLine($"\tCode: {Code}");
            sb.AppendLine($"\tDescription: {Description}");
            sb.AppendLine($"\tExplanation: {Explanation}");
            sb.AppendLine($"\tColor: {Color}");
            sb.AppendLine($"\tOccupiedBy: {OccupiedBy.CardUID}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
