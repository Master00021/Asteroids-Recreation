using UnityEngine;

namespace Game {

    public enum BorderType {
        Up,
        Right,
        Down,
        Left
    }

    public class Border : MonoBehaviour {

        [SerializeField] private BorderType _borderType;
        [SerializeField] private Transform _borderToTeleport;
        [SerializeField] private Vector3 _offset;
    
        private void OnTriggerEnter2D(Collider2D other) {
            var newPosition = Vector3.zero;
            if (_borderType == BorderType.Up || _borderType == BorderType.Down) {
                newPosition = new Vector3(other.transform.position.x, _borderToTeleport.position.y);
            }
            else {
                newPosition = new Vector3(_borderToTeleport.position.x, other.transform.position.y);
            }

            other.transform.position = newPosition + _offset;
        }
    
    }
}

