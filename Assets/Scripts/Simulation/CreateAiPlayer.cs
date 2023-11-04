using UnityEngine;

public class CreateAiPlayer : MonoBehaviour {
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private BrainCreator _brainCreator;
    [SerializeField] private CheckPointList _checkpointList;
    [SerializeField] private Transform _spawnContainer;

    private bool _startSpawning;
    [SerializeField] private int _spawnCount;

    // Start is called before the first frame update
    void Start() {
        _brainCreator.CreateNewBrainGen();
        _spawnCount = 0;
        SpawnCharacter();
    }
    private void SpawnCharacter() {
        int spawnAmount = SettingsObject.Instance.AISpawnAmount;

        for (int i = 0; i < spawnAmount; i++) {
            GameObject obj = Instantiate(_playerPrefab, _startPosition.position, Quaternion.identity, _spawnContainer);
            obj.SetActive(false);
        }
    }

    private bool SpawnAIPlayer(int spawnIndex) {
        ICharacter character = CharacterContainer.Instance.ControlAbleCharacterQueue.Pop();

        if (character.CanTakeControl() == false) {
            return false;
        }
        //NeuralNetwork neuralNetwork = new NeuralNetwork(_brainCreator.BrainList[spawnIndex]);
        //if (character.TakeControl(neuralNetwork) == false) {
        //    return false;
        //}
        //neuralNetwork.ControledCharacter = character;
        //ResetCharacter(character);

        //NeuralNetworkManager.Instance.AddAgent(neuralNetwork);

        ResetCharacter(character, spawnIndex);

        return true;
    }
    private bool ResetCharacter(ICharacter character, int spawnIndex) {
        GameObject obj = character.GetGameObject();

        NeuralNetwork neuralNetwork = obj.GetComponent<NeuralNetwork>();
        if (character.TakeControl(neuralNetwork) == false) {
            return false;
        }
        neuralNetwork.Brain = _brainCreator.BrainList[spawnIndex];

        AIInputDataCreator input = obj.GetComponent<AIInputDataCreator>();
        input.enabled = true;

        neuralNetwork.InputDataCreator = input;
        neuralNetwork.ControledCharacter = character;

        neuralNetwork.enabled = true;

        obj.transform.position = _startPosition.position;
        obj.transform.rotation = Quaternion.identity;
        CharacterScore score = obj.GetComponent<CharacterScore>();
        score.CheckpointList = _checkpointList;



        obj.SetActive(true);
        return true;
    }

    private void Update() {
        if (_brainCreator.BrainList == null) {
            _brainCreator.CreateNewBrainGen();
            _spawnCount = 0;
        }

        if (_spawnCount >= _brainCreator.BrainList.Count && SettingsObject.Instance.AISpawnAmount == CharacterContainer.Instance.ControlAbleCharacterQueue.Count) {
            _brainCreator.CreateNewBrainGen();
            _spawnCount = 0;
        }
        else if (_spawnCount >= _brainCreator.BrainList.Count) {
            return;
        }

        if (_startSpawning == true && CharacterContainer.Instance.ControlAbleCharacterQueue.Count != 0) {

            if (SpawnAIPlayer(_spawnCount) == true) {
                _spawnCount = _spawnCount + 1;
            }
        }

    }

    [ContextMenu("Start AI spawning")]
    private void StartAI() {
        _startSpawning = true;
    }

    [ContextMenu("Stop AI spawning")]
    public void StopAI() {
        _startSpawning = false;
    }
}
