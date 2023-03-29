using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSelector.Model
{
    internal class GameData
    {
        public uint Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Explanation { get; set; }

        public string Color { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("GameData {");
            sb.AppendLine($"\tId: {Id}");
            sb.AppendLine($"\tCode: {Code}");
            sb.AppendLine($"\tDescription: {Description}");
            sb.AppendLine($"\tExplanation: {Explanation}");
            sb.AppendLine($"\tColor: {Color}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
