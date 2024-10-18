using Ogani.BLL.Exceptions.Common;

namespace Ogani.BLL.Exceptions
{
    public class InvalidInputException : Exception,IBaseException
    {
        public InvalidInputException(string message = "invalid input") : base(message)
        {
            
        }
    }
}
