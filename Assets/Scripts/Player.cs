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

   public void AceCheck()
   {
      foreach (Card ace in aceList)
      {
         if (handValue + 10 < 22 && ace.GetValue() == 11)
         {
            ace.SetValue(11);
            handValue += 10;
         }
         else if(handValue > 21 && ace.GetValue() == 11)
         {
            ace.SetValue(1);
            handValue -= 10;
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
