using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace HumanResources.Business.Base
{
    public class BaseResult<T>
    {
        public T? Data { get; set; }
        public IEnumerable<object> Errors { get; set; }




        public bool IsSuccessful => Errors == null || !Errors.Any();//any deger varsa demek eger yani deger yoksa ve nullsa true olacak

        public bool IsFailure => Errors != null; // errors varsa true olacak



        public static BaseResult<T> Success()
        {
            return new BaseResult<T> { Errors = null };
        }


        //t object
        public static BaseResult<T> Success(T data)//entity döndüreceksek bu metodu kullanýrýz
        {
            return new BaseResult<T> { Data = data };
        }

        public static BaseResult<T> Fail(string errorMessage)
        {
            return new BaseResult<T> { Errors = new[] { new { ErrorMessage = errorMessage, PropertyName = "key" } } };
        }


        public static BaseResult<T> Fail(List<ValidationFailure> errorMessages)
        {

            IEnumerable<object> errors = (from error in errorMessages
                                          select new
                                          {
                                              PropertyName = error.PropertyName,
                                              ErrorMessage = error.ErrorMessage

                                          }).ToList();



            return new BaseResult<T>
            {
                Errors = errors
            };


        }

        public static BaseResult<T> Fail(IEnumerable<IdentityError> identityErrors)
        {

            IEnumerable<object> errors = (from error in identityErrors
                                          select new
                                          {
                                              PropertyName = error.Code,
                                              ErrorMessage = error.Description

                                          }).ToList();



            return new BaseResult<T>
            {
                Errors = errors
            };


        }

    }
}
