using LogProxyApi.Models;

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LogProxyApi.Services;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;

namespace LogProxyApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("messages")]
    public class LogMessageController: ControllerBase
    {
        private readonly ILogger<LogMessageController> _logger;
        private readonly AirTableService airTableService;

        public LogMessageController(ILogger<LogMessageController> logger, AirTableService airTableService)
        {
            this._logger = logger;
            this.airTableService = airTableService;
        }

        [HttpGet]
        public async Task<List<LogMessage>> GetMesages()
        {
            AirTableLogResponse response = await airTableService.GetAllMessages();
            _logger.LogInformation(response.Records.Count.ToString());

            var logs = response.Records.Select(a => new LogMessage{Id=a.fields.id, Title=a.fields.Summary, Text=a.fields.Message, ReceivedAt=a.fields.receivedAt}).ToList();

            return logs;
        }

        [HttpPost]
        public async Task<LogMessage> CreateMessage([FromBody] LogMessage input)
        {
            input.Id = Guid.NewGuid().ToString();
            input.ReceivedAt = DateTime.UtcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
            
            AirTableLogMessage logMessage = new AirTableLogMessage {id=input.Id, Summary=input.Title, Message=input.Text, receivedAt=input.ReceivedAt} ;
            AirTableLogRecord record = new AirTableLogRecord{fields=logMessage};
            
            List<AirTableLogRecord> list = new List<AirTableLogRecord>();
            list.Add(record);
            LogRequest logRequest = new LogRequest{records=list};

            await airTableService.CreateMessage(logRequest);
            return input;
        }
    }
}