using System.Collections.Generic;

namespace SharedLibrary.Dto
{
    public class ErrorDto
    {
        public List<string> Errors { get; private set; }
        public bool IsShow { get; set; }

        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public ErrorDto(string error, bool isShow)
        {
            Errors.Add(error);
            isShow = true;
        }

        public ErrorDto(List<string> errors, bool isShow)
        {
            //Errors = errors
            Errors = errors;
            IsShow = isShow;
        }
    }
}