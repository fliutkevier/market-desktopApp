using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DtoValidator
    {
        public static void Validate(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            if (!isValid)
            {
                //muestra todos los mensajes de error juntos
                var errorMessages = validationResults.Select(r => r.ErrorMessage);
                var fullErrorMessage = string.Join("\n", errorMessages);

                throw new ValidationException(fullErrorMessage);
            }
        }
    }
}
