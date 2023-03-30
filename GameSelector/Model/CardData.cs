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

        public DateTime? LastInserted { get; set; }

        public override string ToString()
        {
            var output = new StringBuilder();
            
            output.AppendLine("CardData {");
            output.AppendLine($"\tGroupId = {GroupId}");
            output.AppendLine($"\tGroupName = {GroupName}");
            output.AppendLine($"\tLastInserted = {LastInserted}");
            output.AppendLine("}");

            return output.ToString();
        }
    }
}
