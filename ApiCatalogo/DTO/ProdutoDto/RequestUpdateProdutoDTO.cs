using ApiCatalogo.Models;
using ApiCatalogo.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiCatalogo.DTO.ProdutoDto;
public class RequestUpdateProdutoDTO:IValidatableObject
{
    [StringLength(80, ErrorMessage = "O nome deve ter até 80 caracteres", MinimumLength = 3)]//Define o tamanho em bytes
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }


    [Range(1,9999,ErrorMessage = "O estoque deve estar entre 1 e 9999")]
    public float Estoque { get; set; }

    public DateTime DataCadastro { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DataCadastro <= DateTime.Now)
        {
            yield return new ValidationResult("A data de cadastro deve ser maior que a data atual!",
                new[] { nameof(this.DataCadastro)});

        }
    }
}
