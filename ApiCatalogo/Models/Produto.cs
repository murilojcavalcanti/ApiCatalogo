﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage ="Nome é Obrigatorio")]
        [StringLength(80,ErrorMessage ="O nome deve ter até 80 caracteres", MinimumLength =3)]//Define o tamanho em bytes
        public string? Nome { get; set; }
        
        [Required]
        [StringLength(300, ErrorMessage ="o numero maximo de caracteres é {1}")]
        public string? Descricao { get; set; }
        
        [Required]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Preco { get; set; }
        
        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int categoriaId { get; set; }
        
        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
