using HumanResouerces.WebUI.Base;

namespace HumanResouerces.WebUI.Exceptions
{
    public class ApiValidationException(List<Error> errors) : Exception("API tarafından validasyon hatası alındı")
    {
        public List<Error> Errors { get; } = errors;



    }
}
