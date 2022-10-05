using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
   [SerializeField] private Card card;
   [SerializeField] private CardDeck cardDeck;
   [SerializeField] public int handValue = 0;
   private int chips = 1000;

   [SerializeField] private GameObject[] hand;
   public int cardIndex = 0;
   private int aceCount = 0;

   private List<Card> aceList = new List<Card>();

   private const int AceLimit = 10;
   private const int MaxAceValue = 11;
   private const int MinAceValue = 1;
   private const int HandLimit = 21;
   private const int HandEnumeration = 22;

   public void StartHand()
   {
      GetCard();
      GetCard();
   }

   public int GetCard()
   {
      var cardValue = cardDeck.DealCard(hand[cardIndex].GetComponent<Card>());
      hand[cardIndex].GetComponent<Renderer>().enabled = true;
      handValue += cardValue;
      
      if (cardValue == 1)
      {
         aceList.Add(hand[cardIndex].GetComponent<Card>());
      }
      AceCheck();
      cardIndex++;
      return handValue;
      
   }

   private void AceCheck()
   {
      foreach (Card ace in aceList)
      {
         if (handValue + MaxAceValue < HandEnumeration && ace.GetValue() == MaxAceValue)
         {
            ace.SetValue(MaxAceValue);
            handValue += AceLimit;
         }
         else if(handValue > HandLimit && ace.GetValue() == MaxAceValue)
         {
            ace.SetValue(MinAceValue);
            handValue -= AceLimit;
         }
      }
   }

   public void Bank(int amount)
   {
      chips += amount;
   }

   public int GetMoney()
   {
      return chips;
   }

   public void ResetHand()
   {
      for (int i = 0; i < hand.Length; i++)
      {
         hand[i].GetComponent<Card>().ResetCard();
         hand[i].GetComponent<Renderer>().enabled = false;
      }

      cardIndex = 0;
      handValue = 0;
      aceList = new List<Card>();
   }
}
