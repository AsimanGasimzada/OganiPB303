using Ogani.BLL.Exceptions.Common;

namespace Ogani.BLL.Exceptions;

public class NotFoundException : Exception,IBaseException
{
    public NotFoundException(string message="Not found"):base(message)
    {
        
    }
}
