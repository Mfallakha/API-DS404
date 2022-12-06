namespace ProjetoEscola_API.Models
{
    public class Produto
    {
        public int id {get; set;}

        public int codProduto {get; set;}

        public string? nomeProduto { get; set;}

        public string? valorProduto { get; set;}

         public string? imagem { get; set;}
          public string? compradoPor { get; set;}

        public Boolean? comprado {get; set;}
    }
}