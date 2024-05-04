using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebScraping.Function
{
    public class StandardJPG
    {
        public static string PadronizeJPG(string linkOriginal)
        {
            Uri uri = new Uri(linkOriginal);

            // Extrair o nome do arquivo da URL
            string nomeArquivo = Path.GetFileName(uri.LocalPath);

            // Montar o novo link com base na lógica fornecida
            string novoLink = $"...{DateTime.Now.Year}/{DateTime.Now.Month:D2}/{nomeArquivo}";

            return novoLink;
        }
        public static void BaixarImagem(string url)
        {
            using (WebClient webClient = new WebClient())
            {

                Uri uri = new Uri(url);
                string nomeArquivo = Path.GetFileName(uri.LocalPath);
                string pastaDestino = "C:\\Users\\ACER\\Downloads\\Organizador\\ScrapingImg\\"; // Defina o caminho da sua pasta aqui

                // Certifique-se de que o diretório de destino exista
                Directory.CreateDirectory(pastaDestino);

                // Combine o caminho da pasta de destino com o nome do arquivo
                string caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

                // Baixe o arquivo para o diretório de destino
                webClient.DownloadFile(url, caminhoCompleto);
                Console.WriteLine("Download realizado com sucesso!");
            }
        }
    }
}