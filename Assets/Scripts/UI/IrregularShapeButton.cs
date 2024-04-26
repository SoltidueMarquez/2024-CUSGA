using UnityEngine;
using UnityEngine.UI;

namespace Frame.UI
{
    public class IrregularShapeButton : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }

    }
}
