using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
  [SerializeField] private int value = 0;

  public int GetValue()
  {
    return value;
  }

  public void SetValue(int newValue)
  {
    value = newValue;
  }

  public string GetSpriteName()
  {
    return GetComponent<SpriteRenderer>().sprite.name;
  }
  
  public void SetSprite(Sprite newSprite)
  {
    gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
  }

  public void ResetCard()
  {
    Sprite back = GameObject.Find("CardDeck").GetComponent<CardDeck>().GetCardBack();
    gameObject.GetComponent<SpriteRenderer>().sprite = back;
    value = 0;
  }
}