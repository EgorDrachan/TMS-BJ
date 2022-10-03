using UnityEngine;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
   [SerializeField] private Sprite[] cardSprite;
   private int[] cardValues = new int[53];
   private int currentIndex;

   private void Start()
   {
      GetCardsValues();
   }

   void GetCardsValues()
   {
      for (int i = 0; i < cardSprite.Length; i++)
      {
         var num = i;
         num %= 13;
         if (num > 10 || num == 0)
         {
            num = 10;
         }

         cardValues[i] = num;
      }
   }

   public void Shuffle()
   {
      for (int i = cardSprite.Length - 1; i > 0; --i)
      {
         int random = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardSprite.Length - 1) + 1;
         (cardSprite[i], cardSprite[random]) = (cardSprite[random], cardSprite[i]);

         (cardValues[i], cardValues[random]) = (cardValues[random], cardValues[i]);
      } 
      currentIndex = 1;
   }

   public int DealCard(Card card)
   {
      card.SetSprite(cardSprite[currentIndex]);
      card.SetValue(cardValues[currentIndex++]);
     
      currentIndex++;
     
      return card.GetValue();
   }

   public Sprite GetCardBack()
   {
      return cardSprite[0];
   }
}


