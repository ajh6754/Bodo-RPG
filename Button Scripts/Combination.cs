using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Combination : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Button descriptionBackground;
    public Text description;

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionBackground.gameObject.SetActive(true);
        description.text = "A flurry of punches into a kick, finished with a dropkick. Costs 30 SP";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionBackground.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        descriptionBackground.gameObject.SetActive(false);
    }
}
