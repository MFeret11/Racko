using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Diagnostics;


namespace TextRacko
{

	public class RackoGame
	/* Rules: 52 normal card deck. each player starts with 26 cards, face down. 
	 * In unison, players flip cards to reveal the top card 1 at a time.
	 * If player A's card has a higher rank than player B's card, (2-A) player A's 
	 * gets both cards and places them at the bottom of their hand.
	 * If the cards are the same value both players place 3 cards face-down and 
	 * flip and compare a fourth card. Whomever has the greater ranking card between 
	 * the two wins all the cards in that round. Game ends when 1 player runs out of cards.
	 * thus, other player wins.
	 * Written by: Mattthew Feret
	 * */
	{
		private const int NUM_OF_CARDS2 = 40;
		private const int NUM_OF_CARDS3 = 50;
		private const int NUM_OF_CARDS4 = 60;
		private static int cardAmount = 0;
		private const int MAXROUNDS = 1500;

		static RackoGame ()
		{
			bool play = false;
			Console.WriteLine ("\n\nRacko!!!\n");
			Console.WriteLine ("How many players?\nEnter a 2 3 or 4, or Q for quit.");
			Console.Write ("Entry: ");
			String input = Console.ReadLine ().ToUpper ();

			while (!play) {
				if (input == "2") {
					Console.WriteLine ("2 Player Mode Active!");
					cardAmount = NUM_OF_CARDS2;
					play = true;
				} else if (input == "3") {
					Console.WriteLine ("3 Player Mode Active!");
					cardAmount = NUM_OF_CARDS3;
					play = true;
				} else if (input == "4") {
					Console.WriteLine ("4 Player Mode Active!");
					cardAmount = NUM_OF_CARDS4;
					play = true;
				} else if (input == "Q") {
					Console.WriteLine ("Good bye!");
					break;
				} else {
					Console.WriteLine ("Poor Entry, try again");
					input = Console.ReadLine ().ToUpper ();
				}
			}

			if (play) {
				//numOfPlayers
				int players = Int32.Parse (input);
				//createDeck based on amount of players we have selected. Shuffled too.
				List<int> deck = createDeck (cardAmount);
				//create discard deck
				List<int> discardDeck = new List <int> (){ };

				//				//print deck
				//				foreach (int c in deck) {
				//					Console.WriteLine (c);
				//				}
				//				Console.WriteLine ("Deck count: " + deck.Count());

				if (players == 2) {

					//add 10 cards to both players
					List<int> player1 = new List <int> (){ };
					for (int k = 0; k < 10; k++) {
						player1.Add (deck [k]);
					}
					List<int> player2 = new List <int> (){ };
					for (int k = 10; k < 20; k++) {
						player2.Add (deck [k]);
					}
					//remove those cards from deck.
					deck.RemoveRange (0, 20);
					//add 1 card from deck to discardDeck
					discardDeck.Add (deck.ElementAt (0));
					//and remove from deck.
					deck.RemoveAt (0);

					//					Console.WriteLine ("Player1 count: " + player1.Count());
					//					Console.WriteLine ("Player2 Count: " + player2.Count());
					Console.WriteLine ("Player 1 Rack:");
					foreach (int c in player1) {
						Console.WriteLine (c);
					}
					Console.WriteLine ("-----------------------");
					Console.WriteLine ("[1] --- To Draw: card " + discardDeck.ElementAt (discardDeck.Count () - 1) + " from discard pile." +
						"\n" + "[2] --- To Draw from top of deck." +
						"\n[3] --- To swap 2 cards' positions in rack.");
					String entry = Console.ReadLine ().ToUpper ().Trim ();

					if (entry == "1") { //draw from discard pile
						Console.Write ("OK --- card " + discardDeck.ElementAt (discardDeck.Count () - 1) + " Drawn. Enter card you'd like to replace: ");
						String removeCard = Console.ReadLine ().ToUpper ().Trim ();
						int numberToRemove;
						Int32.TryParse (removeCard, out numberToRemove);
						//player gets top discarded card
						if (player1.Contains (numberToRemove)) {
							player1 [player1.IndexOf (numberToRemove)] = discardDeck.ElementAt (discardDeck.Count () - 1);
							//remove card from hand and from discarded deck
							player1.Remove (numberToRemove);
							discardDeck.RemoveAt (discardDeck.Count () - 1);
						}

						//add case of bad input TODO
					} else if (entry == "2") { //draw from deck
						Console.Write ("OK. Card Drawn is: " + deck.ElementAt (0) + " -- Enter card you'd like to replace: ");
						string replace = Console.ReadLine ();
						int number3;
						Int32.TryParse (replace, out number3);
						if (player1.Contains (number3)) {
							player1 [player1.IndexOf (number3)] = deck.ElementAt (0);
							//remove card from hand and from discarded deck
							player1.Remove (number3);
							deck.RemoveAt (0);
						}
					} else if (entry == "3") { //else swap 2 cards.
						Console.Write ("OK. Enter 2 cards you'd like switch - Card 1: ");
						string switchNums = Console.ReadLine ();
						int number;
						Int32.TryParse (switchNums, out number);
						Console.Write ("And card 2: ");
						string switchNums2 = Console.ReadLine ();
						int number2;
						Int32.TryParse (switchNums2, out number2);
						//while (good) {
						//now switch cards.
						if (player1.Contains (number) && player1.Contains (number2)) {
							swap (player1, player1.IndexOf (number), player1.IndexOf (number2));
						}
						//}
					}

					//print out new rack!
					Console.WriteLine ("Player 1 New Rack:");
					foreach (int c in player1) {
						Console.WriteLine (c);
					}

					Console.WriteLine ("-----------------------");





					Console.WriteLine ("Player2hand:");
					foreach (int c in player2) {
						Console.WriteLine (c);
					}
					Console.WriteLine ("---------------\nDeck:");

					//print deck
					foreach (int c in deck) {
						Console.WriteLine (c);
					}
					Console.WriteLine ("Deck count: " + deck.Count());

				}
				if (players == 3) {
					//Player1
					//player2
					//player3
				}
				if (players == 4) {
					//Player1
					//player2
					//player3
					//player4
				}

			}

		}

		public static void swap<T>(IList<T> list, int indexA, int indexB)
		{
			T tmp = list[indexA];
			list[indexA] = list[indexB];
			list[indexB] = tmp;
		}



		public static List<int> createDeck (int deckLength)
		{
			Console.WriteLine ("-----------------------");
			//create list
			List<int> newDeck = new List <int> (){ };
			//populate deck
			for (int y = 1; y <= deckLength; y++) {
				int value = y;
				newDeck.Add (value);
			}
			//shuffle deck
			int n = deckLength;
			Random range = new Random ();
			while (n > 1) {
				int i = (range.Next (0, n) % n);
				n--;
				int value = newDeck [i];
				newDeck [i] = newDeck [n];
				newDeck [n] = value;
			}

			return newDeck;
		}

		public static void PlayGame ()
		{
		}
	}
}

