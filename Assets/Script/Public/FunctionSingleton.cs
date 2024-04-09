using UnityEngine;

    public class FunctionSingleton<T> : MonoBehaviour where T : FunctionSingleton<T>
    {
        protected static T instance = null;
        public static T Instance => instance;
        protected virtual void Awake()
        {
            if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
            {
                instance = (T)this; //내자신을 instance로 넣어줍니다.
            }
        }
    }