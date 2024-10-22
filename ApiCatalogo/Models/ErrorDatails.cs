using System.Text.Json;

namespace ApiCatalogo.Models
{
    //Classe usada para representar is detalhes dos erros
    public class ErrorDatails
    {
        public int StatusCode {  get; set; }
        public string ? Mensage {  get; set; }
        public string? Trace { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
