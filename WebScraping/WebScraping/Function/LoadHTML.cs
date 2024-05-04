using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraping.Function
{
    public class LoadHTML
    {
        public HtmlDocument LoadHtmlDocument(string url, HtmlWeb web)
        {
            try
            {
                Uri uriResult;
                if (Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    HtmlDocument document = web.Load(url);

                    return document;
                }
                else
                {
                    Console.WriteLine($"URL Inválida: {url}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar documento HTML de {url}: {ex.Message}");
                return null;
            }
        }
    }
}