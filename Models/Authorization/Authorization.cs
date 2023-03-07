using System.ComponentModel.DataAnnotations;

namespace TrainRepairCRUDWebApplication.Models.Authorization
{
    public class Authorization
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо ввести логин")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Необходимо ввести пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? RoleName { get; set; }
    }
}
