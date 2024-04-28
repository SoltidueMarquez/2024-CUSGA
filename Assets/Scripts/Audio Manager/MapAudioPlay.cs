using UnityEngine;

namespace Audio_Manager
{
    public class MapAudioPlay : MonoBehaviour
    {
        void Start()
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayMusic("Level1");
            }
        }
    }
}
