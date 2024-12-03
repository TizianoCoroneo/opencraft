/// <summary>
/// Enum to keep track of the client's scenes without having to rely on strings.
/// </summary>
public enum GameScenes
{
    /// <summary>
    /// A client that connects to a server.
    /// </summary>
    Client,

    /// <summary>
    /// A client that connects to another (render) client. The thin client sends
    /// user inputs and receives rendered frames that it plays back for the
    /// user.
    /// </summary>
    ThinClient,
}
