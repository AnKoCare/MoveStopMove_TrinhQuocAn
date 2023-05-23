using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache
{
    private static Dictionary<Collider, Character> m_CharacterList = new Dictionary<Collider, Character>();

    public static Character GetCharacter(Collider key)
    {
        if(!m_CharacterList.ContainsKey(key))
        {
            Character character = key.GetComponent<Character>();

            if(character != null)
            {
                m_CharacterList.Add(key,character);
            }
            else
            {
                return null;
            }
        }

        return m_CharacterList[key];
    }
}