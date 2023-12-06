using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication26.Models
{
    public partial class Trabajadore
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo TipoDocumento es obligatorio.")]
        public string? TipoDocumento { get; set; }
        [Required(ErrorMessage = "El campo Numero de Documento.")]
        [RegularExpression("^[0-8]{8}$", ErrorMessage = "El número de documento debe contener 8 dígitos.")]
        public string? NumeroDocumento { get; set; }
        [Required(ErrorMessage = "El campo Nombres es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Solo se permiten letras en el campo Nombres.")]
        public string? Nombres { get; set; }
        [Required(ErrorMessage = "El campo TipoDocumento es obligatorio.")]
        public string? Sexo { get; set; }
        [Required(ErrorMessage = "El campo TipoDocumento es obligatorio.")]
        public int? IdDepartamento { get; set; }
        [Required(ErrorMessage = "El campo TipoDocumento es obligatorio.")]
        public int? IdProvincia { get; set; }
        [Required(ErrorMessage = "El campo TipoDocumento es obligatorio.")]
        public int? IdDistrito { get; set; }

        public virtual Departamento? IdDepartamentoNavigation { get; set; }
        public virtual Distrito? IdDistritoNavigation { get; set; }
        public virtual Provincium? IdProvinciaNavigation { get; set; }
    }
}