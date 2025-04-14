using System.Net.Http.Json;
using System.Text.Json;

Console.WriteLine("Ingrese el nombre del Pokémon (charcadet, scatterbug, emboar):");
string? nombrePokemon= Console.ReadLine()?.ToLower();

//se valida el nombre del pokémon
if (nombrePokemon != "charcadet" && nombrePokemon != "scatterbug" && nombrePokemon != "emboar")
{
    //si el nombre no es valido responde
    Console.WriteLine("Error. Pokémon no válido");
}
else 
{
    //si el nombre el valido se hace la conexion con la URL
    using var client = new HttpClient();
    var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{nombrePokemon}");
   
    if (response.IsSuccessStatusCode)
    {
        //se pone en formato json para mejor lectura
        var json = await response.Content.ReadAsStringAsync();
        var jsonElemen = JsonSerializer.Deserialize<JsonElement>(json);
        string formattedJson = JsonSerializer.Serialize(jsonElemen, new JsonSerializerOptions {WriteIndented = true});

        Console.WriteLine("\n==Información del Pokémon==");
        Console.WriteLine(formattedJson);
    }
    else
    {
        Console.WriteLine($"Error. No se pudo obtener la información del Pokémon {nombrePokemon}. Código: {response.StatusCode}");
    }
}
Console.WriteLine("\nPresione cualquier tecla para salir...");
Console.ReadLine();
