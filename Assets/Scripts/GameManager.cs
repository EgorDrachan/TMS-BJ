using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button dealButton;
    [SerializeField] private Button hitButton;
    [SerializeField] private Button standButton;
    [SerializeField] private Button betButton;

    [Header("Scripts")]
    [SerializeField] private Player player;
    [SerializeField] private Player dealer;

    [Header("Text")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI dealText;
    [SerializeField] private TextMeshProUGUI betText;
    [SerializeField] private TextMeshProUGUI bankText;
    [SerializeField] private TextMeshProUGUI standButtonText;
    [SerializeField] private TextMeshProUGUI mainText;

    public int pot = 0;

    public int standClick = 0;

    public GameObject hideCard;
    private void Start()
    {
        dealButton.onClick.AddListener(DealClicked);
        hitButton.onClick.AddListener(HitClicked);
        standButton.onClick.AddListener(StandClicked);
        betButton.onClick.AddListener(BetClick);
    }
    private void StandClicked()
    {
        standClick++;
        if (standClick > 1)
        {
            RoundResult();
        }
        HitDeal();
        standButtonText.text = "Call";
    }

    private void HitDeal()
    {
        while (dealer.handValue < 17 && dealer.cardIndex < 10)
        {
            dealer.GetCard();
            dealText.text = "Score: " + dealer.handValue.ToString();
            if (dealer.handValue > 20)
            {
                RoundResult();
            }
        }
    }

    private void HitClicked()
    {
        if (player.cardIndex <= 10)
        {
            player.GetCard();
            scoreText.text = "Score: " + player.handValue.ToString();
            if (player.handValue > 20)
            {
                RoundResult();
            }
        }
    }

    private void DealClicked()
    {
        player.ResetHand();
        dealer.ResetHand();
        
        mainText.gameObject.SetActive(false);
        dealText.gameObject.SetActive(false);
        dealText.gameObject.SetActive(false);
        
        GameObject.Find("CardDeck").GetComponent<CardDeck>().Shuffle();
        
        player.StartHand();
        dealer.StartHand();
        
        scoreText.text = "Hand: " + player.handValue.ToString();
        dealText.text = "Hand: " + dealer.handValue.ToString();
        
        hideCard.GetComponent<Renderer>().enabled = true;
        
        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        standButtonText.text = "Stand";
        
        BetCheck();
    }
    private void BetCheck()
    {
        pot = 20;
        betText.text = pot.ToString();
        player.Bank(-20);
        bankText.text = player.GetMoney().ToString();
    }

    private void RoundResult()
    { 
        var playerLose = player.handValue > 21;
        var dealerLose = player.handValue > 21;
        var playerWin = player.handValue == 21;
        var dealerWin = player.handValue == 21;

        if (standClick < 2 && !playerLose && !playerWin && !dealerLose && !dealerWin)
        {
            return;
        }

        var roundOver = true;
       
        if (playerLose && dealerLose)
        {
            mainText.text = "Dealer Win!";
        }
        
        else if (playerLose || (!dealerLose && dealer.handValue > player.handValue))
        {
            mainText.text = "Dealer Win!";
        }
        
        else if (dealerLose || player.handValue > dealer.handValue)
        {
            mainText.text = "You Win!";
            player.Bank(pot);
        }
        
        else if (player.handValue == dealer.handValue)
        {
            mainText.text = "Stay";
            player.Bank(pot / 2);
        }

        else
        {
            roundOver = false;
        }

        if (roundOver)
        {
            AdsScript.ShowAdsVideo("Interstitial Android");
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            dealButton.gameObject.SetActive(true);
            mainText.gameObject.SetActive(true);
            dealText.gameObject.SetActive(true);
            
            hideCard.GetComponent<Renderer>().enabled = false;
            
            bankText.text = player.GetMoney().ToString();
            
            standClick = 0;
        }
    }

    private void BetClick()
    {
        var newBet = betButton.GetComponentInChildren(typeof(Text)) as Text;
        if (newBet != null)
        {
            var bet = int.Parse(newBet.text.ToString().Remove(0, 1));
            player.Bank(-bet);
            bankText.text = player.GetMoney().ToString();
            pot += (bet * 2);
            betText.text = pot.ToString();
        }
    }
}
