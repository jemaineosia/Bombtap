using UnityEngine;

public class SmokeCloud : MonoBehaviour {
  public float lifetime = 2f;
  void Start() => Destroy(gameObject, lifetime);
}
