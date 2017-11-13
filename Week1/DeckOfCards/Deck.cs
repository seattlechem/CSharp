using System;
using System.Collections.Generic;


namespace deckofcards
{
    public class Deck
    {
        //constructor
        //when a deck object is created, 52 cards have to be created
        //List<Card>

        public List<Card> myCards;

        public Deck()
        {
           Reset();
        }
        //Deal method: remove one card from deck at index 0
        public Card Deal()
        {
            //Before remove it save what's at index 0
            if (myCards.Count >0)
            {
                Card temp = myCards[0];
                myCards.RemoveAt(0); //when you remove it, it's gone. 

                return temp;
            }
            //otherwise return null value
            return null;

        }

        public Deck Shuffle()
        {
            Random rand = new Random();
            //going from the backward (start from last index of myCards)
            for (int idx = myCards.Count-1; idx>0; idx--)
            {
                int randIndex = rand.Next(idx);
                //randIndex is less than idx because of rand.Next(idx)
                //myCards[randIndex] is something other than myCards[idx]
                Card temp = myCards[randIndex];
                //swap the value on myCards[idx] is transferred to myCards[randIndex]
                myCards[randIndex] = myCards[idx];
                myCards[idx] = temp;

            }
            return this;
        }

        public Deck Reset()
        {
            myCards = new List<Card>();
            for(int i=0; i<4; i++)
            {
                for(int j=1; j<14; j++)
                {
                    Card myCard = new Card(i, j);
                    myCards.Add(myCard);
                }
            }

            return this;
        }

        public override string ToString()
        {
            string info = "";
            foreach(Card card in myCards)
            {
                info += card + "\n";    
            }

            return info;
        }   
    }





}