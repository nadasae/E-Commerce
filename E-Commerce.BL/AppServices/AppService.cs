using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL.AppServices
{
    public abstract class AppService
    {
        protected async Task DoValidationAsync<TValidator, TRequest>(TRequest request, params object[] constructorParameters)
           where TValidator : AbstractValidator<TRequest>
        {

            var instance = (TValidator)Activator.CreateInstance(typeof(TValidator), constructorParameters)!;

            var validateResult = await instance.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                throw new ValidationException(validateResult.Errors);
            }
        }
    }
}
