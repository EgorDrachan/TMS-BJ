using UnityEngine;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
   [SerializeField] private Sprite[] cardSprite;
   
   private const int CardSum = 53;
   private const int AceCoef = 13;
   private const int MaxAceCoef = 10;
  
   private int[] cardValues = new int[CardSum];
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
         num %= AceCoef;
         if (num > MaxAceCoef || num == 0)
         {
            num = MaxAceCoef;
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


