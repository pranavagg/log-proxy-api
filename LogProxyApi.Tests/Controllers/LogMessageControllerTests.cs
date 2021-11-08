using System;
using Xunit;
using Moq;
using LogProxyApi.Services;
using LogProxyApi.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LogProxyApi.Controllers
{
    public class LogMessageControllerTests
    {
        [Fact]
        public void ConstructorTest()
        {
            var mock = new Mock<ILogger<LogMessageController>>();
            ILogger<LogMessageController> logger = mock.Object;

            var serviceMock = new Mock<AirTableService>();
            AirTableService airTableService = serviceMock.Object;

            LogMessageController logMessageController = new LogMessageController(logger, airTableService);

            Assert.NotNull(logMessageController);
        }

        [Fact]
        public async void GetAllTest()
        {
            var mock = new Mock<ILogger<LogMessageController>>();
            ILogger<LogMessageController> logger = mock.Object;

            var serviceMock = new Mock<AirTableService>();

            AirTableLogMessage airTableLogMessage = new AirTableLogMessage{id="1", Message="sample Message", Summary = "sample Summary", receivedAt="2021-11-07T21:43:40.906Z"};
            LogRecordResponse airTableLogRecord = new LogRecordResponse{fields=airTableLogMessage, Id="1234", createdTime="2021-11-07T21:43:40.906Z"};

            List<LogRecordResponse> logRecordResponses = new List<LogRecordResponse>();
            logRecordResponses.Add(airTableLogRecord);

            AirTableLogResponse airTableLogResponse = new AirTableLogResponse{Records=logRecordResponses};
            serviceMock.Setup(x => x.GetAllMessages()).ReturnsAsync(airTableLogResponse);
            AirTableService airTableService = serviceMock.Object;

            LogMessageController logMessageController = new LogMessageController(logger, airTableService);

            var response = await logMessageController.GetMesages();

            Assert.Equal("1", response[0].Id);
            Assert.Equal("sample Message", response[0].Text);
            Assert.Equal("sample Summary", response[0].Title);
            Assert.Equal("2021-11-07T21:43:40.906Z", response[0].ReceivedAt);
        }

        [Fact]
        public async void CreateLogTest()
        {
            var mock = new Mock<ILogger<LogMessageController>>();
            ILogger<LogMessageController> logger = mock.Object;

            var serviceMock = new Mock<AirTableService>();
            AirTableLogMessage airTableLogMessage = new AirTableLogMessage{id="1", Message="sample Message", Summary = "sample Summary", receivedAt="2021-11-07T21:43:40.906Z"};
            LogRecordResponse airTableLogRecord = new LogRecordResponse{fields=airTableLogMessage, Id="1234", createdTime="2021-11-07T21:43:40.906Z"};

            List<LogRecordResponse> logRecordResponses = new List<LogRecordResponse>();
            logRecordResponses.Add(airTableLogRecord);

            AirTableLogResponse airTableLogResponse = new AirTableLogResponse{Records=logRecordResponses};

            LogRecordResponse logRecordResponse = new LogRecordResponse();

            serviceMock.Setup(x => x.CreateMessage(It.IsAny<LogRequest>())).ReturnsAsync(logRecordResponse);

            var airTableService = serviceMock.Object;

            LogMessageController logMessageController = new LogMessageController(logger, airTableService);

            LogMessage log = new LogMessage{Title="Some Title", Text="SomeText"};
            var response = await logMessageController.CreateMessage(log);

            serviceMock.Verify(x => x.CreateMessage(It.Is<LogRequest>(lo => lo.records[0].fields.Message == "SomeText" && lo.records[0].fields.Summary == "Some Title")), Times.Once());

            Assert.NotNull(response.Id);
            Assert.NotNull(response.ReceivedAt);
        }

    }
}