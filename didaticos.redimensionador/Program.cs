
using System.Drawing;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Iniciando redimensionador");

        Thread thread = new Thread(Redimensionar);
        thread.Start();

        Console.ReadLine();



    }

    static void Redimensionar()
    {
        #region "Diretorios"
        string diretorio_entrada = @"C:\Users\leo15\source\repos\didaticos.redimensionador\didaticos.redimensionador\Arquivos_Entrada";
        string diretorio_finalizado = @"C:\Users\leo15\source\repos\didaticos.redimensionador\didaticos.redimensionador\Arquivos_Finalizados";
        string diretorio_redimensionados = @"C:\Users\leo15\source\repos\didaticos.redimensionador\didaticos.redimensionador\Arquivos_Redimensionados";

        if (!Directory.Exists(diretorio_entrada))
        {
            Directory.CreateDirectory(diretorio_entrada);
        }


        if (!Directory.Exists(diretorio_redimensionados))
        {
            Directory.CreateDirectory(diretorio_redimensionados);
        }


        if (!Directory.Exists(diretorio_finalizado))
        {
            Directory.CreateDirectory(diretorio_finalizado);
        }

        #endregion

        
        {
            //Meu programa vai olhar para a pasta de entrada
            //Se tiver arquivo, ele irá redimensionar

            var arquivosEntrada = Directory.EnumerateFiles(diretorio_entrada);

            //ler o tamanho que irá redimensionar
            int novaAltura = 200;

            foreach (var arquivo in arquivosEntrada)
            {
                FileStream fileStream = new FileStream(arquivo, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                FileInfo fileInfo = new FileInfo(arquivo);

                string caminho = diretorio_redimensionados
                    + @"\" + fileInfo.Name + fileInfo.Extension;

                //Redimensiona
                Redimensionador(Image.FromStream(fileStream), novaAltura, caminho);

                //fecha o arquivo
                fileStream.Close();



                
                fileInfo.MoveTo(diretorio_finalizado + @"\" + fileInfo.Name + fileInfo.Extension);



            }
            //copia os arquivos redimensionados para a pasta de redimensionados

            // Move o arquivo de entrada para a pasta de finalizados


            Thread.Sleep(new TimeSpan(0, 0, 5));

            
        }
    }

    
    static void Redimensionador(Image imagem, int altura, string caminho)
    {
        double ratio = (double)altura / imagem.Height;
        int novaLargura = (int)(imagem.Width * ratio);
        int novaAltura = (int)(imagem.Height * ratio);

        Bitmap novaImage = new Bitmap(novaLargura, novaAltura);

        
        Graphics g = Graphics.FromImage(novaImage);
        g.DrawImage(imagem, 0, 0, novaLargura, novaAltura);



            try
        {
            novaImage.Save(caminho);
        } 
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        

        

        imagem.Dispose();

    }

    


}
