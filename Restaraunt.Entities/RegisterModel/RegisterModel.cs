namespace Restaraunt.Entities
{
    #region << Using >>

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class RegisterModel
    {
        #region Properties

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вы не представились")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        #endregion
    }
}