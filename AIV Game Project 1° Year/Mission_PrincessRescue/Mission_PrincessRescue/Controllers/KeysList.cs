﻿using System.Collections.Generic;
using Aiv.Fast2D;

namespace Mission_PrincessRescue
{
    enum KeyName { Up, Down, Right, Left, Fire, Jump, LAST }

    struct KeysList
    {
        private KeyCode[] keycodes;

        public KeysList(List<KeyCode> keys)
        {
            keycodes = new KeyCode[(int)KeyName.LAST];

            for (int i = 0; i < keys.Count; i++)
                keycodes[i] = keys[i];
        }

        public void AddKey(KeyName name, KeyCode code)
        {
            keycodes[(int)name] = code;
        }

        public KeyCode GetKey(KeyName name)
        {
            return keycodes[(int)name];
        }

    }
}