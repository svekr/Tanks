using System;
using com.Tanks.TanksBattle.Game.GameEntity;
using UnityEngine;

namespace com.Tanks.TanksBattle.Game.Data {
    [Serializable]
    public class EntityData {
        [SerializeField] private EntityType _type;
        [SerializeField] private string _name;
        [SerializeField] private Vector3 _position = Vector3.zero;
        [SerializeField] private Quaternion _rotation = Quaternion.identity;

        public EntityType Type => _type;
        public string Name => _name;
        public Vector3 Position => _position;
        public Quaternion Rotation => _rotation;

        public EntityData() {

        }

        public EntityData(IGameEntity entity) {
            if (entity == null) {
                throw new ArgumentNullException();
            }
            _type = entity.Type;
            _name = entity.Name;
            _position = entity.Position;
            _rotation = entity.View?.Transform.rotation ?? Quaternion.LookRotation(Vector3.zero - _position);
        }
    }
}