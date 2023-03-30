using System;
using System.Text;

namespace GameSelector.Model
{
    internal class CardData
    {
        public static CardData Empty
        {
            get
            {
                return new CardData();
            }
        }

        public string CardUID { get; set; } = string.Empty;

        public uint GroupId { get; set; }

        public string GroupName { get; set; } = string.Empty;

        public GameData CurrentGame { get; set; }

        public DateTime StartTime { get; set; } = new DateTime(1970, 1, 1, 0, 0, 0, 0); // unix timestamp 0

        public override string ToString()
        {
            var output = new StringBuilder();
            
            output.AppendLine("CardData {");
            output.AppendLine($"\tGroupId = {GroupId}");
            output.AppendLine($"\tGroupName = {GroupName}");
            output.AppendLine($"\tCurrentGame = {CurrentGame}");
            output.AppendLine($"\tStartTime = {StartTime}");
            output.AppendLine("}");

            return output.ToString();
        }
    }
}
