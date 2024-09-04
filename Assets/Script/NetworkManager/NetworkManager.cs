using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;
using TMPro;
using Photon.Pun.Demo.Cockpit;
using UnityEngine.Scripting;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private readonly string version = "prototype v0.11";
    private string userId = "ChanHwi";


    // 스크립트가 시작되자마자 실행됨
    private void Awake()
    {
        // 같은 룸의 유저들에게 자동으로 씬을 로딩
        PhotonNetwork.AutomaticallySyncScene = true;

        // 같은 버전의 유저끼리 접속 허용
        PhotonNetwork.GameVersion = version;
        // 유저에게 닉네임 할당
        PhotonNetwork.NickName = userId;

        // Photon Server와 통신 횟수. 초당 60회
        PhotonNetwork.SendRate = 60;
        Debug.Log(PhotonNetwork.SendRate);

        // Server Connect
        PhotonNetwork.ConnectUsingSettings();

     //   userWritedId = text.GetComponent<TextMeshPro>(); // ** 추후 아이디를 닉네임으로 쓰기위한 발판
        
    }

    // Photon Server에 접속 후 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby} In Server");
        PhotonNetwork.JoinLobby(); // Lobby 입장
    }

    // Lobby 입장 후 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby} In Lobby");
        PhotonNetwork.JoinRandomRoom(); // 이미 만들어진 Room들 중 랜덤하게 Join
    }

    // JoinRandomRoom이 실패했을 경우 호출되는 콜백함수
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRoomFailed {returnCode} : {message}");
        // room의 속성 정의 32760
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4; // 최대 입장 가능 인원수
        roomOptions.IsOpen = true; // Room의 Open 여부
        roomOptions.IsVisible = true; // Lobby에서 Room의 노출 여부

        // 룸 생성
        if (returnCode == 32760){ // 방이 없어서 랜덤 입장에 실패했을 경우 새로운 Room 생성
            PhotonNetwork.CreateRoom("TestRoom", roomOptions);
        }
    }

    // 룸 생성이 완료된 후 생성되는 콜백함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"RoomName = {PhotonNetwork.CurrentRoom.Name}");
    }

    // OnCreatedRoom Method 실행 후 혹은 PhotonNetwork.JoinRandomRoom() method의 return이 true인 경우 호출되는 콜백함수
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        Debug.Log($"PhntonNetwork.InRoom = {PhotonNetwork.InRoom}");

        Debug.Log($"PlayerCount = {PhotonNetwork.CurrentRoom.PlayerCount}");
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(player.Value.NickName+ " " + player.Value.ActorNumber);
        }

        GameObject PI = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
