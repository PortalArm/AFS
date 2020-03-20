namespace ActualFileStorage.BLL.Salts
{
    public interface ISaltBuilder
    {
        string GetSalt(int size);
    }
}