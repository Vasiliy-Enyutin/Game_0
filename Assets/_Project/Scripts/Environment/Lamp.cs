using System.Collections.Generic;
using _Project.Scripts.PlayerLogic;
using UnityEngine;
using System.Linq;

namespace _Project.Scripts.Environment
{
    public class Lamp : MonoBehaviour, IInteractable
    {
        private List<Light> _lights = new();

        private void Awake()
        {
            _lights = GetComponentsInChildren<Light>().ToList();
        }

        public void Interact()
        {
            SwitchLight();
        }

        private void SwitchLight()
        {
            //LampSwitchAudioEffect();
            _lights.ToList().ForEach(light => light.enabled = !light.enabled);
        }
    }
}
