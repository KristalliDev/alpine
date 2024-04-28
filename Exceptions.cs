namespace alpine
{
    public abstract class Exceptions
    {
        public static Exception NotOwner(string message)
        {
            return new Exception(message); 
        }
    }
}