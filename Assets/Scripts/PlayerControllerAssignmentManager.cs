using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerControllerAssignmentManager : MonoBehaviour
{
    public List<PlayerShipMovementController> players = new List<PlayerShipMovementController>();
    private List<InputDevice> _assignedDevices = new List<InputDevice>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var player in players)
        {
            if (player.inputDevice != null)
                continue;

            foreach (InputDevice device in InputManager.Devices)
            {
                if (_assignedDevices.Contains(device))
                    continue;
                if (device.GetControl(InputControlType.Action1).WasPressed)
                {
                    _assignedDevices.Add(device);
                    player.inputDevice = device;
                    break;
                }
            }
        }
    }

    public bool IsGamePaused()
    {
        foreach (var player in players)
        {
            if (player.inputDevice == null)
                return true;
        }
        return false;
    }
}
