namespace ActualFileStorage.BLL.Salts
{
    public interface ISaltResolver
    {
        string GetSalt(int size);
    }
}