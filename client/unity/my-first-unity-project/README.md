
# Opencraft Unity Client

Open the project in **Unity 2022.3.26f1**. You can run the game either from the
editor or as a stand-alone build. If you intend to edit the client code, we
recommend running the game from the editor because it allows you to more quickly
test your changes in practice.

In the editor, there are two main scenes: **Client** and **ThinClient**. The
Client scene connects to a running server. To run the client from the editor,
open the Client scene, configure the correct settings by clicking on the
Bootstrap game object in the pane on the left-hand side (the command-line
arguments will appear in the pane on the right-hand side), then hit the play
button at the top-middle of the editor.

If you want to run both the client and the thin-client from the editor, use the
Unity Editor Extension [ParrelSync](https://github.com/VeriorPies/ParrelSync) to
open the same project in two different editors. Open the Client scene in one
editor, and the ThinClient in another. Configure them correctly and press play
in both editors.
