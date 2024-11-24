using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Meta.XR.ImmersiveDebugger.UserInterface;
using UnityEngine.Networking;

public class PokemonProfileManager : MonoBehaviour
{
    private static PokemonProfileManager _instance;

    public static PokemonProfileManager Instance
    {
        get { return _instance; }
    }

    public GameObject UIPanel;

    // Start is called before the first frame update
    public TMP_Text id_name;
    public TMP_Text species;
    public TMP_Text height;
    public TMP_Text weight;
    public TMP_Text hp;
    public TMP_Text attack;
    public TMP_Text defense;
    public TMP_Text specialAttack;
    public TMP_Text specialDefense;
    public TMP_Text speed;
    public TMP_Text types;
    public TMP_Text abilities;
    public TMP_Text description;

    public GameObject sound;

    public Image image;

    public void SetPokemonProfile(string _id, string _name,string _species,string _height, string _weight, string _hp, 
                                    string _attack, string _defense, string _specialAttack, string _specialDefense, 
                                    string _speed, List<string> _types, List<string> _abilities, string _description, string _image, string _audioUrl)
    {
        UIPanel.SetActive(true);

        id_name.text = _id + " - " + _name;
        species.text = "Species: " + _species;
        height.text = "Height: " + _height;
        weight.text = "Weight: " + _weight;
        hp.text = "HP: " + _hp;
        attack.text = "Attack: " + _attack;
        defense.text = "Defense: " + _defense;
        specialAttack.text = "Special Attack: " + _specialAttack;
        specialDefense.text = "Special Defense: " + _specialDefense;
        speed.text = "Speed: " + _speed;
        types.text = "Types: ";
        for (int i = 0; i < _types.Count; i++) {
            types.text += Uppercase(_types[i]) + ", ";
        }
        types.text = types.text.Substring(0, types.text.Length - 2);
        abilities.text = "Abilities: ";
        for (int i = 0; i < _abilities.Count; i++)
        {
            abilities.text += Uppercase(_abilities[i]) + ", ";
        }
        abilities.text = abilities.text.Substring(0, abilities.text.Length - 2);
        description.text = _description;
        sound.GetComponent<PokemonSound>().audioUrl = _audioUrl;
        StartCoroutine(LoadImageFromURL(_image));
        

    }

    public void UpdateDescription(string _description)
    {
        description.text = _description;
        description.fontSize = 0.011f;
        description.fontSize = 0.012f;
    }
    private IEnumerator LoadImageFromURL(string url)
    {

        // Start a UnityWebRequest to download the image
        using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();


            // Convert the downloaded texture into a Sprite and assign it to the Image component
            Texture2D texture = UnityEngine.Networking.DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            image.sprite = sprite;

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

    private void Awake()
    {
        _instance = this;
    }
}
