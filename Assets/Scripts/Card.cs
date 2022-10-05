using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
  [SerializeField] private int value = 0;
  private SpriteRenderer _spriteRenderer;
  [SerializeField] private CardDeck _deck;

  private void Start()
  {
    _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    
    if (_deck == null)
    {
      _deck = GameObject.FindWithTag("Deck").GetComponent<CardDeck>();
    }
  }

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
    _spriteRenderer.sprite = newSprite;
  }

  public void ResetCard()
  {
    Sprite back = _deck.GetCardBack();
    gameObject.GetComponent<SpriteRenderer>().sprite = back;
    value = 0;
  }
}
