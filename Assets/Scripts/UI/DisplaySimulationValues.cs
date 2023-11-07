//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:30:56
//  GitUser: azzinoth01
//===================================================
using UnityEngine;

public class DisplaySimulationValues : MonoBehaviour {
    [SerializeField] private CreateAiPlayer _createAIPlayer;
    [SerializeField] private BrainCreator _brainCreator;
    [SerializeField] private TMPro.TMP_Text _spawnCount;
    [SerializeField] private TMPro.TMP_Text _trainigCycle;
    [SerializeField] private TMPro.TMP_Text _generation;
    [SerializeField] private TMPro.TMP_Text _bestGeneration;

    // Update is called once per frame
    void Update() {
        // to simplyfy the displaycode I just get the values each frame

        _spawnCount.text = _createAIPlayer.SpawnCount.ToString();
        _trainigCycle.text = _brainCreator.CurrentTestCycle.ToString();
        _generation.text = _brainCreator.CurrentBrainId.ToString();
        _bestGeneration.text = _brainCreator.BestGeneration.ToString();
    }
}
