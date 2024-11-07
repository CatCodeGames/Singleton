namespace CatCode
{
    public enum ObjectCreationMode
    {
        FindOnScene = 1 << 0,
        CreateNewInstance = 1 << 1,
        LoadFromResources = 1 << 2,
        All = FindOnScene | CreateNewInstance | LoadFromResources
    }
}