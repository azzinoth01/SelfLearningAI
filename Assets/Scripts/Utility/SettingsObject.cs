//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:31:12
//  GitUser: azzinoth01
//===================================================
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsObject", menuName = "ScriptableObjects/SettingsObject", order = 2)]
public class SettingsObject : ScriptableObject {


    private static SettingsObject _instance;

    [SerializeField] private LayerMask _aILayerMask;
    [SerializeField] private int _aIObjectSize;
    [SerializeField] private float _aIInputScale;
    [SerializeField] private bool _aIDeactivateSaveAIScore;
    [SerializeField] private int _aISpawnAmount;
    [SerializeField] private int _aIBrainsToCreate;
    [SerializeField] private int _aIRaycastMaxLength;
    [SerializeField] private int _aIMaxNoMoveFrames;
    [SerializeField] private int _aIMaxOnlyRotationFrames;
    [SerializeField] private int _aITestCycle;

    public static SettingsObject Instance {
        get {
            if (_instance == null) {
                _instance = Resources.Load<SettingsObject>("SettingsObject");
            }
            return _instance;
        }

    }

    public LayerMask AILayerMask {
        get {
            return _aILayerMask;
        }
    }

    public int AIObjectSize {
        get {
            return _aIObjectSize;
        }
    }

    public float AiInputScale {
        get {
            return _aIInputScale;
        }
    }

    public bool AIDeactivateSaveAIScore {
        get {
            return _aIDeactivateSaveAIScore;
        }
    }

    public int AISpawnAmount {
        get {
            return _aISpawnAmount;
        }
    }

    public int AIBrainsToCreate {
        get {
            return _aIBrainsToCreate;
        }

    }

    public int AIRaycastMaxLength {
        get {
            return _aIRaycastMaxLength;
        }

    }

    public int AIMaxNoMoveFrames {
        get {
            return _aIMaxNoMoveFrames;
        }

    }

    public int AIMaxOnlyRotationFrames {
        get {
            return _aIMaxOnlyRotationFrames;
        }

    }

    public int AITestCycle {
        get {
            return _aITestCycle;
        }
    }

    private SettingsObject() {
    }


    [ContextMenu("Save Asset")]
    private void SaveSetting() {
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
