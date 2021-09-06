using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  private enum EGameState {
    Prepare,
    Paused,
    Running,
  }

  private const int WIDTH = 160;
  private const int HEIGHT = 90;
  private const float SIZE = 0.16f;

  private void Awake() {
    instance = this;
    Prepare();
  }

  private void OnDestroy() {
    instance = null;
  }

  private void Update() {
    if (_state == EGameState.Prepare || _state == EGameState.Paused) {
      return;
    }

    _timer += Time.deltaTime;
    if (_timer > _interval) {
      Iterate();
      _timer = 0f;
    }
  }

  private void Prepare() {
    _timer = 0f;
    _state = EGameState.Prepare;
    var startX = (1 - WIDTH) * 0.5f * SIZE;
    var startY = (1 - HEIGHT) * 0.5f * SIZE;
    for (var i = 0; i < WIDTH; i++) {
      for (var j = 0; j < HEIGHT; j++) {
        var cell = Instantiate(_cell, transform);
        cell.name = $"cell({i}, {j})";
        cell.transform.position = new Vector2(startX + i * SIZE, startY + j * SIZE);
        _cellMap[i, j] = cell.GetComponent<Cell>();
      }
    }
  }

  private void Iterate() {
    for (var i = 0; i < WIDTH; i++) {
      for (var j = 0; j < HEIGHT; j++) {
        var lifeCountAround = GetLifeCountAround(i, j);
        var cell = _cellMap[i, j];
        cell.alive = CalculateCellLife(lifeCountAround, cell.alive);
      }
    }

    for (var i = 0; i < WIDTH; i++) {
      for (var j = 0; j < HEIGHT; j++) {
        _cellMap[i, j].lifeSnapshot = _cellMap[i, j].alive;
      }
    }
  }

  private int GetLifeCountAround(int x, int y) {
    var count = 0;
    for (var i = Mathf.Max(0, x - 1); i <= Mathf.Min(WIDTH - 1, x + 1); i++) {
      for (var j = Mathf.Max(0, y - 1); j <= Mathf.Min(HEIGHT - 1, y + 1); j++) {
        if (i == x && j == y) {
          continue;
        }

        if (_cellMap[i, j].lifeSnapshot) {
          count++;
        }
      }
    }

    return count;
  }

  private bool CalculateCellLife(int lifeCount, bool alive) {
    if (!alive) {
      if (lifeCount == 3) {
        return true;
      }
    } else {
      if (lifeCount < 2 || lifeCount > 3) {
        return false;
      }
    }

    return alive;
  }

  public void StartGame() {
    _state = EGameState.Running;
  }

  public void Pause() {
    if (_state == EGameState.Prepare || _state == EGameState.Paused) {
      return;
    }

    _state = EGameState.Paused;
  }

  public void StopGame() {
    if (_state == EGameState.Prepare) {
      return;
    }

    _timer = 0f;
    _state = EGameState.Prepare;
    for (var i = 0; i < WIDTH; i++) {
      for (var j = 0; j < HEIGHT; j++) {
        _cellMap[i, j].Die();
      }
    }
  }

  public static GameController instance;

  private EGameState _state;
  private float _timer;
  private readonly Cell[,] _cellMap = new Cell[WIDTH, HEIGHT];

  [SerializeField]
  private GameObject _cell;
  [SerializeField]
  private float _interval = 1f;
}
