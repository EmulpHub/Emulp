using UnityEngine;
using UnityEngine.UI;

public class SelectionnedSpell_Holder : MonoBehaviour
{
    public Image img;

    public Transform child1, child2;

    public void ActiveSpellGraphics()
    {
        child1.transform.gameObject.SetActive(true);
        child2.transform.gameObject.SetActive(true);
    }

    public void DeactiveSpellGraphics()
    {
        child1.transform.gameObject.SetActive(false);
        child2.transform.gameObject.SetActive(false);
    }
}
