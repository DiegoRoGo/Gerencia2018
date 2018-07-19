using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGerencia.BusinessLogic
{
    public class Encriptacion
    {
        private string chars = "FqwA5e*rt!yZ16ui)oVpSXa]sNd7fG(ghP2jk{BlzODH8x@cvbCn3mQW0ERT.YU4I}_+[JK#LM$^9&'";

        public string Encriptar(string text)
        {
            string code = "";
            for (int i = 0; i < text.Length; i++)
            {
                bool found = false;
                for (int j = 0; j < chars.Length; j++)
                {
                    if(chars[j] == text[i])
                    {
                        int newIndex = j + 13;
                        if (newIndex >= chars.Length)
                            newIndex -= chars.Length;
                        code += chars[newIndex];
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    code += text[i];
                }
            }
            return code;
        }

        public string Desencriptar(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            string code = "";
            for (int i = 0; i < text.Length; i++)
            {
                bool found = false;
                for (int j = 0; j < chars.Length; j++)
                {
                    if (chars[j] == text[i])
                    {
                        int newIndex = j - 13;
                        if (newIndex < 0)
                            newIndex += chars.Length;
                        code += chars[newIndex];
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    code += text[i];
                }
            }
            return code;
        }
    }
}