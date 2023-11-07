//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:29:16
//  GitUser: azzinoth01
//===================================================
using System.Collections.Generic;

public class CharacterContainer {
    private static CharacterContainer _instance;

    public Stack<ICharacter> ControlAbleCharacterQueue {
        get;
        private set;
    }

    public static CharacterContainer Instance {
        get {
            if (_instance == null) {
                _instance = new CharacterContainer();
            }
            return _instance;
        }

    }


    private CharacterContainer() {
        ControlAbleCharacterQueue = new Stack<ICharacter>();
    }
}
