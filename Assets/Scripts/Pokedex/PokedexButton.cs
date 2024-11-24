using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class PokedexButton : MonoBehaviour
{

    public PokedexManager.Pokemon pokemon;

    public string description;
    public void SetText(string text)
    {
        transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = text;
    }

    public void DisplayPokemon()
    {

        List<string> stats = new List<string>();
        List<string> types = new List<string>();
        List<string> abilities = new List<string>();
        List< PokedexManager.Type> temp_types = new List<PokedexManager.Type>();
        List<PokedexManager.Ability> temp_abilities = new List<PokedexManager.Ability>();

        List<PokedexManager.Species> _species = pokemon.species;
        foreach (PokedexManager.Stats _stats in pokemon.stats) {
            stats.Add((_stats.base_stat).ToString());
        }

        foreach (PokedexManager.Types _types in pokemon.types)
        {
            temp_types.Add(_types.type);
        }
        foreach (PokedexManager.Type _type in temp_types) {
            types.Add(_type.name);
        }
        foreach (PokedexManager.Abilities _abilities in pokemon.abilities)
        {
            temp_abilities.Add(_abilities.ability);
        }
        foreach (PokedexManager.Ability _ability in temp_abilities)
        {
            abilities.Add(_ability.name);
        }

        StartCoroutine(GetPokemonDescriptionRequest(pokemon.name));

        PokemonProfileManager.Instance.SetPokemonProfile((pokemon.id).ToString(), Uppercase(pokemon.name), Uppercase(_species[0].name), (pokemon.height).ToString(), (pokemon.weight).ToString(), stats[0],
                                                            stats[1], stats[2], stats[3], stats[4], stats[5], types, abilities,
                                                            description, pokemon.image, pokemon.cries);
    }

    private IEnumerator GetPokemonDescriptionRequest(string pokemon)
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:5000/obtener_descripcion/" + pokemon))
        {
            // Send the request and wait for the response
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"GET request failed: {request.error}");
            }
            else
            {
                // Process the response data
                string responseText = request.downloadHandler.text;
                description = responseText;
                //description = description.Split('\u0022' + "description:" + '\u0022' + ": " + '\u0022')[1];
                PokemonProfileManager.Instance.UpdateDescription(description);
                Debug.Log($"Response: {responseText}");
            }
        }
    }

    public string Uppercase(string lowercase)
    {
        return char.ToUpper(lowercase[0]) + lowercase.Substring(1);
    } 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
