using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebScraping.Function.ExtractsFunc
{
    public class ExtractNa1
    {
        public static List<string> ParseHtml(HtmlNode nodeElement)
        {
            List<string> elements = new List<string>();

            foreach (HtmlNode node in nodeElement.ChildNodes)
            {
                if (node.Name == "div")
                {

                    if (node.SelectSingleNode("strong") != null)
                    {
                        elements.Add(node.SelectSingleNode("strong").InnerText.Trim(':'));
                        elements.Add(node.InnerText.Substring(node.SelectSingleNode("strong").InnerText.Length).Trim());
                    }
                    else
                    {
                        elements.Add(node.InnerText.Trim());
                    }
                }
            }

            return elements;
        }

    }
}
