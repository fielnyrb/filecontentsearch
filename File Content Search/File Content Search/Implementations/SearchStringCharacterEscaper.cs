using File_Content_Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Implementations
{
    internal class SearchStringCharacterEscaper : ICharacterEscaper
    {
        public string Apply(string text)
        {
            string tempText = text.Replace("Á", "[\\u0193 ?]");
            tempText = tempText.Replace("á", "[\\u0225 ?]");
            tempText = tempText.Replace("Ý", "[\\u0221 ?]");
            tempText = tempText.Replace("ý", "[\\u0253 ?]");
            tempText = tempText.Replace("Ú", "[\\u0218 ?]");
            tempText = tempText.Replace("ú", "[\\u0250 ?]");
            tempText = tempText.Replace("Í", "[\\u0205 ?]");
            tempText = tempText.Replace("í", "[\\u0237 ?]");
            tempText = tempText.Replace("Ó", "[\\u0211 ?]");
            tempText = tempText.Replace("ó", "[\\u0243 ?]");
            tempText = tempText.Replace("Ð", "[\\u0208 ?]");
            tempText = tempText.Replace("ð", "[\\u0240 ?]");
            tempText = tempText.Replace("Æ", "[\\u0298 ?]");
            tempText = tempText.Replace("æ", "[\\u0230 ?]");
            tempText = tempText.Replace("Ø", "[\\u0216 ?]");
            tempText = tempText.Replace("ø", "[\\u0248 ?]");

            return tempText;
        }
    }
}
