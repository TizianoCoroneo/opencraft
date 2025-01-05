
# Opencraft

A open-source research platform for modifiable virtual environments

## Usage/Examples

### Switching between Regular and Thin Client

The project has only been tested and ran on Linux and OSX environments

1. Build the go server by running `./build_server`.
1. Start the [Go server](server/go/opencraft-go), which will start listening on port 7979, by simply running
   `./run_server`
2. Start the signaling [web app](https://docs.unity3d.com/Packages/com.unity.renderstreaming@3.1/manual/webapp.html),
   which will set up WebRTC connections, on port 7981, using `./run_signaling`.
3. Open the [Unity client](client/unity/my-first-unity-project/) in the Unity editor twice, using
   [ParrelSync](https://github.com/VeriorPies/ParrelSync), and open the `Client` scene in the local editor and `Remote`
    in the remote editor.
4. Press Play in both editors, first on the remote. If the setup is done correctly, the original editor should connect
    as a client to the server. The cloned editor only starts an HTTP server but does not log in.
5. You can now switch the original editor to a thin client and back. If a policy is selected in the Networking field
    then that will switch, otherwise if not then you can do the following:
   1. To make the original editor switch to become a thin client, make the following web request from the terminal:

      ```sh
      curl "http://localhost:7980/become/thinclient?host=localhost&port=7999&signalingPort=7981"
      ```

   2. Once the original editor has become a thin client, you can make it go back to be a regular client by making the
      following web request, from the terminal:

      ```sh
      curl "http://localhost:7980/become/client?host=localhost&port=7979&playerID=1"
      ```

## Documentation

1. [Unity client](./client/unity/my-first-unity-project/Documentation/Home.md)
2. [Go server](./server/go/opencraft-go/docs/README.md)
