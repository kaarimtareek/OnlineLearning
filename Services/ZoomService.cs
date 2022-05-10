﻿using Microsoft.AspNetCore.Authentication.JwtBearer;

using OnlineLearning.Common;
using OnlineLearning.Models.NetworkModels;

using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public class ZoomService : IZoomService
    {
        private static string _createUrl = "https://zoom.us/v2/users/me/meetings";
        public async Task<OperationResult<CreatedZoomMeetingResponse>> CreateZoomMeeting(string token, CreateZoomMeetingRequest request)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
                var jsonRequest = JsonSerializer.Serialize(request);
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, jsonRequest);
                var jsonResponse = await client.PostAsync("https://zoom.us/v2/users/me/meetings", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
                if (!jsonResponse.IsSuccessStatusCode)
                {
                    string result = jsonResponse.Content.ReadAsStringAsync().Result;
                    return OperationResult.Fail<CreatedZoomMeetingResponse>();
                }
                var successContent = await jsonResponse.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<CreatedZoomMeetingResponse>(successContent);
                return OperationResult.Success(response);
            }
            catch(Exception ex)
            {
                return OperationResult.Fail<CreatedZoomMeetingResponse>();

            }
        }
    }
}