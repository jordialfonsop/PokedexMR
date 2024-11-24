using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using static PokedexManager;

public class PokedexManager : MonoBehaviour
{

    [System.Serializable]
    public class Pokemons
    {
        public List<Pokemon> PokemonList;
    }

    [System.Serializable]
    public class Pokemon
    {
        public int id;
        public string name;
        public List<Abilities> abilities;
        public string cries;
        public int height;
        public List<string> location_area_encounters;
        public List<EvolvesTo> evolves_to;
        public List<Move> moves;
        public List<Species> species;
        public string image;
        public List<Stats> stats;
        public List<Types> types;
        public int weight;
        public Pokemon(int _id, string _name,List<Abilities> _abilities, string _cries, int _height
                        , List<string> _location_area_encounters, List<EvolvesTo> _evolvesTo, List<Move> _moves
                        , List<Species> _species, string _image, List<Stats> _stats, List<Types> _types, int _weight)
        {
            id = _id;
            name = _name;
            abilities = _abilities;
            cries = _cries;
            height = _height;
            location_area_encounters = _location_area_encounters;
            evolves_to = _evolvesTo;
            moves = _moves;
            species = _species;
            image = _image;
            stats = _stats;
            types = _types;
            weight = _weight;            
        }
    }

    [System.Serializable]
    public class Abilities
    {
        public Ability ability;
        public bool is_hidden;
        public int slot;

        public Abilities(Ability _ability,bool _is_hidden, int _slot)
        {

            ability = _ability;
            is_hidden = _is_hidden;
            slot = _slot;

        }
    }

    [System.Serializable]
    public class Ability
    {
        public string name;
        public string url;

        public Ability(string _name, string _url)
        {

            name = _name;
            url = _url;

        }
    }

    [System.Serializable]
    public class EvolvesTo
    {
        public string name;
        public string id;

        public EvolvesTo(string _name, string _id)
        {

            name = _name;
            id = _id;

        }
    }

    [System.Serializable]
    public class Move
    {
        public string name;
        public string url;

        public Move(string _name, string _url)
        {

            name = _name;
            url = _url;

        }
    }

    [System.Serializable]
    public class Species
    {
        public string name;
        public string url;

        public Species(string _name, string _url)
        {

            name = _name;
            url = _url;

        }
    }

    [System.Serializable]
    public class Stats
    {
        public int base_stat;
        public int effort;
        public Stat stat;

        public Stats(int _base_stat, int _effort, Stat _stat)
        {

            base_stat = _base_stat;
            effort = _effort;
            stat = _stat;

        }
    }

    [System.Serializable]
    public class Stat
    {
        public string name;
        public string url;

        public Stat(string _name, string _url)
        {

            name = _name;
            url = _url;

        }
    }

    [System.Serializable]
    public class Types
    {
        public int slot;
        public Type type;

        public Types(int _slot, Type _type)
        {

            slot = _slot;
            type = _type;

        }
    }

    [System.Serializable]
    public class Type
    {
        public string name;
        public string url;

        public Type(string _name, string _url) {

            name = _name;
            url = _url;

        }
    }

    [SerializeField] private TextAsset JSONFile;
    [SerializeField] private JSONNode JSONParse;

    [SerializeField] private GameObject ButtonSlider;
    [SerializeField] private GameObject ButtonPrefab;

    public Pokemons _Pokemons = new Pokemons();


    private static PokedexManager _instance;


    public static PokedexManager Instance
    {
        get { return _instance; }
    }

    public void InitializePokemonList()
    {
        JSONParse = JSONNode.Parse(JSONFile.text);
        foreach (JSONNode Pokemon in JSONParse)
        {
            int _id = Pokemon["id"];

            string _name = Pokemon["name"];
            
            List<Abilities> _abilities = new List<Abilities>();
            foreach (JSONNode _abilitiesList in Pokemon["abilities"])
            {

                List<Ability> ability = new List<Ability>();
                List<string> abilityValues = new List<string>();
                foreach (JSONNode _ability in _abilitiesList["ability"])
                {

                    abilityValues.Add(_ability);
                    
                }
                ability.Add(new Ability(abilityValues[0], abilityValues[1]));

                bool temp_is_hidden;
                if (_abilitiesList["is_hidden"] == "true")
                {
                    temp_is_hidden = true;
                }else
                {
                    temp_is_hidden = false;
                }
                _abilities.Add(new Abilities(ability[0], temp_is_hidden, _abilitiesList["slot"]));

            }

            string _cries = Pokemon["cries"];

            int _height = Pokemon["height"];

            List<string> _location_area_encounters = new List<string>();
            foreach (JSONNode _location_area in Pokemon["location_area_encounters"])
            {
                _location_area_encounters.Add(_location_area);
            }

            List<EvolvesTo> _evolves_To = new List<EvolvesTo>();
            List<string> evolvesToValues = new List<string>();
            foreach (JSONNode _evolves_ToList in Pokemon["evolves_to"])
            {
                evolvesToValues.Add(_evolves_ToList);
                //Debug.Log(_evolves_ToList);
            }
            if (evolvesToValues.Count == 2)
            {

                _evolves_To.Add(new EvolvesTo(evolvesToValues[0], evolvesToValues[1]));
            }
            else
            {
                _evolves_To.Add(new EvolvesTo("null", "null"));
            }            

            List<Move> _moves = new List<Move>();
            string _nameMoves;
            string _urlMoves;
            foreach (JSONNode _movesList in Pokemon["moves"])
            {
                _nameMoves = _movesList["name"];
                _urlMoves = _movesList["url"];
                _moves.Add(new Move(_nameMoves, _urlMoves));

            }


            List<Species> _species = new List<Species>();
            List<string> speciesValues = new List<string>();
            foreach (JSONNode _speciesList in Pokemon["species"])
            {
                speciesValues.Add(_speciesList);
                

            }
            _species.Add(new Species(speciesValues[0], speciesValues[1]));

            string _image = Pokemon["image"];

            List<Stats> _stats = new List<Stats>();
            foreach (JSONNode _statsList in Pokemon["stats"])
            {

                List<Stat> stat = new List<Stat>();
                List<string> statValues = new List<string>();
                foreach (JSONNode _stat in _statsList["stat"])
                {
                    statValues.Add(_stat);
                }
                stat.Add(new Stat(statValues[0], statValues[1]));

                _stats.Add(new Stats(_statsList["base_stat"], _statsList["effort"], stat[0]));

            }

            List<Types> _types = new List<Types>();
            foreach (JSONNode _typesList in Pokemon["types"])
            {

                List<Type> type = new List<Type>();
                List<string> typeValues = new List<string>();
                foreach (JSONNode _type in _typesList["type"])
                {
                    typeValues.Add(_type);
                }
                type.Add(new Type(typeValues[0], typeValues[1]));

                _types.Add(new Types(_typesList["slot"], type[0]));

            }

            int _weight = Pokemon["weight"];

            Pokemon _pokemon = new Pokemon(_id, _name, _abilities, _cries, _height, _location_area_encounters
                                            , _evolves_To, _moves, _species, _image, _stats, _types, _weight);

            _Pokemons.PokemonList.Add(_pokemon);
        }
    }

    public void GeneratePokedexButtons()
    {
        for (int i = 0; i < _Pokemons.PokemonList.Count; i++)
        {
            GameObject button = Instantiate(ButtonPrefab);

            button.transform.SetParent(ButtonSlider.transform);

            button.GetComponent<RectTransform>().localScale = new Vector3(0.86f, 1.0f, 1.0f);
            button.GetComponent<RectTransform>().localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            button.GetComponent<RectTransform>().localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

            button.name = _Pokemons.PokemonList[i].name;

            bool is_collected = false;
            for (int j = 0; j < TeamStatsManager.Instance.teamStats.captured_pokemons.Count; j++) {
                if (_Pokemons.PokemonList[i].id == TeamStatsManager.Instance.teamStats.captured_pokemons[j].pokemon_id)
                {
                    is_collected = true;
                    break;
                }
            }
            if (is_collected)
            {

                button.GetComponent<PokedexButton>().SetText((_Pokemons.PokemonList[i].id) + ": " + char.ToUpper(_Pokemons.PokemonList[i].name[0]) + _Pokemons.PokemonList[i].name.Substring(1));

            }
            else
            {
                button.GetComponent<PokedexButton>().SetText((_Pokemons.PokemonList[i].id) + ": ???");
                button.GetComponent<Toggle>().interactable = false;
                button.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }

            button.GetComponent<PokedexButton>().pokemon = _Pokemons.PokemonList[i];

            if(_Pokemons.PokemonList[i].name == "metapod")
            {
                //button.GetComponent<PokedexButton>().DisplayPokemon();
            }
            

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        InitializePokemonList();
        GeneratePokedexButtons();
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
