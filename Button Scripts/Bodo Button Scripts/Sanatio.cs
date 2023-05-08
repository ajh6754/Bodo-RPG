using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Sanatio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Button descriptionBackground;
    public Text description;

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionBackground.gameObject.SetActive(true);
        description.text = "Heals one ally for 500 HP and restores 125 SP. Costs 25 SP";
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