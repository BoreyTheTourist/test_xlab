using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class GameInstance : MonoBehaviour
    {
        public Transform states;
        private void Start()
        {
            foreach (Transform state in states) {
                state.gameObject.SetActive(false);
            }
            states.GetChild(0).gameObject.SetActive(true);
        }
    }
}
