using System;

[Serializable]
public class PokemonData
{
    public string name;
    public PokemonSprites sprites;
    public PokemonTypeSlot[] types;
}

[Serializable]
public class PokemonSprites
{
    public string front_default;
}

[Serializable]
public class PokemonTypeSlot
{
    public PokemonType type;
}

[Serializable]
public class PokemonType
{
    public string name;
}