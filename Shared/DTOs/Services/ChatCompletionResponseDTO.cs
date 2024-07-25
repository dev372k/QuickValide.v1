namespace Shared.DTOs.Services;
using Newtonsoft.Json;
using System;

public class Choice
{
    [JsonProperty("message")]
    public Message Message { get; set; }
}

public class Message
{
    [JsonProperty("content")]
    public string Content { get; set; }
}

public class ChatCompletionResponseDTO
{
    [JsonProperty("choices")]
    public Choice[] Choices { get; set; }
}

public class Datum
{
    [JsonProperty("data")]
    public string Data { get; set; }
}
