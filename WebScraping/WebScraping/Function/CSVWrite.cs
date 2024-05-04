using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using WebScraping.Model;

namespace WebScraping.Function
{
    public class CSVWrite
    {
        public static void WriteToCsv(List<Itens> products, string filePath)
        {
            bool fileExists = File.Exists(filePath);

            var csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);

            using (var writer = new StreamWriter(filePath, true, Encoding.UTF8))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                // Escrever cabeçalho

                if (!fileExists)
                {
                    writer.WriteLine(
                        "Tipo," +
                        "Ref," +
                        "Nome," +
                        "Marca," +
                        "Descrição," +
                        "Em estoque?," +
                        "Estoque," +
                        "Preço Promocional," +
                        "Preço," +
                        "Categorias," +
                        "Tags," +
                        "Imagens," +
                        "Produto Ascendente," +
                        "Nome do atributo 1," +
                        "Valores do atributo 1," +
                        "Nome do atributo 2," +
                        "Valores do atributo 2," +
                        "Nome do atributo 3," +
                        "Valores do atributo 3," +
                        "Nome do atributo 4," +
                        "Valores do atributo 4," +
                        "Nome do atributo 5," +
                        "Valores do atributo 5," +
                        "Nome do atributo 6," +
                        "Valores do atributo 6," +
                        "Nome do atributo 7," +
                        "Valores do Atributo 7," +
                        "Nome do atributo 8," +
                        "Valor do atributo 8," +
                        "Nome do atributo 9," +
                        "Valor do atributo 9," +
                        "Nome do atributo 10," +
                        "Valor do atributo 10,");
                    writer.WriteLine();
                }

                // Escrever dados
                foreach (var product in products)
                {

                    // Escrevendo os campos restantes do produto
                    csv.WriteField(product.Tipo ?? "");
                    csv.WriteField(product.REF ?? "");
                    csv.WriteField(product.Title ?? "");
                    csv.WriteField(product.Brand ?? "");
                    csv.WriteField(product.Description ?? "");
                    csv.WriteField(product.EmEstoque ?? "");
                    csv.WriteField(product.Estoque ?? "");
                    csv.WriteField(product.PrecoPromo ?? "");
                    csv.WriteField(product.Preco.Replace("\"", "\"\"") ?? "");
                    csv.WriteField(product.Categorias ?? "");
                    csv.WriteField(product.Tags ?? "");
                    csv.WriteField(product.Imagens ?? "");
                    csv.WriteField(product.ProdAsc ?? "");

                    int teste = product.Na1.Length;

                    for (int i = 0; i < teste; i++)
                    {
                        if (product.Na1[i] != "")
                        {
                            string field = product.Na1[i].Trim().Replace(":", "").Replace(": ", "").Replace(" :", "").Replace("-", "").Replace("- ", "").Replace(" -", "").Replace(" -", "") ?? "";

                            if (field != "Beneficios")
                            {
                                csv.WriteField(field);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }



                }
                Console.WriteLine("Deu certo o teste");

                // Escrever csvContent em um arquivo CSV ou fazer o que for necessário com ele

                writer.WriteLine();
                csv.NextRecord();
            }
        }
    }
}
