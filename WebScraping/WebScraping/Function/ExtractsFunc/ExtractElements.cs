using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebScraping.Model;

namespace WebScraping.Function.ExtractsFunc
{
    public class ExtractElements
    {
        public static List<Itens> ExtractProductsFromPage(HtmlDocument document)
        {
            List<Itens> products = new List<Itens>();
            var productNodess = document.DocumentNode.SelectNodes("//div[@class='container-fluid p-lg-5 p-3 mt-2 m-lg-0']");
            foreach (var node in productNodess)
            {
                string title = node.SelectSingleNode(".//div[@class='col-lg-6 px-lg-5']//h3[@class='text-uppercase']")
                                      ?.InnerText.Trim()?.Replace("#39;", "'").Replace("&quot;", "").Replace("&", "") ?? "";

                var selectNode = document.DocumentNode.SelectSingleNode("//select[@id='attribute_value_id']");
                string tipo = "";
                if (selectNode != null)
                {
                    var optionNodes = selectNode.SelectNodes(".//option[@value != '']/text()");
                    if (optionNodes != null && optionNodes.Count > 0)
                    {
                        tipo = "Variation";
                        title = string.Join(", ", optionNodes.Select(optionNode => optionNode.InnerText.Trim()));
                    }
                    else
                    {
                        tipo = "Simple";
                    }
                }
                else
                {
                    tipo = "Simple";
                }

                string price = node.SelectSingleNode(".//div[@class='col-md-6 my-3 d-flex flex-column']/h6")
                                      ?.InnerText.Trim().Replace("$", "").Replace("TAX", "").Replace("FREE", "").Replace("*", "") ?? "";

                string brand = node.SelectSingleNode(".//h6[contains(@class,'mt-4 text-secondary')]/a")
                                      ?.InnerText.Trim() ?? "";

                string categorias = node.SelectSingleNode(".//a[@class=\"breadcrumb-item text-uppercase\"]")
                                      ?.InnerText.Trim() ?? "";

                string img = "";
                var carouselInnerNode = document.DocumentNode.SelectSingleNode("//div[@class='carousel-inner']");
                if (carouselInnerNode != null)
                {
                    //StringBuilder im = new StringBuilder();
                    string im = "";

                    var imgNodes = carouselInnerNode.SelectNodes(".//div[contains(@class,'carousel-item')]/a/img");

                    if (imgNodes != null && imgNodes.Count > 0)
                    {
                        foreach (var imgNode in imgNodes)
                        {
                            string imgSrc = imgNode.Attributes["src"].Value;
                            im = StandardJPG.PadronizeJPG(imgSrc);
                            StandardJPG.BaixarImagem(imgSrc);

                            Console.WriteLine(im);

                            im = string.Concat(im, ", ");
                        }
                        img = im.ToString();
                    }
                }

                string description = node.SelectSingleNode(".//div[@class='trix-content']/div")
                                      ?.InnerText.Trim() ?? "";

                string[] Na1 = [];
                HtmlNode trixContentDiv = document.DocumentNode.SelectSingleNode("(//div[@class='trix-content'])[2]");

                if (trixContentDiv != null)
                {
                    List<string> elements = ExtractNa1.ParseHtml(trixContentDiv);
                    Na1 = elements.ToArray();
                }



                products.Add(new Itens
                {
                    Title = title,
                    Preco = price,
                    Brand = brand,
                    Description = description,
                    Tipo = tipo,
                    Categorias = categorias,
                    Imagens = img,
                    Na1 = Na1,
                    EmEstoque = "1"
                });
            }
            return products;
        }
    }
}