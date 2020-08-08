using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class GameManager : MonoBehaviour
    {
        [Range(1, 8)]
        public int NumPlayers = 1;

        [SerializeField]
        private PlayerEngine[] players;

        private void Start()
        {
            CreatePlayers();
        }

        private void CreatePlayers()
        {
            players = new PlayerEngine[NumPlayers];

            for (int i = 0; i < NumPlayers; i++)
            {
                players[i] = Instantiate(ResourceManager.Instance.PlayerPrefab, transform).GetComponent<PlayerEngine>();
                players[i].Initialize(i + 1);
            }
        }     
    }
}
