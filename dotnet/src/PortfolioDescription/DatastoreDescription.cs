namespace System {
  public class DatastoreDescription {
    public string Key { get; set; } = "";

    /// <summary>
    /// 'localstore' | 'fuse' 
    /// </summary>
    public string ProviderClass { get; set; } = "fuse";
    public object ProviderArguments { get; set; }
  }
}