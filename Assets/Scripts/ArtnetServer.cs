using System;
using System.Linq; 
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;


namespace Scripts {

    public class ArtnetServer : MonoBehaviour {

        private List<IFixtureController> _fixtures;
        
        private const int _artnetPort = 6454;
        private const int _receiveBufferSize = 120000;
        
        private UdpClient _udpClient;
        private IPEndPoint _remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private ArtnetPacket _artnetPacket = new ArtnetPacket();
        private System.AsyncCallback _asyncCallback;
        private object _object = null;
        private byte[] _receivedBytes;
        
        void Start()
        {
            _fixtures = GetComponentsInChildren<IFixtureController>().ToList();
            Debug.Log(String.Format("IFixtureController found: {0}", _fixtures.Count));

            _asyncCallback = new System.AsyncCallback(ReceivedUDPPacket);

            _udpClient = new UdpClient(_artnetPort);
            _udpClient.Client.ReceiveBufferSize = _receiveBufferSize;
            _udpClient.BeginReceive(_asyncCallback, _object);
        }

        void Update()
        {
            UpdateFixtures();
        }

        void ReceivedUDPPacket(System.IAsyncResult result)
        {
            _receivedBytes = _udpClient.EndReceive(result, ref _remoteIpEndPoint);
            _artnetPacket.ParseArtnetPacket(_receivedBytes);
            _udpClient.BeginReceive(_asyncCallback, _object);
        }

        void UpdateFixtures() 
        {
            foreach (var fixture in _fixtures) {
                for (int i = 0; i < fixture.Channels.Count(); i++) {
                    fixture.Channels[i] = _artnetPacket.data[fixture.Address + i - 1];
                }
            }
        }
        
        void OnDestroy()
        {
            if (_udpClient != null)
            {
                _udpClient.Close();
            }

        }
    }

    public class ArtnetPacket  {
        /*
        public byte[] header = new byte[7];             // 0-6
        public byte[] opcode = new byte[2];             // 8-9
        public byte[] protocolVersion = new byte[2];    // 10-11
        public byte sequence = 0;                       // 12
        public byte physical = 0;                       // 13
        public byte[] universe = new byte[2];           // 14-15
        public byte[] dataLength = new byte[2];         // 16-17
        public byte[] data = new byte[512];             // 18-530
        public byte hasChanged = 0;

        public int pCnt = 0;
        public int pIndex = 0;

        public void parseArtNetPacket(byte[] packetBuffer) {
            
            // header
            pIndex=0;
            for(pCnt=0; pCnt<7; pCnt++) {
                header[pCnt] = packetBuffer[pIndex];
                pIndex++;
            }
            // opcode
            pIndex++;
            for(pCnt=0; pCnt<2; pCnt++) {
                opcode[pCnt] = packetBuffer[pIndex];
                pIndex++;
            }
            // protocol Version
            for(pCnt=0; pCnt<2; pCnt++) {
                protocolVersion[pCnt] = packetBuffer[pIndex];
                pIndex++;
            }
            // sequence
            sequence = packetBuffer[pIndex];
            pIndex++;
            // physical
            physical = packetBuffer[pIndex];
            pIndex++;
            // universe
            for(pCnt=0; pCnt<2; pCnt++)  {
                universe[pCnt] = packetBuffer[pIndex];
                pIndex++;
            }
            // datalength
            for(pCnt=0; pCnt<2; pCnt++)  {
                dataLength[pCnt] = packetBuffer[pIndex];
                pIndex++;
            }
            // data
            for(pCnt=0; pCnt<512; pCnt++) {
                if( data[pCnt] != packetBuffer[pIndex] && hasChanged == 0 ) {
                    hasChanged=1;
                }
                data[pCnt] = packetBuffer[pIndex];
                pIndex++;
            }
            // check for blank
            pIndex=0;
            for(pCnt=0; pCnt<512; pCnt++) {
                pIndex+=data[pCnt];
            }
            if(pIndex==0 && hasChanged==0) {
                hasChanged=1;
            }
        }*/
         /* 41-72-74-2D-4E-65-74-00-00-50-00-0E-00-00-00-00-02-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00 */
        /* 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20 ..                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 /*00*/

        public byte[] data = new byte[512];

        private ushort _dataLength = 0;

        public void ParseArtnetPacket(byte[] packetBuffer)
        {
            _dataLength = (ushort)((packetBuffer[16] << 8) + packetBuffer[17]);
            for (int i = 0; i < 512; i++) {
                if (i < _dataLength) {
                    data[i] = packetBuffer[i + 18];
                } else {
                    data[i] = 0;
                }
            }
        }
    }
}
