namespace MyRecipeBook.Exceptions.ExceptionsBase;

public class ErrorOnValidationException ( IList<string> errorsMessages ) : MyRecipeBookException
{
    public IList<string> ErrorsMessages { get; set; } = errorsMessages;
}