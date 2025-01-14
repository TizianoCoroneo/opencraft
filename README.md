
# Opencraft

An open-source research platform for modifiable virtual environments.

## Notes

The project has only been tested and ran on Linux a environment, and partially on a OSX environments. Running on windows
 might not work as it is untested & most commands are shellscripts, not powershell.

## Usage/Run experiment

To run the experiment you first need to set up the remote/thin-client, which can be done by running (on the remote):

```sh
./run_remote.sh
```

And selecting & running the `Remote` scene in the Unity editor that pops up (found under `Scenes/Client`).

After this you can run the client, which performs the actual measurements of the metrics during a test where the client
 will walk in a circle for 60 seconds. This can be done by running:

```sh
./run_client.sh
```

Where You then select one of the following supported policies under `Networking -> PolicyManager` :

```md
* ThresholdNoPolicy
* Threshold RTT Policy
* Threshold System Policy
* Threshold Combo Policy
```

Additionally, also set the host to the local network ipv4 address of the remote device. Then, after running the test the
 program will have outputted a csv file with the measured results. To get and plot it, run the following commands:

```sh
cd stats
./fetch_csv.sh
python3 plot.py "your chosen csv"
```

This will plot the relevant metric data for the client, after which you can export this graph.

## Manual setup && Switching between Regular and Thin Client

The project has only been tested and ran on Linux and OSX environments

1. Build the go server by running `./build_server`.
2. Start the [Go server](server/go/opencraft-go), which will start listening on port 7979, by simply running
   `./run_server`
3. Start the signaling [web app](https://docs.unity3d.com/Packages/com.unity.renderstreaming@3.1/manual/webapp.html),
   which will set up WebRTC connections, on port 7981, using `./run_signaling`.
4. Open the [Unity client](client/unity/my-first-unity-project/) in the Unity editor twice, using
   [ParrelSync](https://github.com/VeriorPies/ParrelSync), and open the `Client` scene in the local editor and `Remote`
    in the remote editor.
5. Press Play in both editors, first on the remote. If the setup is done correctly, the original editor should connect
    as a client to the server. The cloned editor only starts an HTTP server but does not log in.
6. You can now switch the original editor to a thin client and back. If a policy is selected in the Networking field
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
