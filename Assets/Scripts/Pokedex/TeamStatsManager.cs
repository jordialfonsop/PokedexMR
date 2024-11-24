using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using static PokedexManager;
using TMPro;

public class TeamStatsManager : MonoBehaviour
{

    [System.Serializable]
    public class TeamStats
    {
        public int id;
        public string name;
        public List<CapturedPokemon> captured_pokemons;
        public int pve_score;
        public int pvp_score;
        public int pokedex_score;
        public bool is_active;
        public TeamStats(int _id, string _name, List<CapturedPokemon> _captured_pokemons, int _pve_score
                        , int _pvp_score, int _pokedex_score, bool _is_active)
        {
            id = _id;
            name = _name;
            captured_pokemons = _captured_pokemons;
            pve_score = _pve_score;
            pvp_score = _pvp_score;
            pokedex_score = _pokedex_score;
            is_active = _is_active;
        }
    }

    [System.Serializable]
    public class CapturedPokemon
    {
        public string id;
        public int pokemon_id;

        public CapturedPokemon(string _id, int _pokemon_id)
        {
            id = _id;
            pokemon_id = _pokemon_id;
        }
    }

    [SerializeField] private TextAsset JSONFile;
    [SerializeField] private JSONNode JSONParse;

    public TMP_Text PVE_Score;
    public TMP_Text PVP_Score;
    public TMP_Text pokedexScore;
    public TMP_Text capturedPokemons;
    public TMP_Text id;


    public TeamStats teamStats;

    private static TeamStatsManager _instance;

    public static TeamStatsManager Instance
    {
        get { return _instance; }
    }

    public void InitializeTeamStats()
    {
        JSONParse = JSONNode.Parse(JSONFile.text);



            int _id = JSONParse["id"];

            string _name = JSONParse["name"];

            int _pve_score = JSONParse["pve_score"];

            List<CapturedPokemon> _captured_pokemons = new List<CapturedPokemon>();
            foreach (JSONNode pokemon in JSONParse["captured_pokemons"]) { 
            
                CapturedPokemon captured_pokemon = new CapturedPokemon(pokemon["id"], pokemon["pokemon_id"]);

                _captured_pokemons.Add(captured_pokemon);
            }

            int _pvp_score = JSONParse["pvp_score"];

            int _pokedex_score = JSONParse["pokedex_score"];

            bool _is_active = true;

            TeamStats _teamStats = new TeamStats(_id,_name,_captured_pokemons,_pve_score,_pvp_score,_pokedex_score,_is_active);

            SetTeamStats(_id.ToString(), _pve_score.ToString(), _pvp_score.ToString(), _pokedex_score.ToString(), _captured_pokemons.Count.ToString());

            teamStats = _teamStats;

    }

    public void SetTeamStats(string _id, string _pve_score, string _pvp_score, string _pokedex_score, string _capturedPokemons)
    {
        PVE_Score.text += " " + _pve_score;
        PVP_Score.text += " " + _pvp_score;
        pokedexScore.text += " " + _pokedex_score;
        capturedPokemons.text += " " + _capturedPokemons;
        id.text += " " + _id;
    }

    void Start()
    {
        InitializeTeamStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        _instance = this;
    }
}
