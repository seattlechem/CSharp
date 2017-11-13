using System.Collections.Generic;

namespace deckofcards
{
    public class Player
    {
        public string name;
        private List<Card> hand;

        public Player(string n)
        {
            hand = new List<Card>();
            name = n;     
        }

        //Draw a card
        public void Draw(Deck currentDeck)
        {
            //if a player accepts the card drawn from deck, it will be added into player's hand
            hand.Add(currentDeck.Deal());

        }

        //We need at least index of what we're discarding
        public Card Discard(int i)
        {
            //reference is the list of cards in our hands
            Card temp = hand[i];
            hand.RemoveAt(i);
            //card that ended up removing
            return temp;

        }

    }



}
