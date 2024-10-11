using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.RenderStreaming;

namespace Opencraft.RenderStreaming
{
    static class InputSenderExtension
    {
        public static (Rect, Vector2Int) GetRegionAndSize(this RawImage image)
        {
            // correct pointer position
            Vector3[] corners = new Vector3[4];
            image.rectTransform.GetWorldCorners(corners);
            Camera camera = image.canvas.worldCamera;
            var corner0 = RectTransformUtility.WorldToScreenPoint(camera, corners[0]);
            var corner2 = RectTransformUtility.WorldToScreenPoint(camera, corners[2]);
            var region = new Rect(
                corner0.x,
                corner0.y,
                corner2.x - corner0.x,
                corner2.y - corner0.y
                );

            var size = new Vector2Int(image.texture.width, image.texture.height);
            return (region, size);
        }
    }

    public class ReceiverScript : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private SignalingManager renderStreaming;
        [SerializeField] private RawImage remoteVideoImage;
        [SerializeField] private AudioSource remoteAudioSource;
        [SerializeField] private VideoStreamReceiver receiveVideoViewer;
        [SerializeField] private AudioStreamReceiver receiveAudioViewer;
        [SerializeField] private SingleConnection connection;
#pragma warning restore 0649

        private string connectionId;
        private InputSender inputSender;
        private Vector2 lastSize;

        // Start is called before the first frame update
        void Start()
        {
            if (renderStreaming.runOnAwake)
                return;

            // if (settings != null)
            //     renderStreaming.useDefaultSettings = settings.UseDefaultSettings;
            // if (settings?.SignalingSettings != null)
            //     renderStreaming.SetSignalingSettings(settings.SignalingSettings);
            renderStreaming.Run();

            // if (settings != null)
            // receiveVideoViewer.SetCodec(settings.ReceiverVideoCodec);
            receiveAudioViewer.targetAudioSource = remoteAudioSource;

            connectionId = System.Guid.NewGuid().ToString("N");
            connection.CreateConnection(connectionId);
        }

        void Awake()
        {
            receiveVideoViewer.OnUpdateReceiveTexture += OnUpdateReceiveTexture;
            receiveAudioViewer.OnUpdateReceiveAudioSource += source =>
            {
                source.loop = true;
                source.Play();
            };

            inputSender = GetComponent<InputSender>();
            inputSender.OnStartedChannel += OnStartedChannel;

            // settings = new RenderStreamingSettings();
        }

        void OnUpdateReceiveTexture(Texture texture)
        {
            remoteVideoImage.texture = texture;
            CalculateInputRegion();
        }

        // Update is called once per frame
        void Update()
        {
            // Call SetInputChange if window size is changed.
            var size = remoteVideoImage.rectTransform.sizeDelta;
            if (lastSize == size)
                return;
            lastSize = size;
            CalculateInputRegion();
        }

        void OnStartedChannel(string connectionId)
        {
            CalculateInputRegion();
        }

        void CalculateInputRegion()
        {
            if (inputSender == null || !inputSender.IsConnected || remoteVideoImage.texture == null)
                return;
            var (region, size) = remoteVideoImage.GetRegionAndSize();
            inputSender.CalculateInputResion(region, size);
            inputSender.EnableInputPositionCorrection(true);
        }
    }
}
