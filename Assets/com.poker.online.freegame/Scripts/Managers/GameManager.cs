using System.Collections.Generic;
using System.Collections;

using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get => FindObjectOfType<GameManager>();
    }

    [SerializeField] Deck deck;
    [SerializeField] UserDatabase userDatabase;

    private Room Room { get; set; }
    private List<Card> CardsForGame { get; set; }

    public Sprite Shirt
    {
        get => deck.shirt;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Transform parent = GameObject.Find("Environment").transform;
        Room = Instantiate(Resources.Load<Room>("room"), parent);

        List<Profile> profiles = userDatabase.Profiles;

        for (int i = 0; i < Room.players.Length; i++)
        {
            Profile profile = profiles[Random.Range(0, profiles.Count)];
            Room.players[i].Profile = profile;

            profiles.Remove(profile);
        }

        CardsForGame = deck.Cards;
        StartCoroutine(nameof(GameCycle));
    }

    public void RestartGame()
    {
        StopAllCoroutines();

        if(Room)
        {
            Destroy(Room.gameObject);
        }

        StartGame();
    }

    public void Deal()
    {
        Player.IsMyStep = true;
    }

    private IEnumerator GameCycle()
    {
        while(true)
        {
            foreach(Player p in Room.players)
            {
                yield return p.WaitPlayerTurn();
            }

            yield return null;
        }
    }

    public void AddToPot(int amount)
    {
        Room.AddToPot(amount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(nameof(DealCards));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(DealTableCards(5));
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //foreach(Player player in Room.players)
            //{
            //    player.HideCards(false);
            //}

            Debug.Log($"Winner: {GetWinner().Profile.name}");
        }
    }

    private Card GetCardFromDeck()
    {
        Card card = CardsForGame.GetRandomCard();
        CardsForGame.Remove(card);
        return card;
    }

    private Player GetWinner()
    {
        var tableAndPlayerCards = Room.players.GroupBy(player => new List<Card>()
        {
            player.Cards[0],
            player.Cards[1],

            Room.table.Cards[0],
            Room.table.Cards[1],
            Room.table.Cards[2],
            Room.table.Cards[3],
            Room.table.Cards[4],
        });

        Dictionary<Player, (int, string)> result = new Dictionary<Player, (int, string)>();
        tableAndPlayerCards.ToList().ForEach((data) =>
        {
            result.Add(data.Single(), Combination.GetCombination(data.Key.ToArray()));
        });

        foreach(var res in result)
        {
            res.Key.Combination = res.Value.Item2;
            //Debug.Log($"<color=red>{res.Key.Name}</color> has a combination <color=red>{res.Value.Item2}</color>");
        }

        var winRating = result.Max(res => res.Value.Item1);
        var winCombination = result.Select(res => res.Value).Where(v => v.Item1 == winRating).First();
        //Debug.Log($"Winning combination among players: <color=red>{winCombination.Item2}</color>");

        var winners = result.Where(res => res.Value == winCombination).Select(p => p.Key);

        if (winners.Count() > 1)
        {
            List<Card> winnerCards = new List<Card>();
            winners.ToList().ForEach((data) =>
            {
                winnerCards.AddRange(data.Cards);
            });

            var t = winnerCards.GroupBy(wc => wc.CardValue).ToDictionary(y => y.Key, y => y.Count());
            foreach(var i in t)
            {
                //Debug.Log($"{i.Key} {i.Value}");
            }

            Card HighCard = Combination.GetHighCard(winnerCards.ToArray());
            //Card[] tmp = winners.Select(c => c.Cards.GetMinCard(HighCard)).ToArray();
            //HighCard = Combination.GetHighCard(tmp);

            Player winner = Room.players.Where(player => player.Cards.Contains(HighCard)).First();
            //Debug.Log($"{winner.Name} <color=red>high card </color>{HighCard.CardValue} {HighCard.GetCardStringSuit()}");
            return winner;
        }

        return winners.Single();
    }

    private IEnumerator DealCards()
    {
        foreach (Player player in Room.players)
        {
            player.Cards = new Card[]
            {
                GetCardFromDeck(), GetCardFromDeck()
            };

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator DealTableCards(int count)
    {
        Card[] cards = new Card[count];
        for(int i = 0; i < cards.Length; i++)
        {
            cards[i] = GetCardFromDeck();
        }

        Room.table.Cards = cards.ToList();
        yield return null;
    }
}
