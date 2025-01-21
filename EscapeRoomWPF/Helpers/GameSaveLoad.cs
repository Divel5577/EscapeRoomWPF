using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using EscapeRoomWPF.Models;
using EscapeRoomWPF.Models.Items;

namespace EscapeRoomWPF.Helpers
{
    public static class GameSaveLoad
    {
        private const string SaveFilePath = "game_save.json";

        public static void SaveGame(GameState gameState)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new ItemJsonConverter() } // Dodaj niestandardowy konwerter
            };
            var json = JsonSerializer.Serialize(gameState, options);
            File.WriteAllText(SaveFilePath, json);
        }

        public static GameState LoadGame()
        {
            if (!File.Exists(SaveFilePath))
                return null;

            var options = new JsonSerializerOptions
            {
                Converters = { new ItemJsonConverter() } // Dodaj niestandardowy konwerter
            };
            var json = File.ReadAllText(SaveFilePath);
            return JsonSerializer.Deserialize<GameState>(json, options);
        }
    }

    // Niestandardowy konwerter JSON dla abstrakcyjnej klasy Item
    public class ItemJsonConverter : JsonConverter<Item>
    {
        public override Item Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var doc = JsonDocument.ParseValue(ref reader))
            {
                var typeProperty = doc.RootElement.GetProperty("Type").GetString();
                var json = doc.RootElement.GetRawText();

                Item item = typeProperty switch
                {
                    "Door" => JsonSerializer.Deserialize<Door>(json, options),
                    "Bookshelf" => JsonSerializer.Deserialize<Bookshelf>(json, options),
                    "Chandelier" => JsonSerializer.Deserialize<Chandelier>(json, options),
                    "Cobweb" => JsonSerializer.Deserialize<Cobweb>(json, options),
                    "Desk" => JsonSerializer.Deserialize<Desk>(json, options),
                    "Key" => JsonSerializer.Deserialize<Key>(json, options),
                    "Painting" => JsonSerializer.Deserialize<Painting>(json, options),
                    "Journal" => JsonSerializer.Deserialize<Journal>(json, options),
                    _ => throw new NotSupportedException($"Nieobsługiwany typ przedmiotu: {typeProperty}")
                };

                // Przywrócenie interakcji
                item.InitializeInteractions();

                if (item is Bookshelf bookshelf)
                {
                    bookshelf.IsMoved = doc.RootElement.GetProperty("IsMoved").GetBoolean();
                }
                if (item is Desk desk)
                {
                    desk.IsSearched = doc.RootElement.GetProperty("IsSearched").GetBoolean();
                }
                if (item is Door door)
                {
                    door.Code = doc.RootElement.GetProperty("Code").GetString();
                    door.IsOpen = doc.RootElement.GetProperty("IsOpen").GetBoolean();
                }

                return item;
            }
        }

        public override void Write(Utf8JsonWriter writer, Item value, JsonSerializerOptions options)
        {
            var type = value.GetType().Name;
            var json = JsonSerializer.Serialize(value, value.GetType(), options);

            using (var doc = JsonDocument.Parse(json))
            {
                writer.WriteStartObject();
                writer.WriteString("Type", type);

                foreach (var property in doc.RootElement.EnumerateObject())
                {
                    property.WriteTo(writer);
                }

                writer.WriteEndObject();
            }
        }
    }
}