using HtmlAgilityPack;
using WebScraping.Function;
using WebScraping.Function.ExtractsFunc;
using WebScraping.Model;

namespace WebScraping
{

    class Program
    {
        
        public static HtmlWeb web = new HtmlWeb();
        public static LoadHTML load = new LoadHTML();

        static void Main(string[] args)
        {
            //Links para Extração
            List<(string Url, string FileName)> urls = new List<(string, string)>
            {

            };

            foreach (var (url, fileName) in urls)
            {
                List<Itens> produtos = new List<Itens>();

                HtmlDocument document = load.LoadHtmlDocument(url, web);

                if (document != null)
                { 

                    List<Itens> currentPageProducts = ExtractElements.ExtractProductsFromPage(document);
                            produtos.AddRange(currentPageProducts);

                    foreach(var product in currentPageProducts)
                    {
                        Console.WriteLine(product.ToString());
                    }
                    
                }

                string csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                CSVWrite.WriteToCsv(produtos, csvFilePath);

                Console.WriteLine("Dados gravados com Sucesso em: ", fileName);
                
            }
            
        }
    }
}