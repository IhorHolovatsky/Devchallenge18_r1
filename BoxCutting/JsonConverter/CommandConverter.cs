using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using BoxCutting.Core.Interfaces;
using BoxCutting.Core.Models.Commands;

namespace BoxCutting.Api.JsonConverter
{
    public class CommandConverter : JsonConverter<ICommand>
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeof(ICommand).IsAssignableFrom(typeToConvert);

        public override ICommand Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // not needed in task
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ICommand command, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("command", command.Type.ToString().ToUpper());

            if (command is GotoCommand gotoCommand)
            {
                writer.WriteNumber("x", gotoCommand.X);
                writer.WriteNumber("y", gotoCommand.Y);
            }

            writer.WriteEndObject();
        }
    }
}