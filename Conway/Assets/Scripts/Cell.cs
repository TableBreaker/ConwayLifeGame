using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
  private void Awake() {
    _sprite = GetComponent<SpriteRenderer>();
    Debug.Assert(_sprite);
  }

  public void Born() {
    _alive = true;
    _sprite.color = Color.white;
  }

  public void Die() {
    _alive = false;
    _sprite.color = Color.black;
  }

  public void OnMouseDown() {
    Born();
    lifeSnapshot = true;
  }

  private bool _alive;
  public bool alive {
    get => _alive;
    set {
      if (_alive == value) {
        return;
      }

      _alive = value;
      if (_alive) {
        Born();
      } else {
        Die();
      }
    }
  }

  public bool lifeSnapshot;

  private SpriteRenderer _sprite;
}
