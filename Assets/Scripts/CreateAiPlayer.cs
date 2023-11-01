using System.Collections.Generic;
using UnityEngine;

public class CreateAiPlayer : MonoBehaviour {
    public GameObject playerPrefab;
    public GameObject startPosition;
    public BrainCreator brainCreator;

    public float maxTrainingDuration;
    private float _duration;
    public List<GameObject> _playerList;
    public CheckPointList checkpointList;
    public bool stop;

    public List<GameObject> poolList;
    public int createdPlayerCount;

    public int maxActivePlayer;
    public int currentActivePlayer;

    // Start is called before the first frame update
    void Start() {
        _playerList = new List<GameObject>();
        brainCreator.CreateNewBrainGen();
        createdPlayerCount = 0;
        currentActivePlayer = 0;
        SpawnNewPlayer();

    }

    public void SpawnNewPlayer() {
        while (currentActivePlayer <= maxActivePlayer && createdPlayerCount < brainCreator.brainList.Count) {

            GameObject player;
            if (poolList.Count != 0) {
                player = poolList[poolList.Count - 1];
                poolList.RemoveAt(poolList.Count - 1);
            }
            else {
                player = Instantiate(playerPrefab);
            }

            player.transform.position = startPosition.transform.position;
            player.transform.rotation = Quaternion.identity;

            Player p = player.GetComponent<Player>();

            p.checkpointList = checkpointList;
            p.ResetValues();
            p.enabled = true;

            NeuralNetwork network = player.GetComponent<NeuralNetwork>();
            network.brain = brainCreator.brainList[createdPlayerCount];
            network.enabled = true;

            player.SetActive(true);
            _playerList.Add(player);

            createdPlayerCount = createdPlayerCount + 1;
            currentActivePlayer = currentActivePlayer + 1;

        }

        _duration = 0;
    }

    private void Update() {

        for (int i = _playerList.Count - 1; i >= 0;) {

            if (_playerList[i].activeSelf == false) {
                poolList.Add(_playerList[i]);
                _playerList[i].transform.position = startPosition.transform.position;
                _playerList[i].transform.rotation = Quaternion.identity;
                _playerList[i].GetComponent<Player>().ResetValues();
                _playerList[i].GetComponent<AIInputDataCreator>().ResetValues();
                _playerList.RemoveAt(i);
                currentActivePlayer = currentActivePlayer - 1;
            }

            i = i - 1;
        }
        if (currentActivePlayer <= maxActivePlayer) {
            SpawnNewPlayer();
        }

        if (_playerList.Count == 0) {
            currentActivePlayer = 0;
            createdPlayerCount = 0;
            brainCreator.CreateNewBrainGen();
            SpawnNewPlayer();
        }
    }

    //private void Update() {
    //    _duration = _duration + Time.deltaTime;

    //    if (_duration >= maxTrainingDuration) {
    //        foreach (GameObject g in _playerList) {
    //            Destroy(g);
    //        }
    //    }
    //}

    [ContextMenu("Stop AI")]
    public void StopAi() {
        foreach (GameObject g in _playerList) {
            Destroy(g);
        }
    }
}
