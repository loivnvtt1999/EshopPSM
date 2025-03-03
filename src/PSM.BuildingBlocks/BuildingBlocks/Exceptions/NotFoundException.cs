namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string name, object key) : base($"Entity \"({name})\" ({key}) was not found")
        {
        }
    }
}
