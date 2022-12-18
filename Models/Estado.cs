using System.ComponentModel.DataAnnotations;
namespace APIMySql.Models
{
	public class Estado
	{
		[Key]
		[StringLength(2, MinimumLength = 2, ErrorMessage = "O campo UF deve ter 2 caracteres")]
		public string UF { get; set; }

		[Required(ErrorMessage = "O campo Nome é obrigatório")]
		[StringLength(200, MinimumLength = 3, ErrorMessage = "O campo Nome deve possuir pelo menos 200 caracteres e no mínimo 3")]
		public string Nome { get; set; }
	}
}

