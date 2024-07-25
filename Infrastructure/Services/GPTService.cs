using Azure.Messaging;
using Domain.Repositories.Services;
using Newtonsoft.Json;
using Shared.DTOs.Services;
using System.Text;

namespace Infrastructure.Services;

public class GPTService : IGPTService
{
    public async Task<string> ChatCompletion(string input)
    {
        using (var client = new HttpClient())
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://sharp-gpt.ai/postapirequest");
            var content = new StringContent($@"
            {{
                ""inputPrompt"": ""business type: {input}"",
                ""ChatMessage"": [
                    {{
                        ""role"": ""system"",
                        ""content"": ""You are a highly skilled content generator for business landing pages.""
                    }},
                    {{
                        ""role"": ""user"",
                        ""content"": ""Generate a compelling and relevant 1-2 liner descriptive slogans(multiple in an array) for a landing page. The business type is {input}. Only provide the content without any additional text or unrelated information. If the business type is unclear or irrelevant, respond with 'No relevant content found.'. Send response in json format with status true/false for relevant or irrelevant data and content""
                    }}
                ]
            }}",
            Encoding.UTF8,
            "application/json"
            );

            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
            //var chatCompletion = JsonConvert.DeserializeObject<ChatCompletionResponseDTO>(responseJson) ?? new ChatCompletionResponseDTO();
            
            //return chatCompletion.Choices[0].Message.Content ?? "No relevant content found.";
        }
    }
}
