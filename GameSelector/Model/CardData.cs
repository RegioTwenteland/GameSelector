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
                return new CardData
                {
                    GroupId = 0,
                    GroupName = string.Empty,
                    CurrentGame = string.Empty,
                    StartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0), // unix timestamp 0
                };
            }
        }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string CurrentGame { get; set; }

        public DateTime StartTime { get; set; }

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
