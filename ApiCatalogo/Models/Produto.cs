﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalogo.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required]
        [StringLength(80)]//Define o tamanho em bytes
        public string? Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string? Descrição { get; set; }
        [Required]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Preco { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int categoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
