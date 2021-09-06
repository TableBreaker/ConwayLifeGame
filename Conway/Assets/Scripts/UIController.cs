using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  private void Awake() {
    _start.onClick.AddListener(OnStartButton);
    _pause.onClick.AddListener(OnPauseButton);
    _stop.onClick.AddListener(OnStopButton);
  }

  private void OnStartButton() {
    GameController.instance.StartGame();
    _start.GetComponent<Image>().color = Color.green;
    _pause.GetComponent<Image>().color = Color.white;
    _stop.GetComponent<Image>().color = Color.white;
  }

  private void OnPauseButton() {
    GameController.instance.Pause();
    _start.GetComponent<Image>().color = Color.white;
    _pause.GetComponent<Image>().color = Color.green;
    _stop.GetComponent<Image>().color = Color.white;
  }

  private void OnStopButton() {
    GameController.instance.StopGame();
    _start.GetComponent<Image>().color = Color.white;
    _pause.GetComponent<Image>().color = Color.white;
    _stop.GetComponent<Image>().color = Color.green;
  }

  [SerializeField]
  private Button _start;
  [SerializeField]
  private Button _pause;
  [SerializeField]
  private Button _stop;
}
