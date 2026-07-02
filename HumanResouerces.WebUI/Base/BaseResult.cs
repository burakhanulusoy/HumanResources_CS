namespace HumanResouerces.WebUI.Base
{
    public class BaseResult<T>
    {

        public T? Data { get; set; }
        public List<Error> Errors { get; set; }


        public bool IsSuccessful { get; set; }
        public bool IsFailure { get; set; }

    }

    public class Error
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }


    }
}
