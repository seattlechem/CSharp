using System.Collections.Generic;

namespace deckofcards
{
    public class Card
    {   
        private int _suit;
        private int _val;
        static string[] mySuit = {"Diamonds", "Hearts", "Spades", "Clubs"};

        public string Suit
        {
            get
            {
                return mySuit[_suit];
            }        
        }

        public string Val
        {
            get
            {
                if(_val == 1)
                {
                    return "Ace";
                }

                else if(_val>1 && _val<11)
                {
                    return _val.ToString();
                }
                else if (_val == 11)
                {
                    return "Jack";
                }
                else if (_val == 12)
                {
                    return "Queen";
                }
                else if (_val == 13)
                {
                    return "King";
                }                
                else 
                {
                    return "Jocker";
                }
            }
        }

        //constructor
        public Card(int suit, int val)
        {
            _suit = suit;
            _val = val;

        }

        public override string ToString()
        {
            return Val + " of " + Suit;
        }
    }    


       

}