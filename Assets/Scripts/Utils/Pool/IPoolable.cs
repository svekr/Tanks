using System;
using UnityEngine;

namespace Utils.Pool {
    public interface IPoolable {
        GameObject GameObject { get; }

        void ReturnToPool();
        void SetReturnToPoolAction(Action<IPoolable> returnToPoolAction);
    }
}