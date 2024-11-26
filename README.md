
# Opencraft

A open-source research platform for modifiable virtual environments

## Usage/Examples

### Switching between Regular and Thin Client

1. Start the [Go server](server/go/opencraft-go), which will start listening on port 7979, by simply running
   `.\opencraft-go.exe`
2. Start the signaling [web app](https://docs.unity3d.com/Packages/com.unity.renderstreaming@3.1/manual/webapp.html),
   which will set up WebRTC connections, on port 7981, using `.\webserver.exe -p 7981`.
3. Open the [Unity client](client/unity/my-first-unity-project/) in the Unity editor twice, using
   [ParrelSync](https://github.com/VeriorPies/ParrelSync), and open the `Client` scene in both editors.
4. In the cloned editor, select the `Bootstrap` game object in the pane on the left-hand side, and configure the `Extra Arguments` in the pane on the right-hand side to be:
   1. `--noLogin`
   2. `--httpPort`
   3. `7999`
5. Do the same for the original editor, but configure the options to be:
   1. `--host`
   2. `localhost`
   3. `--port`
   4. `7979`
   5. `--user`
   6. `1`
6. Press Play in both editors. If the setup is done correctly, the original editor should connect as a client to the
   server. The cloned editor only starts an HTTP server but does not log in.
7. You can now switch the original editor to a thin client and back.
   1. To make the original editor switch to become a thin client, make the following web request from the terminal:
      ```powershell
      Invoke-WebRequest -URI "http://localhost:7980/become/thinclient?host=localhost&port=7999&signalingPort=7981"
      ```
   2. Once the original editor has become a thin client, you can make it go back to be a regular client by making the
      following web request, from the terminal:
      ```powershell
      Invoke-WebRequest -URI "http://localhost:7980/become/client?host=localhost&port=7979&playerID=1"
      ```
