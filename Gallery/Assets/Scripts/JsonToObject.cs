using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [System.Serializable]
    public class JsonToObject
    {
        public T loadJson<T>()
        {
            TextAsset r = (TextAsset)Resources.Load("op2", typeof(TextAsset));
            string json = r.text;
            T result = JsonUtility.FromJson<T>(json);
            return result;
        }
    }

}